// producer
using MassTransit;
using Shared.Messages;
var rabbitmqhost = "amqps://yvjtpqwu:a6LVFGHIgCTeFVTrhY0xGOQ1QZdXmfNS@vulture.rmq.cloudamqp.com/yvjtpqwu";
var queuename = "example-queue";
IBusControl bus = Bus.Factory.CreateUsingRabbitMq(configure =>
{
    configure.Host(rabbitmqhost);
});
ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"{rabbitmqhost}/{queuename}"));
Console.Write("Gönderilecek mesaj: ");
string message = Console.ReadLine();
await sendEndpoint.Send<IMessage>(new ExampleMessage
{
    Text = message
});
Console.Read();