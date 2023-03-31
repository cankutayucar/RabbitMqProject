using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using NetCoreApp.Web.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;

namespace NetCoreApp.Web.BackgroundServices
{
    public class ImageWatermarkProcessBackgroundService : BackgroundService
    {
        private readonly RabbitMqClientService _rabbitMqClientService;
        private readonly ILogger<ImageWatermarkProcessBackgroundService> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IModel _channel;

        public ImageWatermarkProcessBackgroundService(RabbitMqClientService rabbitMqClientService, ILogger<ImageWatermarkProcessBackgroundService> logger, IWebHostEnvironment webHostEnvironment)
        {
            _rabbitMqClientService = rabbitMqClientService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = _rabbitMqClientService.Connect();
            _channel.BasicQos(0, 1, false);
            return base.StartAsync(cancellationToken);
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {            
            var consumer = new AsyncEventingBasicConsumer(_channel);
            _channel.BasicConsume(RabbitMqClientService.QueueName, false, consumer);
            consumer.Received += Consumer_Received;
            return Task.CompletedTask;
        }

        private Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            Task.Delay(10000).Wait();
            try
            {
                var imageCreatedEvent = JsonSerializer.Deserialize<ProductImageCreatedEvent>(Encoding.UTF8.GetString(@event.Body.ToArray()));
                var siteName = "www.cankutayucar.com";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", imageCreatedEvent.ImageName);
                using var image = Image.FromFile(path);
                using var graphic = Graphics.FromImage(image);
                var font = new Font(FontFamily.GenericMonospace, 40, FontStyle.Bold, GraphicsUnit.Pixel);
                var textSize = graphic.MeasureString(siteName, font);
                var color = Color.FromArgb(128, 255, 255, 255);
                var brush = new SolidBrush(color);
                var position = new Point(image.Width - ((int)textSize.Width + 30), image.Height - ((int)textSize.Height + 30));
                graphic.DrawString(siteName, font, brush, position);
                image.Save("wwwroot/Images/watermarks/" + imageCreatedEvent.ImageName);
                image.Dispose();
                _channel.BasicAck(@event.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
    }
}
