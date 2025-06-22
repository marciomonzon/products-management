namespace ProductsManagement.Domain.DomainEvents
{
    public class ProductCreatedEvent
    {
        public int ProductId { get; }
        public string Name { get; } = string.Empty;
        public string Description { get; } = string.Empty;
        public DateTime CreatedAt { get; }

        public ProductCreatedEvent(int productId,
                                   string name,
                                   string description)
        {
            ProductId = productId;
            CreatedAt = DateTime.UtcNow;
            Name = name.Trim();
            Description = description.Trim();
        }
    }
}
