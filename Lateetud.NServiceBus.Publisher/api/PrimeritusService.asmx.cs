using System;
using System.Configuration;
using System.Web.Services;
using Lateetud.NServiceBus.Common;
using Lateetud.NServiceBus.Common.Models.Primeritus;

namespace Lateetud.NServiceBus.Publisher.api
{
    /// <summary>
    /// Summary description for PrimeritusService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PrimeritusService : System.Web.Services.WebService
    {
        MsmqSqlDBConfiguration msmqsqldbconfig = new MsmqSqlDBConfiguration(ConfigurationManager.ConnectionStrings["Lateetud.db.conn"].ConnectionString, "primeritus.error", 50, 5);

        [WebMethod]
        public string CreatePublisherQueues()
        {
            try
            {
                var endpointConfiguration = msmqsqldbconfig.ConfigureEndpoint("primeritus.publisher");
                msmqsqldbconfig.CreateEndpointInitializePipeline(endpointConfiguration).GetAwaiter().GetResult();

                return "Created publishers queues";
            }
            catch (Exception err)
            {
                //return Messages.ServerDown;
                return err.Message;
            }
        }

        [WebMethod]
        public string InvokeXmlService(string XmlString, string RequestType)
        {
            try
            {
                var requestId = "ixs-" + Guid.NewGuid();
                return msmqsqldbconfig.PublishedToBus(msmqsqldbconfig.ConfigureEndpoint("primeritus.publisher"), new VMXml { RequestID = requestId, Message = XmlString, RequestType = RequestType });
            }
            catch (Exception err)
            {
                //return Messages.ServerDown;
                return err.Message;
            }
        }
    }
}
