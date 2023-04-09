using MassTransit;
using Shared.Messages;
using Shared.RequestResponseMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mt.worker.publisher.Services
{
    public class PublishMessageService : BackgroundService
    {
        readonly IPublishEndpoint _publishEndpoint;
        readonly ISendEndpointProvider _sendEndpointProvider;
        readonly IBus _bus;
        string host = "amqps://localhost:5000";
        public PublishMessageService(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider, IBus bus)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _publishEndpoint = publishEndpoint;
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //var send = await _sendEndpointProvider.GetSendEndpoint(new("queue:asd"));
            //await send.Send<IMessage>(
            //     new ExampleMessage()
            //     {
            //         Text = $"mesaj"
            //     });
            //int i = 0;
            //while (true)
            //{
            //    ExampleMessage message = new()
            //    {
            //        Text = $"{++i}. mesaj"
            //    };
            //    await _publishEndpoint.Publish(message);
            //}

            IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
            {
                factory.Host(host);
            });

            await bus.StartAsync();

            IRequestClient<RequestMessage> request = bus.CreateRequestClient<RequestMessage>(new Uri($"{host}/request-queue"));

            int i = 1;
            while (true)
            {
                await Task.Delay(1000);
                var response = await request.GetResponse<ResponseMessage>(new() { MessageNo = i, Text = $"{i}. request" });
                await Console.Out.WriteLineAsync($"Response Received : {response.Message.Text}");
            }
        }
    }
}

