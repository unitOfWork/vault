using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Vault.Services.OnBoarding.Domain.Common;

namespace Vault.Services.OnBoarding.Application.Common.Behaviors;

/// <summary>
/// MediatR middleware : Runs every validator before the execution of the handler
/// </summary>
internal sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        if (!validators.Any())
        {
            return await next(ct);
        }

        var failures = new List<ValidationFailure>();
        foreach (var validator in validators)
        {
            var result = await validator.ValidateAsync(request, ct);
            failures.AddRange(result.Errors);
        }

        var errors = failures
            .GroupBy(failure => failure.PropertyName.ToCamelCase())
            .ToDictionary(group => group.Key, group => group.Select(failure => failure.ErrorMessage).ToArray());

        return errors.Count == 0 ? await next(ct) : new ValidationError(errors).ToFailure<TResponse>();
    }
}
