using AgsrTest.Api.Domain.Exceptions.Abstract;
using System.Net;

namespace AgsrTest.Api.Domain.Exceptions;

public class BadRequestError : Error
{
    public BadRequestError(string? message = null, string? detail = null)
    {
        Code = HttpStatusCode.BadRequest;
        Message = message;
        Detail = detail;
    }
}
