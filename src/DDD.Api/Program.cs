using DDD.Api.Utils;
using DDD.Infrastructure.IoC.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDependencyInjection();

builder.Services.AddApiVersioning(cfg =>
{
    cfg.DefaultApiVersion = new ApiVersion(1, 0);
    cfg.AssumeDefaultVersionWhenUnspecified = true;
    cfg.ReportApiVersions = true;
    cfg.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new() { Title = "DDD.Api Template", Version = "v1" });
        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
        c.CustomSchemaIds(x => x.FullName);
        c.DocumentFilter<SwaggerDocumentFilter>();
        c.OperationFilter<SwaggerOperationFilter>();
        c.DescribeAllParametersInCamelCase();
    }
);


var app = builder.Build();

app.UseMiddleware<RequestMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint($"{"v1"}/swagger.json", $"Template API V1");
    c.ShowExtensions();
});

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

public partial class Program { }