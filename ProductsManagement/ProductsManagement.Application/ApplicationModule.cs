using Microsoft.Extensions.DependencyInjection;
using ProductsManagement.Application.UseCases.ProductUseCase.Interfaces;
using ProductsManagement.Application.UseCases.ProductUseCase.Validations;

namespace ProductsManagement.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductUseCase, ProductUseCase>();


            return services;
        }
    }
}
