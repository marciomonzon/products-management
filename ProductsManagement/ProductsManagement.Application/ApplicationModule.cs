using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductsManagement.Application.UseCases.ProductUseCase.Dtos.Request;
using ProductsManagement.Application.UseCases.ProductUseCase.Interfaces;
using ProductsManagement.Application.UseCases.ProductUseCase.Validations;
using ProductsManagement.Application.UseCases.ProductUseCase.Validations.InputValidations;

namespace ProductsManagement.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductUseCase, ProductUseCase>();

            services.AddScoped<IValidator<CreateProductRequest>, CreateProductRequestValidator>();
            services.AddScoped<IValidator<UpdateProductRequest>, UpdateProductRequestValidator>();

            return services;
        }
    }
}
