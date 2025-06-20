using Microsoft.EntityFrameworkCore;
using ProductsManagement.Application.Persistence;
using ProductsManagement.Domain.Entities;
using ProductsManagement.Infrastructure.Data;
using ProductsManagement.Infrastructure.Persistence.Base;

namespace ProductsManagement.Infrastructure.Persistence
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        {
            var products = await _context.Products
                                         .Include(p => p.Category)
                                         .Where(p => p.CategoryId == categoryId)
                                         .ToListAsync();
            return products;
        }
    }
}
