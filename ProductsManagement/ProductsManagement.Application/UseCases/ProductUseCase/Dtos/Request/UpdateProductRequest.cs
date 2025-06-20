namespace ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Request
{
    public class UpdateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int Stock { get; set; }
    }
}
