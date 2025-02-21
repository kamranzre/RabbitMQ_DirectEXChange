using RabbitMQ.Client;
using System;
using System.Text;

namespace Basket
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
            string message = $"Send Shopping cart information to place an order Time{DateTime.Now.Second}";
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish("", routingKey: "order.create", null, body);

            //در ورودی BasicPublish 
            // است Direct را خالی ارسال کنیم یعنی نوع آن Exchange نام

            Console.ReadKey();
        }
    }
}
