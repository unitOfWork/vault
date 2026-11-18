namespace Vault.Services.OnBoarding.Domain.Common;

/// <summary>
/// A failure a user's request can cause, identified by a stable code.
/// </summary>
public record Error(string Code, string Message, ErrorType Type)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Validation);

    public static Error Validation(string code, string message) => new(code, message, ErrorType.Validation);
    public static Error NotFound(string code, string message) => new(code, message, ErrorType.NotFound);
    public static Error Conflict(string code, string message) => new(code, message, ErrorType.Conflict);
    public static Error Unauthorized(string code, string message) => new(code, message, ErrorType.Unauthorized);
    public static Error Forbidden(string code, string message) => new(code, message, ErrorType.Forbidden);
    public static Error Unavailable(string code, string message) => new(code, message, ErrorType.Unavailable);
}

/// <summary>
/// Category of an <see cref="Error"/>, so callers can react to it without parsing the code.
/// </summary>
public enum ErrorType { Validation, NotFound, Conflict, Forbidden, Unavailable, Unauthorized }
