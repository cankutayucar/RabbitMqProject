


using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ConsoleApp2
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

            var randomQueueName = channel.QueueDeclare().QueueName;

            //channel.QueueDeclare(randomQueueName,)

            channel.QueueBind(randomQueueName, "logs-fanout", "", null);

            channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume(randomQueueName, false, consumer);

            Console.WriteLine("loglar dinleniyor...");

            consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());

                Console.WriteLine(message);

                channel.BasicAck(e.DeliveryTag, false);
            };

            Console.ReadLine();
        }

    }
}