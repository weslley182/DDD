using System.Net;

namespace DDD.Application.Exceptions;

[Serializable]
public abstract class ApplicationException : Exception
{
    public HttpStatusCode HttpStatus { get; protected set; }

    public ApplicationException(string message) : base(message)
    {

    }

    public ApplicationException(string message, Exception ex) : base(message, ex)
    {

    }
}
