using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.IO;
using System.Text;

namespace RabbitMq.Publisher
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
            //  var emailHost = config["Smtp:Host"];
            factory.Uri = new Uri(connectionString);
            for (int i = 0; i < 10; i++)
            {
                

                using var connection = factory.CreateConnection();
                string routeKey = "hello-queue";
                var channel = connection.CreateModel();
                channel.QueueDeclare(routeKey, true, false, false);
                string message = "Hello rabbitmq :P";
                var messageBody = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(string.Empty, routeKey, null, messageBody);
               
            }
            Console.WriteLine("mesaj gönderildi");
            Console.ReadLine();
        }
    }
}
