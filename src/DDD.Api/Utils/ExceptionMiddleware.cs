using DDD.Api.Models;
using DDD.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace DDD.Api.Utils;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ApplicationBusinessException ex)
        {
            await HandleExceptionAsync(httpContext, ex.HttpStatus, ex, ex.InnerException);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex, ex.InnerException);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, HttpStatusCode statusCode, Exception ex, Exception? innerException)
    {
        ErrorResponse errorResponse = new ErrorResponse
        {
            Title = statusCode.ToString(),
            Error = ex.Message,
            Detail = string.Format("{Trace} {Message}", ex.StackTrace, innerException?.Message),
            Instance = httpContext.Request.Path
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)statusCode;
        var json = JsonConvert.SerializeObject(errorResponse);

        await httpContext.Response.WriteAsync(json);
    }
}
