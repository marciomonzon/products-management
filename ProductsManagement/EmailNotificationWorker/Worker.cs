using EmailNotificationWorker.Events;
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

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = connectionFactory.CreateConnection("notifications-service-product-created-consumer");
            _channel = _connection.CreateModel();
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
            };

            _channel.BasicConsume(Queue, false, consumer);
        }
    }
}
