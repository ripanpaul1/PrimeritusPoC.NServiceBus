
using System.ServiceProcess;

namespace Lateetud.NServiceBus.Subscriber
{
    partial class PrimeritusPoCNServiceBus : ServiceBase
    {
        public PrimeritusPoCNServiceBus()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Program program = new Program();
            program.ServiceConfig();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        public void debug()
        {
            OnStart(null);
        }
    }
}
