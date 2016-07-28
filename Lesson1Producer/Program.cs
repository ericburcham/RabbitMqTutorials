using System;
using System.Text;
using RabbitMQ.Client;

namespace Lesson1Producer
{
    public class Program
    {
        static void Main()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "Isaiah_60"
            };

            const string message = "Hello World!";
            using (var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: "Lesson1Producer",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: "Lesson1Producer",
                        basicProperties: null,
                        body: body);

                    Console.WriteLine($" [x] Sent {message}");
                }
            }

            Console.WriteLine(value: " Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
