using Azure.Messaging.ServiceBus;
using ServiceBusTestApi.Models;
using System.Text.Json;

namespace ServiceBusTestApi.Services
{
    public class ServiceBusPublisher
    {
        private readonly ServiceBusClient _client;
        private readonly ServiceBusSender _sender;

        public ServiceBusPublisher(string connectionString)
        {
            _client = new ServiceBusClient(connectionString);
            _sender = _client.CreateSender("orderplaced");
        }

        public async Task PublishOrderAsync(CreateOrderDto order)
        {
            var json = JsonSerializer.Serialize(order);
            var message = new ServiceBusMessage(json);
            await _sender.SendMessageAsync(message);
        }
    }
}
