using System.Reflection;
using System.Text.Json;
using Vault.Services.OnBoarding.Domain.Common;

namespace Vault.Services.OnBoarding.Application.Common.Behaviors;

/// <summary>
/// ToFailure() extension method for converting an Error to a Result.
/// ToCamelCase() extension method for turning a FluentValidation property path into the key clients see, matching their JSON field names.
/// </summary>

internal static class BehaviorExtensions
{
    public static TResponse ToFailure<TResponse>(this Error error) where TResponse : Result
    {
        if (typeof(TResponse) == typeof(Result))
        {
            return (TResponse)Result.Failure(error);
        }

        if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            // The generic Result.Failure<T> can only be reached through reflection here.
            return (TResponse)typeof(Result)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Single(method => method.Name == nameof(Result.Failure) && method.IsGenericMethodDefinition)
                .MakeGenericMethod(typeof(TResponse).GetGenericArguments()[0])
                .Invoke(null, [error])!;
        }

        throw new InvalidOperationException($"Pipeline behaviors support Result and Result<T>, not {typeof(TResponse).Name}.");
    }

    public static string ToCamelCase(this string propertyName) =>
        string.Join('.', propertyName.Split('.').Select(JsonNamingPolicy.CamelCase.ConvertName));
}
