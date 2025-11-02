using Microsoft.AspNetCore.Mvc;
using ServiceBusTestApi.Models;
using ServiceBusTestApi.Services;
using Swashbuckle.AspNetCore.Filters;

namespace ServiceBusTestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ServiceBusPublisher _publisher;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ServiceBusPublisher publisher, ILogger<OrderController> logger)
        {
            _publisher = publisher;
            _logger = logger;
        }

        [HttpPost("publish")]
        [SwaggerRequestExample(typeof(CreateOrderDto), typeof(CreateOrderDtoExample))]
        public async Task<IActionResult> PublishOrder([FromBody] CreateOrderDto order)
        {
            try
            {
                await _publisher.PublishOrderAsync(order);
                _logger.LogInformation($"Order {order.Id} sent to Service Bus.");
                return Ok(new { Message = "Order published successfully!", order.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing message to Service Bus.");
                return StatusCode(500, "Failed to publish message.");
            }
        }
    }
}
