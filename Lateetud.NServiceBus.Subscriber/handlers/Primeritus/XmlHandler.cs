using System.Threading.Tasks;
using NServiceBus;
using Lateetud.NServiceBus.Common.Models.Primeritus;
using Lateetud.Utilities;
using Lateetud.Utilities.Models;
using Lateetud.NServiceBus.Subscriber.LibClasses;

namespace Lateetud.NServiceBus.Subscriber
{
    public class XmlHandler :
        IHandleMessages<VMXml>
    {
        public Task Handle(VMXml message, IMessageHandlerContext context)
        {
            try
            {
                FileModel TheFile = new PrimeritusXmlService().XmlToAuraString(message.Message);
                if (TheFile == null) return Task.FromCanceled(System.Threading.CancellationToken.None);
                if (TheFile.Status == PStatus.Failed) return Task.FromCanceled(System.Threading.CancellationToken.None);
                if (!new PrimeritusXmlService().IsSendAura(TheFile.FileContent)) return Task.FromCanceled(System.Threading.CancellationToken.None);
                return Task.CompletedTask;
            }
            catch
            {
                return Task.FromCanceled(System.Threading.CancellationToken.None);
            }
        }
    }
}


