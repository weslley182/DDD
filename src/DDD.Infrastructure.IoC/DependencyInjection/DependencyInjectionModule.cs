using DDD.Application.Mappings;
using DDD.Application.Services;
using DDD.Application.Services.Interfaces;
using DDD.Application.UseCase;
using DDD.Application.UseCase.Interface;
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
        services.AddScoped<IProductUseCase, ProductUseCase>();

        //Services
        services.AddSingleton<IEmailService, EmailService>();

        //libs
        services.AddAutoMapper(typeof(DomainMapperProfile));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
    }
}
