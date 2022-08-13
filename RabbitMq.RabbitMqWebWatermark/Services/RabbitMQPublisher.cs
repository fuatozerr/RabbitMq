using RabbitMq.RabbitMqWebWatermark.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace RabbitMq.RabbitMqWebWatermark.Services
{
    public class RabbitMQPublisher
    {
        private readonly RabbitMqClientService _rabbitMQClientService;

        public RabbitMQPublisher(RabbitMqClientService rabbitMQClientService)
        {
            _rabbitMQClientService = rabbitMQClientService;
        }

        public void Publish(productImageCreatedEvent productImageCreatedEvent)
        {
            var channel = _rabbitMQClientService.ConnectRabbitMQ();

            var bodyString = JsonSerializer.Serialize(productImageCreatedEvent);

            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: RabbitMqClientService.ExchangeName, routingKey: RabbitMqClientService.RoutingWatermark, basicProperties: properties, body: bodyByte);

        }
    }
}
