using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.IO;

namespace RabbitMq.Publisher
{
    internal class Program
    {
        static void Main(string[] args)
        {
           var factory = new ConnectionFactory ();
            var builder = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.json", true, true);

            var config = builder.Build();
            var connectionString = config["ConnectionStringRabbitMQ"];
          //  var emailHost = config["Smtp:Host"];
            factory.Uri = new Uri(connectionString);

            using var connection = factory.CreateConnection();
            
        }
    }
}
