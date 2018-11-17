using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AzureFunctionsSaver
{
    public static class Saver
    {

        static string ConnectionString = "Endpoint=sb://hector.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=GJ5WPKfRpCivHAmrv6E6jAIjq7Y3cQjHMvFI/v9QkXA=";
        static string QueuePath = "solutionqueue3";

        [FunctionName("SaverFunc")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            
            string output = string.Empty;
            
            dynamic data = await req.Content.ReadAsAsync<object>();
            output  = JsonConvert.SerializeObject(data);
            
            var queueCliente = QueueClient.CreateFromConnectionString(ConnectionString, QueuePath);

            byte[] bytes = Encoding.UTF8.GetBytes(output);
            MemoryStream stream = new MemoryStream(bytes, writable: false);
            BrokeredMessage message = new BrokeredMessage(stream) { ContentType = "application/json" };


            queueCliente.Send(message);

            return req.CreateResponse(HttpStatusCode.OK);
        }
    }
}
