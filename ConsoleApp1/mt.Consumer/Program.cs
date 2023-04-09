//consumer
using MassTransit;
using mt.Consumer.Consumers;
var rabbitmqhost = "amqps://yvjtpqwu:a6LVFGHIgCTeFVTrhY0xGOQ1QZdXmfNS@vulture.rmq.cloudamqp.com/yvjtpqwu";
var queuename = "example-queue";
IBusControl bus = Bus.Factory.CreateUsingRabbitMq(configure =>
{
    configure.Host(rabbitmqhost);
    configure.ReceiveEndpoint(queuename, endpoint =>
    {
        endpoint.Consumer<ExampleMessageConsumer>();
    });
});
await bus.StartAsync();
Console.Read();