
using System;
using System.Configuration;
using System.Collections.Generic;
using Lateetud.NServiceBus.Common;
using Lateetud.NServiceBus.Common.Models.Primeritus;
using System.ServiceProcess;

namespace Lateetud.NServiceBus.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            //ServiceBase[] serviceBase;
            //serviceBase = new ServiceBase[]
            //{
            //    new PrimeritusPoCNServiceBus()
            //};
            //ServiceBase.Run(serviceBase);



            new PrimeritusPoCNServiceBus().debug();
            var key = Console.ReadKey();

        }

        public void ServiceConfig()
        {
            MsmqSqlDBConfiguration msmqsqldbconfig = new MsmqSqlDBConfiguration(ConfigurationManager.ConnectionStrings["Lateetud.db.conn"].ConnectionString, "primeritus.error", 50, 5);

            List<PublisherEndpoints> publisherEndpoints = new List<PublisherEndpoints>();
            publisherEndpoints.Add(new PublisherEndpoints(endpointName: "primeritus.publisher", messageType: typeof(VMXml)));
            var endpointConfiguration = msmqsqldbconfig.ConfigureEndpoint("primeritus.subscriber", publisherEndpoints);
            msmqsqldbconfig.CreateEndpointInitializePipeline(endpointConfiguration).GetAwaiter().GetResult();

            //publisherEndpoints.Clear();
            //publisherEndpoints.Add(new PublisherEndpoints(endpointName: "smart.publisher", messageType: typeof(RPA)));
            //endpointConfiguration = msmqsqldbconfig.ConfigureEndpoint("smart.subscriber", publisherEndpoints);
            //msmqsqldbconfig.CreateEndpointInitializePipeline(endpointConfiguration).GetAwaiter().GetResult();

        }
    }
}
