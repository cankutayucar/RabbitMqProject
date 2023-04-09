using MassTransit;
using mt.worker.publisher;
using mt.worker.publisher.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configurator =>
        {
            configurator.UsingRabbitMq((context, _configurator) =>
            {
                _configurator.Host("amqps://yvjtpqwu:a6LVFGHIgCTeFVTrhY0xGOQ1QZdXmfNS@vulture.rmq.cloudamqp.com/yvjtpqwu");
            });
        });
        services.AddHostedService<PublishMessageService>(provider =>
        {
            using IServiceScope scope = provider.CreateScope();
            IPublishEndpoint publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
            ISendEndpointProvider sendEndpointProvider = scope.ServiceProvider.GetRequiredService<ISendEndpointProvider>();
            IBus bus = scope.ServiceProvider.GetRequiredService<IBus>();
            return new(publishEndpoint, sendEndpointProvider, bus);
        });
    })
    .Build();

host.Run();
