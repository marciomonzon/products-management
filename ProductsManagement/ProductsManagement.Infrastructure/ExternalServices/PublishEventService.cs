using ProductsManagement.Application.Services.ExternalServices;
using ProductsManagement.Domain.DomainEvents;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ProductsManagement.Infrastructure.ExternalServices
{
    public class PublishEventService : IPublishEventService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private const string Exchange = "product-service";

        public PublishEventService()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = connectionFactory.CreateConnection("product-service-publisher");
            _channel = _connection.CreateModel();
        }

        public void PostEventProductCreated(int productId)
        {
            var productCreated = new ProductCreatedEvent(productId);

            var payload = JsonSerializer.Serialize(productCreated);
            var byteArray = Encoding.UTF8.GetBytes(payload);

            Console.WriteLine("ProductCreatedEvent Published");

            _channel.BasicPublish(Exchange, "product-created", null, byteArray);
        }
    }
}
