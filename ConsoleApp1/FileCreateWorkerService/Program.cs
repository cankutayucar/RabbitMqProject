using FileCreateWorkerService;
using FileCreateWorkerService.Models;
using FileCreateWorkerService.Services;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<AdventureWorks2019Context>(opt =>
        {
            opt.UseSqlServer(hostContext.Configuration.GetConnectionString("sqlservercon"));
        });
        services.AddSingleton(sp => new ConnectionFactory() { HostName = "localhost", Port = 5000, DispatchConsumersAsync = true });
        services.AddSingleton<RabbitMQClientService>();
        services.AddHostedService<Worker>();
    })
    .Build();
await host.RunAsync();
