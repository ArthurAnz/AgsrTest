namespace AgsrTest.Api.Domain.Exceptions.Abstract;

public abstract class AbstractNotFoundError : Exception
{
    protected AbstractNotFoundError(string message) : base(message)
    {

    }
}
