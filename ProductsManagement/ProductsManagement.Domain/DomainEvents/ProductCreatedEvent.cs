namespace ProductsManagement.Domain.DomainEvents
{
    public class ProductCreatedEvent
    {
        public int ProductId { get; }
        public DateTime CreatedAt { get; }

        public ProductCreatedEvent(int productId)
        {
            ProductId = productId;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
