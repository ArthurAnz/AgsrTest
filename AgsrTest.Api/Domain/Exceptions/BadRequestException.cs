using AgsrTest.Api.Domain.Exceptions.Abstract;

namespace AgsrTest.Api.Domain.Exceptions;

public class BadRequestException : AbstarctBadRequestError
{
    public BadRequestException(string message) : base(message)
    {

    }
}
