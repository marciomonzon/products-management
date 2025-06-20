using Microsoft.Extensions.DependencyInjection;

namespace ProductsManagement.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            //services.AddDbContext<AppDbContext>(options =>
            //{
            //    options.UseSqlServer("SuaConnectionStringAqui");
            //});

            return services;
        }
    }
}
