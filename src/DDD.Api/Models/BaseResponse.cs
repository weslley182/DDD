namespace DDD.Api.Models;

public class BaseResponse
{
    public BaseResponse() : this(200)
    {
    }

    public BaseResponse(int statusCode)
    {
        Status = statusCode;
    }

    public long Status { get; set; }

    public string[]? Errors { get; set; }
}
