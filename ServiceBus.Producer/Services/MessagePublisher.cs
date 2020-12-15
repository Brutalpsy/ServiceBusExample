using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus.Producer.Controllers
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly ITopicClient _queueClient;

        public MessagePublisher(ITopicClient queueClient)
        {
            _queueClient = queueClient;
        }

        public Task Publish<T>(T obj)
        {
            var objectAsText = JsonConvert.SerializeObject(obj);
            var message = new Message(Encoding.UTF8.GetBytes(objectAsText));
            message.UserProperties["messageType"] = typeof(T).Name;
            return _queueClient.SendAsync(message);
        }

        public Task Publish(string raw)
        {
            var message = new Message(Encoding.UTF8.GetBytes(raw));
            message.UserProperties["messageType"] ="Raw";

            return _queueClient.SendAsync(message);
        }
    }
}
