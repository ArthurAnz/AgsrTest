using AgsrTest.Api.Domain.Exceptions.Abstract;

namespace AgsrTest.Api.Domain.Exceptions;

public class EntityNotFoundException : AbstractNotFoundError
{
    public EntityNotFoundException(string message) : base(message)
    {

    }
}
