using Microsoft.AspNetCore.Mvc;
using ServiceBus.Contracts;
using ServiceBus.Producer.Requests;
using System.Threading.Tasks;

namespace ServiceBus.Producer.Controllers
{
    public class MessagingController : ControllerBase
    {
        private readonly IMessagePublisher _messagePublisher;

        public MessagingController(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }


        [HttpPost("publish/customer")]
        public async Task<IActionResult> PublishCustomer([FromBody] CreateCustomerRequest request)
        {

            var customerCreated = new CustomerCreated
            {
                Id = request.Id,
                FullName = request.FullName,
                DateOfBirth = request.DateOfBirth
            };

            await _messagePublisher.Publish(customerCreated);
            return Ok();
        }

        [HttpPost("publish/text")]
        public async Task<IActionResult> PublishText([FromBody] CreateTextRequest request)
        {
            await _messagePublisher.Publish(request.Prop);
            return Ok();
        }

            
        [HttpPost("publish/order")]
        public async Task<IActionResult> PublishOrder([FromBody] CreateOrderRequest request)
        {

            var orderCreated = new OrderCreated
            {
                Id = request.Id,
                ProductName = request.ProductName,
            };

            await _messagePublisher.Publish(orderCreated);
            return Ok();
        }
    }
}
