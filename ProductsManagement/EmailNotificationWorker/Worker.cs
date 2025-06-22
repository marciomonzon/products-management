using EmailNotificationWorker.Events;
using EmailNotificationWorker.Models;
using EmailNotificationWorker.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace EmailNotificationWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string Queue = "notifications-service/product-created";
        private SendEmailService _sendEmailService;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = connectionFactory.CreateConnection("notifications-service-product-created-consumer");
            _channel = _connection.CreateModel();

            _sendEmailService = new SendEmailService();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += async (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var message = JsonSerializer.Deserialize<ProductCreatedEvent>(contentString);

                Console.WriteLine($"Message ProductCreatedEvent received with Product Id {message?.ProductId}, " +
                                  $"Name {message?.Name} " +
                                  $"and Description {message?.Description}");

                _channel.BasicAck(eventArgs.DeliveryTag, false);

                await _sendEmailService.SendEmailAsync(new EmailMessage
                {
                    Body = $"New Product Created: {message?.ProductId}, Name: {message?.Name}"
                });
            };

            _channel.BasicConsume(Queue, false, consumer);
        }
    }
}
