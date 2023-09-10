using DDD.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Infrastructure.IoC.Config;

public static class DataBase
{
    public static void AddDataBase(this IServiceCollection services)
    {

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        string connection = Environment.GetEnvironmentVariable("MYSQL_HOST");
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

#pragma warning disable CS8604 // Possible null reference argument.
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL(connection),
            ServiceLifetime.Singleton
        );
#pragma warning restore CS8604 // Possible null reference argument.
    }
}
