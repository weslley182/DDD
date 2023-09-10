using DDD.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Infrastructure.IoC.Config;

public static class DataBase
{
    public static void AddDataBase(this IServiceCollection services)
    {

        string connection = Environment.GetEnvironmentVariable("MYSQL_HOST");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL(connection),
            ServiceLifetime.Singleton
        );
    }
}
