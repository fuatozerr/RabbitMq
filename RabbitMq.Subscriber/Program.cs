using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMq.Subscriber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            var builder = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.json", true, true);

            var config = builder.Build();
            var connectionString = config["ConnectionStringRabbitMQ"];
            factory.Uri = new Uri(connectionString);

            using var connection = factory.CreateConnection();
            string routeKey = "hello-queue";
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(routeKey, false, consumer);
            consumer.Received += Consumer_Received;
            /*channel.QueueDeclare(routeKey, true, false, false);
            string message = "Hello rabbitmq :P";
            var messageBody = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(string.Empty, routeKey, null, messageBody);
            Console.WriteLine("mesaj gönderildi");
            Console.ReadLine();*/
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body.ToArray());
            Console.WriteLine(message);
         //   throw new NotImplementedException();
        }
    }
}
