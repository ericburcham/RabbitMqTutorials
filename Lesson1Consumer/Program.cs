using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Lesson1Consumer
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

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($" [x] Received {message}");
                    };

                    channel.BasicConsume(
                        queue: "Lesson1Producer",
                        noAck: true,
                        consumer: consumer);

                    Console.WriteLine(value: " Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
