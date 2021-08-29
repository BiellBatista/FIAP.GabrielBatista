using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace FIAP.GabrielBatista.RabbitMQ
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("RabbitMQ!");

            int count = 0;

            var factory = new ConnectionFactory();
            factory.ConsumerDispatchConcurrency = 100;
            factory.Uri = new Uri("");

            var connection = factory.CreateConnection();

            using (var channel = connection.CreateModel())
            {
                //for (int i = 0; i < 1000; i++)
                //{
                //    var body = Encoding.UTF8.GetBytes($"{i}: Olá mundo!");
                //    channel.BasicPublish(exchange: "xgabriel", routingKey: "", body: body);
                //}

                var consumidor = new EventingBasicConsumer(channel);

                consumidor.Received += (model, eventArg) =>
                {
                    string msg = Encoding.UTF8.GetString(eventArg.Body.Span);
                    Console.WriteLine($"{count++}: recebido {msg}");
                    channel.BasicAck(eventArg.DeliveryTag, false);
                };

                channel.BasicConsume("fiap", autoAck: false, consumidor);

                Console.ReadKey();
            }
        }
    }
}