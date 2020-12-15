using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using ServiceBus.Contracts;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBus.Consumer
{
    public class CustomerConsumerService : BackgroundService
    {
        private readonly ISubscriptionClient _subscriptionClient;

        public CustomerConsumerService(ISubscriptionClient subscriptionClient)
        {
            _subscriptionClient = subscriptionClient;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _subscriptionClient.RegisterMessageHandler((message, token) =>
            {
                var customerCreated = JsonConvert.DeserializeObject<CustomerCreated>(Encoding.UTF8.GetString(message.Body));
                Console.WriteLine($"New customer with name {customerCreated.FullName} and Id {customerCreated.Id}");
                return _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
            }, new MessageHandlerOptions((e) => Task.CompletedTask) 
            { 
                AutoComplete = false,
                MaxConcurrentCalls = 1
            });

            return Task.CompletedTask;
        }
    }
}
