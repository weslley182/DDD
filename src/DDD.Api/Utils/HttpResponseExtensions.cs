namespace DDD.Api.Utils;

public static class HttpResponseExtensions
{
    public static bool IsInformational(this HttpResponse httpResponse)
    {
        return httpResponse.StatusCode >= 100 && httpResponse.StatusCode < 199;
    }
    public static bool IsSuccessfull(this HttpResponse httpResponse)
    {
        return httpResponse.StatusCode >= 200 && httpResponse.StatusCode < 299;
    }
    public static bool IsRedirection(this HttpResponse httpResponse)
    {
        return httpResponse.StatusCode >= 300 && httpResponse.StatusCode < 399;
    }
    public static bool IsClientError(this HttpResponse httpResponse)
    {
        return httpResponse.StatusCode >= 400 && httpResponse.StatusCode < 499;
    }
    public static bool IsServerError(this HttpResponse httpResponse)
    {
        return httpResponse.StatusCode >= 500 && httpResponse.StatusCode < 599;
    }
}
