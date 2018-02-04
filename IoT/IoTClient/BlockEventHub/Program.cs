using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;

namespace BlockEventHub
{
    public class Program
    {
        private static EventHubClient eventHubClient;
        private const string EhConnectionString = "Endpoint=sb://hblockchainXXXXX[Use your EventHub Connection STraing]";
        private const string EhEntityPath = "activityhub";

        public static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }
        private static async Task MainAsync(string[] args)
            {
                // Creates an EventHubsConnectionStringBuilder object from the connection string, and sets the EntityPath.
                // Typically, the connection string should have the entity path in it, but this simple scenario
                // uses the connection string from the namespace.
                var connectionStringBuilder = new EventHubsConnectionStringBuilder(EhConnectionString)
                {
                    EntityPath = EhEntityPath
                };

                eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());

                await SendMessagesToEventHub(100);

                await eventHubClient.CloseAsync();

                Console.WriteLine("Press ENTER to exit.");
                Console.ReadLine();
            }

            // Creates an event hub client and sends 100 messages to the event hub.
            private static async Task SendMessagesToEventHub(int numMessagesToSend)
            {
                for (var i = 0; i < numMessagesToSend; i++)
                {
                string text = System.IO.File.ReadAllText(@"d:\Temp\BlockChain\AssetTransfer-Create.json");

                try
                {
                        var message = text;
                        Console.WriteLine($"以下訊息已被送到區塊鍊儲存" +$": {message}");
                        await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine($"{DateTime.Now} > Exception: {exception.Message}");
                    }

                    await Task.Delay(5000);
                }

                Console.WriteLine($"{numMessagesToSend} messages sent.");
            }
        }

    }
