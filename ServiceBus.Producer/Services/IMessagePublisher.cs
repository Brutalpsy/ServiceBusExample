using System.Threading.Tasks;

namespace ServiceBus.Producer.Controllers
{
    public interface IMessagePublisher
    {
        Task Publish<T>(T obj);
        Task Publish(string raw);
    }
}
