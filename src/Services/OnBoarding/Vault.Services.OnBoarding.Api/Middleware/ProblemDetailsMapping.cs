using System.Diagnostics;
using Vault.Services.OnBoarding.Domain.Common;

namespace Vault.Services.OnBoarding.Api.Middleware;

/// <summary>
/// Maps a domain Error to an HTTP ProblemDetails response, including validation errors and traceId.
/// </summary>
public static class ProblemDetailsMapping
{
    public static IResult ToProblem(this Error error)
    {
        if (error is ValidationError validation)
        {
            return Results.ValidationProblem(
                validation.Errors,
                detail: error.Message,
                extensions: new Dictionary<string, object?> { ["code"] = error.Code });
        }

        var statusCode = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.Unavailable => StatusCodes.Status503ServiceUnavailable,
            // A new ErrorType must get an explicit status, not silently become a 500.
            _ => throw new ArgumentOutOfRangeException(nameof(error), error.Type, "Unmapped error type."),
        };

        return Results.Problem(
            detail: error.Message,
            statusCode: statusCode,
            extensions: new Dictionary<string, object?> { ["code"] = error.Code });
    }

    public static void AddTraceId(ProblemDetailsContext context) =>
        context.ProblemDetails.Extensions["traceId"] = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier;
}
