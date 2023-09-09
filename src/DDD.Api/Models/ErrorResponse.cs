namespace DDD.Api.Models;

public class ErrorResponse
{
    public string Title { get; set; } = string.Empty;
    public string Error { get; set; } = string.Empty;
    public string Detail { get; set; } = string.Empty;
    public string Instance { get; set; } = string.Empty;
}
