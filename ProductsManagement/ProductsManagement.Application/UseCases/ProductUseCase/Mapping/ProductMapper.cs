using ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Request;
using ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Response;
using ProductsManagement.Domain.Entities;

namespace ProductsManagement.Application.UseCases.ProductUseCase.Mapping
{
    public static class ProductMapper
    {
        public static Product ToEntity(CreateProductRequest request)
        {
            var entity = new Product();
            entity.Create(request.Name,
                          request.Description,
                          request.Price,
                          request.CategoryId);
            return entity;
        }

        public static void MapUpdate(Product product, UpdateProductRequest request)
        {
            product.Update(request.Name,
                          request.Description,
                          request.Price,
                          request.CategoryId);
        }

        public static ProductResponse ToResponse(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? string.Empty
            };
        }
    }
}
