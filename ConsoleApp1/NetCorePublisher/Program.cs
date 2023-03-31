using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Threading.Tasks.Sources;

namespace NetCorePublisher
{
    public enum LogNames
    {
        Critical = 1, Info = 4, Warning = 3, Error = 2
    }
    class Program
    {
        public static void Main(string[] args)
        {
            #region hello world

            //// Producer
            //var factory = new ConnectionFactory();
            //factory.Uri = new Uri("amqps://umzrsnor:BIJBbGyxTHvowioamoea6-TJUbJ9e0dE@woodpecker.rmq.cloudamqp.com/umzrsnor");

            //// rabbitmq ya bağlanmak için bir bağlantı oluşturuldu
            //using var connection = factory.CreateConnection();

            //// bağlantı var fakat bağlantıyı kanal üzerinden gerçekleştireceğiz kanal oluşturacağız
            //// kanal oluşturma bu kanal ile rabbitmq ile habeleşebiliriz
            //var channel = connection.CreateModel();


            //// queue => kuyruk ismi
            //// durable => false olursa rabbitmq da oluşan kuyruklar memoryde tutulur restart atılırsa tüm kuyrular gider. true olursa kuyruklar fiziksek olarak kaydedilir rabbitmq ya restart atılsa bile kuyruklar gitmez
            //// exslusive => true olursa sadece burda oluşturulan kanal üzerinden bağlanabiliriz. false olursa farklı bir kanal üzerindende bağlanılabilir
            //// autoDelete => true kuyruğa bağlı olan son subscriber(consumer) bağlantıyı kopartırsa kuyruğu otomatik siler. false durumunda son subscriber(consumer) bağlantıyı koparsa bile kuyruk kalır 
            //channel.QueueDeclare("hello-queue", true, false, false);

            //string message = "hello world";

            //var messageBody = Encoding.UTF8.GetBytes(message);
            //// exchange kullanmadan direk kuyruğa gönderilen işleme default exchange denir
            //// default exchange kullanılıyorsa routeKey'e mutlaka kuyruk ismi verilmeli
            //channel.BasicPublish(string.Empty, "hello-queue", null, messageBody);

            //Console.WriteLine("mesaj gönderilmiştir");

            //Console.ReadLine();


            #endregion

            #region work queue
            //var factory = new ConnectionFactory { HostName = "localhost", Port = 5000 };
            ////factory.Uri = new Uri("amqps://umzrsnor:BIJBbGyxTHvowioamoea6-TJUbJ9e0dE@woodpecker.rmq.cloudamqp.com/umzrsnor");
            //using var connection = factory.CreateConnection();
            //var channel = connection.CreateModel();
            //channel.QueueDeclare("hello-queue", true, false, false);
            //Enumerable.Range(1, 50).ToList().ForEach(x =>
            //{
            //    var message = $"message: {x}";
            //    channel.BasicPublish(string.Empty, "hello-queue", null, Encoding.UTF8.GetBytes(message));
            //    Console.WriteLine($"mesaj gönderilmiştir: {message}");
            //});
            //Console.ReadLine();

            #endregion

            #region fenaout exchange
            // herhangi bir filtreleme yapmadan kendisine bağlı olan tüm kuyruklara mesajı iletir
            // aynı mesajı tüm kuyruklara 1 er 1 er iletir

            //var factory = new ConnectionFactory { HostName = "localhost", Port = 5000 };
            //using var connection = factory.CreateConnection();
            //var channel = connection.CreateModel();
            //channel.ExchangeDeclare("logs-fanout", ExchangeType.Fanout, true, false);

            //Enumerable.Range(1, 200).ToList().ForEach(x =>
            //{
            //    var message = Encoding.UTF8.GetBytes($"log:{x}");
            //    channel.BasicPublish("logs-fanout", string.Empty, null, message);
            //    Console.WriteLine(message);
            //});
            #endregion

            #region direct exchange
            // producer tarafından bir mesaj geldiğinde root bilgisine göre ilgili kuyruğa gönderiyor
            //var factory = new ConnectionFactory { HostName = "localhost", Port = 5000 };
            //var connection = factory.CreateConnection();
            //var channel = connection.CreateModel();
            //channel.ExchangeDeclare("logs-direct", type: ExchangeType.Direct, durable: true);
            //Enum.GetNames(typeof(LogNames)).ToList().ForEach(x =>
            //{
            //    var routeKey = $"route-{x}";
            //    var queueName = $"direct-queue-{x}";
            //    channel.QueueDeclare(queueName, true, false, false);
            //    channel.QueueBind(queueName, "logs-direct", routeKey, null);
            //});
            //Enumerable.Range(1, 200).ToList().ForEach(x =>
            //{
            //    LogNames logName = (LogNames)new Random().Next(1, 5);
            //    string message = $"log-type: {logName}";
            //    var messageBody = Encoding.UTF8.GetBytes(message);
            //    var routeKey = $"route-{logName}";
            //    channel.BasicPublish("logs-direct", routeKey, null, messageBody);
            //    Console.WriteLine($"Log gönderilmiştir: {message}");
            //});
            #endregion

            #region topic exchange
            //daha detaylı routelama yapmak için kullanılır
            //var factory = new ConnectionFactory { HostName = "localhost", Port = 5000 };
            //var connection = factory.CreateConnection();
            //var channel = connection.CreateModel();
            //channel.ExchangeDeclare("logs-topic", ExchangeType.Topic, true);            
            //Random rnd = new Random();
            //Enumerable.Range(1, 200).ToList().ForEach(x =>
            //{
            //    LogNames log1 = (LogNames)rnd.Next(1, 5);
            //    LogNames log2 = (LogNames)rnd.Next(1, 5);
            //    LogNames log3 = (LogNames)rnd.Next(1, 5);
            //    string message = $"log-type: {log1}-{log2}-{log3}";
            //    var messageBody= Encoding.UTF8.GetBytes(message);
            //    var routeKey = $"{log1}.{log2}.{log3}";
            //    channel.BasicPublish("logs-topic",routeKey,null,messageBody);
            //    Console.WriteLine($"Log gönderilmiştir: {message}");
            //});
            #endregion

            #region header exchange
            //header üzerinden rootlama yapmaya yarar
            //var factory = new ConnectionFactory { HostName = "localhost", Port = 5000 };
            //var connection = factory.CreateConnection();
            //var channel = connection.CreateModel();
            //channel.ExchangeDeclare("header-exchange", ExchangeType.Headers, true);
            //Dictionary<string, object> headers = new();
            //headers.Add("format", "pdf");
            //headers.Add("shape", "a4");
            //var properties = channel.CreateBasicProperties();
            //properties.Headers = headers;
            //properties.Persistent = true; // mesajları kalıcı hale getirmek için kullanılır
            //channel.BasicPublish("header-exchange", string.Empty, properties, Encoding.UTF8.GetBytes("header mesajı"));
            //Console.WriteLine("mesaj gönderilmiştir");
            //Console.ReadLine();
            #endregion
        }
    }
}