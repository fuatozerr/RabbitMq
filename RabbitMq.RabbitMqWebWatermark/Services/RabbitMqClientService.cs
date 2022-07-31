using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;

namespace RabbitMq.RabbitMqWebWatermark.Services
{
    public class RabbitMqClientService :IDisposable
    {
        private readonly ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        public static string ExchangeName = "ImageDirectExchange";
        public static string RoutingWatermark = "watermark-route-image";
        public static string QueueName = "queue-watermark-image";
        private readonly ILogger<RabbitMqClientService> _logger;

        public RabbitMqClientService(ConnectionFactory connectionFactory, ILogger<RabbitMqClientService> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        public IModel ConnectRabbitMQ()
        {
            _connection = _connectionFactory.CreateConnection();

            if(_channel is { IsOpen:true})
            {
                return _channel;
            }
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(ExchangeName, type: "direct", true, false);
            _channel.QueueDeclare(QueueName, true, false, false,null);
            _channel.QueueBind(QueueName, ExchangeName, RoutingWatermark);
            _logger.LogInformation("Rabbitmq bağlantı kuruldu");
            return _channel;
        }

        public void Dispose()
        {
            _channel?.Close();
            _channel?.Dispose();
            _connection?.Dispose();
            _connection?.Close();
            _logger.LogInformation("Rabbitmq bağlantı koptu");

        }
    }
}
