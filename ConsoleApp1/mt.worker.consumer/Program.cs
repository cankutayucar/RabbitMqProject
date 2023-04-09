using MassTransit;
using mt.worker.consumer;
using mt.worker.consumer.Consumers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<ExampleMessageConsumer>();
            configurator.UsingRabbitMq((context, _configurator) =>
            {
                _configurator.Host("amqps://yvjtpqwu:a6LVFGHIgCTeFVTrhY0xGOQ1QZdXmfNS@vulture.rmq.cloudamqp.com/yvjtpqwu");
                _configurator.ReceiveEndpoint("example-message-queue", e => e.ConfigureConsumer<ExampleMessageConsumer>(context));
            });
        });
    })
    .Build();

await host.RunAsync();
