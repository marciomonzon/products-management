namespace ProductsManagement.Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public decimal Price { get; private set; }

        public int CategoryId { get; private set; }
        public Category Category { get; set; } = new Category();

        public void CreateNewProduct(string name, decimal price, int categoryId)
        {
            Name = name.Trim();
            Price = price;
            CategoryId = categoryId;
        }
    }
}
