using ProductsManagement.Application.Persistence.Base;
using ProductsManagement.Domain.Entities;

namespace ProductsManagement.Application.Persistence
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
    }
}
