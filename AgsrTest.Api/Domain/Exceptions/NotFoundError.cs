using System.Net;
using AgsrTest.Api.Domain.Exceptions.Abstract;

namespace AgsrTest.Api.Domain.Exceptions;

public class NotFoundError : Error
{
    public NotFoundError(string? message = null, string? detail = null)
    {
        Code = HttpStatusCode.NotFound;
        Message = message;
        Detail = detail;
    }
}
