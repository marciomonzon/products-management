namespace ProductsManagement.Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }

        public Product()
        {
        }

        public void Create(string name,
                           string description,
                           decimal price,
                           int categoryId,
                           int stock)
        {
            Name = name.Trim();
            Description = description.Trim();
            Price = price;
            CategoryId = categoryId;
            Stock = stock;
        }

        public void Update(string name,
                           string description,
                           decimal price,
                           int categoryId,
                           int stock)
        {
            Name = name.Trim();
            Description = description.Trim();
            Price = price;
            CategoryId = categoryId;
            Stock = stock;
        }
    }
}
