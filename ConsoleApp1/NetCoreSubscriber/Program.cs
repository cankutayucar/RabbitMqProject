using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Xsl;

namespace NetCoreSubscriber
{
    class Program
    {
        public static void Main(string[] args)
        {
            #region hello world

            //var factory = new ConnectionFactory();
            //factory.Uri = new Uri("amqps://umzrsnor:BIJBbGyxTHvowioamoea6-TJUbJ9e0dE@woodpecker.rmq.cloudamqp.com/umzrsnor");
            //using var connection = factory.CreateConnection();
            //var channel = connection.CreateModel();
            //// queue declare silinirse subscriber(consumer) ayağa kalktığında böyle bir kuyruk yok ise hata alınır var ise sorun teşkil etmez
            //// publisher(producer) bu kuyruğu oluşturmaz ise subscriber(consumer) bu kuyruğu oluşturur ve uyugulamada hata almayız
            //// publisher(producer) tarafında oluşturulan kuyruk ile subscriber(consumer) tarafında oluşturulan kuruğun parametreleri aynı olmak zorunda
            ////channel.QueueDeclare("hello-queue", true, false, false); 


            //var consumer = new EventingBasicConsumer(channel);



            //// consumer hangi kuyruğu dinleyecek ismini veriyoruz
            //// autoAck => true olursa rabbitmq subscriber'e mesaj gönderdiğinde mesaj doğruda işlense yanlışta işlense mesajı kuyruktan siler. false olursa gelen mesajı kuyruktan silme ben silmen için sana haber vereceğim
            //channel.BasicConsume("hello-queue", true, consumer);

            //// gelen mesajları alma
            //consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
            //{
            //    var message = Encoding.UTF8.GetString(e.Body.ToArray());
            //    Console.WriteLine(message);
            //};

            //Console.ReadLine();
            #endregion

            #region work queue


            //var factory = new ConnectionFactory { HostName = "localhost", Port = 5000 };
            ////factory.Uri = new Uri("amqps://umzrsnor:BIJBbGyxTHvowioamoea6-TJUbJ9e0dE@woodpecker.rmq.cloudamqp.com/umzrsnor");
            //using var connection = factory.CreateConnection();
            //var channel = connection.CreateModel();

            //// rabbitmq dan mesajları kaçar tane alacağımız yani her bir consumere kaç mesaj gelecek 
            //// perfetchSize => gelen mesajın boyutu => (0) herhangi bir boyuttaki mesajı gönderebilirsin
            //// perfetchCount => kaç kaç mesaj gelsin yani her bir consumere kaç mesaj gelsin
            //// global => false olursa girilen count kadar her bir seferde her bir consumere'a count sayısı kadar mesaj gönderir. eğer true olursa girilen count sayısını her bir consumer'e bölüştürür yani 6 mesaj gelsin 3 consumer olsun 2-2-2 bölüştürür veya 5 mesaj gelsin 2 consumer olsun 3-2 olarak merajları bölüştürür
            //channel.BasicQos(0, 1, false);

            //var consumer = new EventingBasicConsumer(channel);
            //// autoAck => false kuyruktan mesajı silmek için haber bekle
            //channel.BasicConsume("hello-queue", false, consumer);

            //Console.WriteLine("mesaj okunuyor...");

            //consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
            //{
            //    var message = Encoding.UTF8.GetString(e.Body.ToArray());
            //    Thread.Sleep(1500);
            //    Console.WriteLine(message);
            //    // ilgili mesaj işlendi ve mesajı kuyruktan silebilirsin
            //    // deliveryTag => hangi tagla mesaj ulaştıysa o tagı bulur ve mesajı siler
            //    // multiple => memoryde işlenmiş ve rabbitmq ya gitmeyen mesajları rabbitmqya haberdar eder
            //    channel.BasicAck(e.DeliveryTag, false);
            //};
            //Console.ReadLine();
            #endregion

            #region fenaout exchange
            //var factory = new ConnectionFactory { HostName = "localhost", Port = 5000 };
            //var connection = factory.CreateConnection();
            //var channel = connection.CreateModel();


            //var randomQueueName = channel.QueueDeclare().QueueName;


            //// kuyruğu kalıcı hale getirme
            ////var randomQueueName = "log-database-save-queue";
            ////channel.QueueDeclare(randomQueueName, true, false, false);

            //// her subscriber için uygulama ayağa kaltığında kuyruk oluşur ve down olunca kuyruk geri silinir
            //channel.QueueBind(randomQueueName, "logs-fanout", string.Empty, null);


            //channel.BasicQos(0, 1, false);
            //var consumer = new EventingBasicConsumer(channel);
            //channel.BasicConsume(randomQueueName, false, consumer);
            //Console.WriteLine("loglar dinleniyor...");
            //consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
            //{
            //    var message = Encoding.UTF8.GetString(e.Body.ToArray());
            //    Thread.Sleep(1000);
            //    Console.WriteLine(message);
            //    channel.BasicAck(e.DeliveryTag, false);
            //};
            //Console.ReadLine();
            #endregion

            #region direct exchange
            //var factory = new ConnectionFactory { HostName = "localhost", Port = 5000 };
            //var connection = factory.CreateConnection();
            //var channel = connection.CreateModel();
            //channel.BasicQos(0, 1, false);
            //var consumer = new EventingBasicConsumer(channel);
            //var queueName = "direct-queue-Critical";
            //channel.BasicConsume(queueName, false, consumer);
            //Console.WriteLine("Loglar dinleniyor");
            //consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
            //{
            //    var message = Encoding.UTF8.GetString(e.Body.ToArray());
            //    Thread.Sleep(1000);
            //    Console.WriteLine($"gelen mesaj: {message}");
            //    File.AppendAllText("log-critical.txt", message + "\n");
            //    channel.BasicAck(e.DeliveryTag,false);
            //};
            //Console.ReadLine();
            #endregion

            #region topic exchange
            //var factory = new ConnectionFactory { HostName = "localhost", Port = 5000 };
            //var connection = factory.CreateConnection();
            //var channel = connection.CreateModel();
            //channel.BasicQos(0, 1, false);
            //var consumer = new EventingBasicConsumer(channel);
            //var randomQueueName = channel.QueueDeclare().QueueName;
            //var routeKey = "*.Error.*";
            ////var routeKey = "Error.#";
            //channel.QueueBind(randomQueueName, "logs-topic", routeKey);
            //channel.BasicConsume(randomQueueName, false, consumer);
            //Console.WriteLine("Loglar dinleniyor...");
            //consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
            //{
            //    var message = Encoding.UTF8.GetString(e.Body.ToArray());
            //    Thread.Sleep(1000);
            //    Console.WriteLine("Gelen mesaj: " + message);
            //    channel.BasicAck(e.DeliveryTag, false);
            //};
            //Console.ReadLine();
            #endregion

            #region header exchange
            //var factory = new ConnectionFactory { HostName = "localhost", Port = 5000 };
            //var connection = factory.CreateConnection();
            //var channel = connection.CreateModel();
            //channel.BasicQos(0, 1, false);
            //var consumer = new EventingBasicConsumer(channel);
            //var randomQueueName = channel.QueueDeclare().QueueName;
            //Dictionary<string, object> headers = new();
            //headers.Add("format", "pdf");
            //headers.Add("shape", "a4");
            ////headers.Add("x-match", "all");
            //headers.Add("x-match", "any");
            //channel.QueueBind(randomQueueName, "header-exchange", string.Empty, headers);
            //channel.BasicConsume(randomQueueName, false, consumer);
            //Console.WriteLine("mesaj dinleniyor...");
            //consumer.Received += (object? sender, BasicDeliverEventArgs e) =>
            //{
            //    var message = Encoding.UTF8.GetString(e.Body.ToArray());
            //    Thread.Sleep(1000);
            //    Console.WriteLine("Gelen mesaj: " + message);
            //    channel.BasicAck(e.DeliveryTag, false);
            //};
            //Console.ReadLine();
            #endregion
        }
    }
}