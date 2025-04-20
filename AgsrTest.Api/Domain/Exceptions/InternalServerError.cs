using AgsrTest.Api.Domain.Exceptions.Abstract;
using System.Net;

namespace AgsrTest.Api.Domain.Exceptions;

public class InternalServerError : Error
{
    public InternalServerError(string? message = null, string? detail = null)
    {
        Code = HttpStatusCode.InternalServerError;
        Message = message;
        Detail = detail;
    }
}
