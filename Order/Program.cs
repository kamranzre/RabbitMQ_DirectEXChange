using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Order
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare("order.create", false, false, false, null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                string Msg = Encoding.UTF8.GetString(body);
                Console.WriteLine("Data" + Msg);
                channel.BasicAck(eventArgs.DeliveryTag,true);
            };
            channel.BasicConsume("order.create",false,consumer);

            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
