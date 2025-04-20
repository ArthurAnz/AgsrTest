using AgsrTest.Api.Domain.Exceptions;
using AgsrTest.Api.Domain.Exceptions.Abstract;

namespace AgsrTest.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(AbstarctBadRequestError ex)
        {
            await WriteBadRequest(context, ex);
        }
        catch(AbstractNotFoundError ex)
        {
            await WriteNotFound(context, ex);
        }
        catch(Exception ex)
        {
            await WriteUnexpected(context, ex);
        }
    }

    private static async Task WriteBadRequest(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new BadRequestError(ex.Message));
    }

    private static async Task WriteNotFound(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new NotFoundError(ex.Message));
    }

    private static async Task WriteUnexpected(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new InternalServerError("An unexpected error occurred", ex.Message));
    }
}
