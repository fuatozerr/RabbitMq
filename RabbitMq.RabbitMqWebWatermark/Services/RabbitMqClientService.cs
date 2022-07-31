using RabbitMQ.Client;

namespace RabbitMq.RabbitMqWebWatermark.Services
{
    public class RabbitMqClientService
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string ExchangeName = "ImageDirectExchange";
        public static string RoutingWatermark = "watermark-route-image";
        public static string QueueName = "queue-watermark-image";
    }
}
