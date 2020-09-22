using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;

namespace NewOrderSender
{
    class Program
    {

        /*
         
          {
            "serviceBus": {
              "resourceGroup": "FIAP-16NET",
              "namespaceName": "geekburger16NET",
              "connectionString": "Endpoint=sb://geekburger16net.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=hWUhqKVScU2nzRMe3qcs+wJSewH37tCoFIBwqH/xA1U=",
              "clientId": "46dbd19e-9f4e-4eeb-af56-de8dc11b9d48",
              "clientSecret": "4o-_S6Mk96nvAs3SE.RuH0B4pyGqf1.4IT",
              "subscriptionId": "505af470-d6ac-4547-9743-3a97059e8e1d",
              "tenantId": "0ddea7e6-2201-4f52-8353-a58bf25762ab"
            }
          }
         
         */


        const string ServiceBusConnectionString = "Endpoint=sb://geekburger16net.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=hWUhqKVScU2nzRMe3qcs+wJSewH37tCoFIBwqH/xA1U=";
        const string TopicName = "neworder";
        static ITopicClient topicClient;

        public static async Task Main(string[] args)
        {

            //IConfiguration configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            //var config = configuration
            //    .GetSection("serviceBus")
            //    .Get<ServiceBusConfiguration>();



            //var credentials = SdkContext.AzureCredentialsFactory
            //    .FromServicePrincipal(config.ClientId,
            //    config.ClientSecret,
            //    config.TenantId,
            //    AzureEnvironment.AzureGlobalCloud);

            //var serviceBusManager = ServiceBusManager
            //    .Authenticate(credentials, config.SubscriptionId);

            //return serviceBusManager
            //    .Namespaces
            //    .GetByResourceGroup(config.ResourceGroup, config.NamespaceName);

            const int numberOfMessages = 10;
            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);

            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after sending all the messages.");
            Console.WriteLine("======================================================");

            // Send messages.
            await SendMessagesAsync(numberOfMessages);

            Console.ReadKey();

            await topicClient.CloseAsync();
        }

        static async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {
                for (var i = 0; i < numberOfMessagesToSend; i++)
                {
                    // Create a new message to send to the topic
                    string messageBody = $"Message {i}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    // Write the body of the message to the console
                    Console.WriteLine($"Sending message: {messageBody}");

                    /*                     
                    // Create a new message to send to the topic
                    NewOrder newOrder = new NewOrder()
                    {
                        OrderId = i,
                        StoreName = "Paulista",
                        Total = i * 10000,
                        Products = new List<Product>()
                        {
                            new Product(){ ProductId=1111 },
                            new Product(){ ProductId=1112 },
                            new Product(){ ProductId=1113 },
                            new Product(){ ProductId=1114 }
                        },
                        ProductionIds = new List<int>(){11111, 1112, 1113, 1114 }
                    };

                    var messageBody = System.Text.Json.JsonSerializer.Serialize(newOrder);
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    // Write the body of the message to the console
                    Console.WriteLine($"Sending message: {messageBody}");
                    */

                    // Send the message to the topic
                    await topicClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }



    }
}
