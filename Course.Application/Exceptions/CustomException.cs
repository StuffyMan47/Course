using System.Net;

namespace Course.Application.Exceptions;

public class CustomException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}