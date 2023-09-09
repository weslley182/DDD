using DDD.Api.Models;
using System.Diagnostics;

namespace DDD.Api.Utils;

public class RequestMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    private readonly Stopwatch _timer;
    private readonly LogData _logData;
    private static List<string> _ignoreLogsByPath = new List<string> { "swagger" };


    public RequestMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next;
        _logger = loggerFactory.CreateLogger<RequestMiddleware>();
        _logData = new LogData();
        _timer = new Stopwatch();
    }

    public async Task Invoke(HttpContext context)
    {
        _timer.Start();
        await FormatRequest(context);

        var originalBody = context.Response.Body;
        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;
            await _next(context);
            await FormatResponse(context.Response);
            await responseBody.CopyToAsync(originalBody);

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;
            if (!IsToLog(context.Request.Path))
            {
                return;
            }

            var message = "Logging FullPath: {FullPath}, Request: {RequestBody}, Response: {ResponseBody}, ElapsedMilliseconds {ElapsedMilliseconds}";

            if (context.Response.IsSuccessfull())
            {
                _logger.LogInformation(message, _logData.FullPath, _logData.RequestBody, _logData.ResponseBody, elapsedMilliseconds);
            }
            else if (context.Response.IsClientError())
            {
                _logger.LogWarning(message, _logData.FullPath, _logData.RequestBody, _logData.ResponseBody, elapsedMilliseconds);
            }
            else
            {
                _logger.LogError(message, _logData.FullPath, _logData.RequestBody, _logData.ResponseBody, elapsedMilliseconds);
            }

        }
    }

    private async Task FormatRequest(HttpContext context)
    {
        var request = context.Request;
        request.EnableBuffering();
        var body = await new StreamReader(request.Body).ReadToEndAsync();

        request.Body.Position = 0;
        _logData.FullPath = $"{request.Host} {request.Path} {request.QueryString}";
        _logData.RequestBody = body;
    }

    private async Task FormatResponse(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        string body = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);
        _logData.ResponseBody = body;
    }

    private static bool IsToLog(string path)
    {
        foreach (var item in _ignoreLogsByPath)
        {
            if (path.Contains(item))
            {
                return false;
            }
        }
        return true;
    }
}
