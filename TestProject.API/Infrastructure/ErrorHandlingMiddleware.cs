using Newtonsoft.Json;
using System.Net;
using TestProject.API.Extension;
using TestProject.Core.Infrastructure.Exceptions;

namespace TestProject.API.Infrastructure;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleException(context, e);
        }
    }

    private async Task HandleException(HttpContext context, Exception e)
    {
        if (e is AggregateException && e.InnerException != null)
        {
            e = e.InnerException;
        }

        switch (e)
        {
            case NotFoundException nfe:
                await WriteResponse(HttpStatusCode.NotFound, context);
                break;
            case InvalidArgumentException ie:
                await WriteResponse(HttpStatusCode.BadRequest, context, ie.ErrorData ?? ie.Message);
                break;
            default:
                _logger.LogError(e, "HTTP {RequestMethod} {RequestPath}", context.Request.Method, context.GetPath());
                await WriteResponse(HttpStatusCode.InternalServerError, context, "Server error");
                break;
        }
    }

    private static async Task WriteResponse(HttpStatusCode statusCode, HttpContext context, object data = null)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        if (data != null)
        {
            await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
        }
    }
}
