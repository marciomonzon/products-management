namespace ProductsManagement.Domain.Entities
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public ICollection<Product> Products { get; set; } = new List<Product>();

        public void CreateNewCategory(string name)
        {
            Name = name.Trim();
        }
    }
}
