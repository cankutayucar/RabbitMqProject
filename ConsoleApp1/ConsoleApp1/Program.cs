


using RabbitMQ.Client;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://umzrsnor:BIJBbGyxTHvowioamoea6-TJUbJ9e0dE@woodpecker.rmq.cloudamqp.com/umzrsnor");

            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            //channel.QueueDeclare("hello-queue", true, false, false);

            channel.ExchangeDeclare("logs-fanout", durable: true, type: ExchangeType.Fanout);

            Enumerable.Range(1, 50).ToList().ForEach(x =>
            {
                string message = $"log {x}";

                var messageBody = Encoding.UTF8.GetBytes(message);

                //channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);
                channel.BasicPublish("logs-fanout", "", null, messageBody);

                Console.WriteLine($"mesaj gönderilmiştir: {message}");
            });

            Console.ReadLine();
        }
    }
}