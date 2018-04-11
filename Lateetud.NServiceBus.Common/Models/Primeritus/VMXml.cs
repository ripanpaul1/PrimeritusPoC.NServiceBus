
using NServiceBus;

namespace Lateetud.NServiceBus.Common.Models.Primeritus
{
    public class VMXml :
        MessageType, IEvent
    {
        public string RequestType { get; set; }
    }
}
