using Microsoft.AspNetCore.Diagnostics;
using Vault.Services.OnBoarding.Application.Common.Exceptions;

namespace Vault.Services.OnBoarding.Api.Middleware;

/// <summary>
/// Converts a conflict exception into a 409 ProblemDetails response, if not then it's a 500 Internal Server Error.
/// </summary>
public sealed partial class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment environment) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is OperationCanceledException && httpContext.RequestAborted.IsCancellationRequested)
        {
            LogRequestAborted(httpContext.Request.Method, httpContext.Request.Path);
            return true;
        }

        if (exception is ConflictException conflict)
        {
            LogConflict(conflict.Error.Code);
            await conflict.Error.ToProblem().ExecuteAsync(httpContext);
            return true;
        }

        LogUnhandled(exception, httpContext.Request.Method, httpContext.Request.Path);
        await Results.Problem(
            detail: environment.IsDevelopment() ? exception.Message : null,
            statusCode: StatusCodes.Status500InternalServerError,
            title: "An unexpected error occurred.").ExecuteAsync(httpContext);
        return true;
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Request {Method} {Path} was aborted by the client.")]
    private partial void LogRequestAborted(string method, PathString path);

    [LoggerMessage(Level = LogLevel.Information, Message = "A request lost a race at save time: {Code}.")]
    private partial void LogConflict(string code);

    [LoggerMessage(Level = LogLevel.Error, Message = "Unhandled exception for {Method} {Path}.")]
    private partial void LogUnhandled(Exception exception, string method, PathString path);
}
