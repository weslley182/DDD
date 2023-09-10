using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DDD.Api.Utils;
public class SwaggerOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var version = operation.Parameters.FirstOrDefault(p => p.Name == "version");
        if (version != null)
        {
            operation.Parameters.Remove(version);
        }
    }
}
