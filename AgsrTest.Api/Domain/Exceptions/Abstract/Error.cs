using System.Net;

namespace AgsrTest.Api.Domain.Exceptions.Abstract;

public abstract class Error
{
    public HttpStatusCode Code { get; set; }
    public string? Message { get; set; }
    public string? Detail { get; set; }
}
