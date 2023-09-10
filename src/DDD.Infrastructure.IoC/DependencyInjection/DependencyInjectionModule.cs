using DDD.Domain.Services.Repositories.Interfaces;
using DDD.Infrastructure.Data.Repositories;
using DDD.Infrastructure.IoC.Config;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Infrastructure.IoC.DependencyInjection;

public static class DependencyInjectionModule
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddDataBase();
        //repository
        services.AddScoped<IProductRepository, ProductRepository>();

        //useCases


    }
}
