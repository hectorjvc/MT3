using System;
using System.Configuration;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Microsoft.ServiceBus.Messaging;


namespace AzureFunctionsExtractor
{
    public static class Function1
    {
        [FunctionName("ExtractorFunc")]
        public static void Run([ServiceBusTrigger("solutionqueue3", AccessRights.Manage, 
            Connection = "ConnectionServiceBus")]string myQueueItem, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {myQueueItem}");

            Customer customer = JsonConvert.DeserializeObject<Customer>(myQueueItem);


            CloudStorageAccount storageAccount =
                CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=solutionproposal;AccountKey=LLZuABlBo3fcA9CrzAv8N0msjUlYaAWkMo11GqPsMvAvWGnvgZiWjW8QVIxp2becGsaMkL+AfB7dAKuSaPb+nw==");
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("customers");
            table.CreateIfNotExists();

            Customer cust = new Customer(customer.Name, customer.Email);
            cust.Address = customer.Address;
            cust.Phone = customer.Phone;
            cust.Age = customer.Age;
            
            var insertOperation = TableOperation.InsertOrReplace(cust);

            table.Execute(insertOperation);
        }
    }

    public class Customer : TableEntity
    {
        public Customer(string name, string email)
        {
            this.PartitionKey = name;
            this.RowKey = email;

        }

        public Customer() { }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
    }
}
