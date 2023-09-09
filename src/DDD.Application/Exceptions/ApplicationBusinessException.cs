using System.Net;

namespace DDD.Application.Exceptions;

[Serializable]
public abstract class ApplicationBusinessException : Exception
{
    public HttpStatusCode HttpStatus { get; protected set; }

    public ApplicationBusinessException(string message) : base(message)
    {

    }

    public ApplicationBusinessException(string message, Exception ex) : base(message, ex)
    {

    }
}
