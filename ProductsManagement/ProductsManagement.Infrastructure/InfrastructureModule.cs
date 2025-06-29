﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsManagement.Application.Persistence;
using ProductsManagement.Application.Services.ExternalServices;
using ProductsManagement.Infrastructure.Data;
using ProductsManagement.Infrastructure.ExternalServices;
using ProductsManagement.Infrastructure.Persistence;

namespace ProductsManagement.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPublishEventService, PublishEventService>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}