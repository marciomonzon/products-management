using UpdateProductStatusWorker.Enums;

namespace UpdateProductStatusWorker.Models
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public int Stock { get; private set; }
        public ProductStatus Status { get; set; }
        public int CategoryId { get; private set; }

        public void SetProductStatus()
        {
            Status = Stock switch
            {
                < 0 => ProductStatus.NegativeStock,
                0 => ProductStatus.OutOfStock,
                > 0 => ProductStatus.Active
            };
        }
    }
}
