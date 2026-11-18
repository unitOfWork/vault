namespace Vault.Services.OnBoarding.Domain.Common;

/// <summary>
/// Outcome of an operation that can fail because of a user's request; a failure carries an <see cref="Common.Error"/>.
/// </summary>
public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        // A success carries Error.None and a failure carries a real error; anything else is a bug.
        if (isSuccess == (error != Error.None))
        {
            throw new ArgumentException(isSuccess ? "A success cannot carry an error." : "A failure needs an error.", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
    public static Result<T> Success<T>(T value) => new(value, true, Error.None);
    public static Result<T> Failure<T>(Error error) => new(default, false, error);
}

/// <summary>
/// A <see cref="Result"/> that carries a value on success.
/// </summary>
public sealed class Result<T> : Result
{
    private readonly T? _value;

    internal Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    public T Value => IsSuccess ? _value! : throw new InvalidOperationException("A failed result has no value.");
}
