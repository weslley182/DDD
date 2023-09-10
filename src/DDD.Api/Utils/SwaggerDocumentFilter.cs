using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DDD.Api.Utils;
public class SwaggerDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var path = new OpenApiPaths();
        foreach (var item in swaggerDoc.Paths)
        {
            path.Add(item.Key.Replace("v{version}", swaggerDoc.Info.Version), item.Value);
        }
        swaggerDoc.Paths = path;
    }
}