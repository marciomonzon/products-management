using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductsManagement.Application.Persistence;
using ProductsManagement.Infrastructure.Data;
using ProductsManagement.Infrastructure.Persistence;

namespace ProductsManagement.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("DefaultConnection");
            });

            return services;
        }
    }
}
