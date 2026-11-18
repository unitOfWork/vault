namespace Vault.Services.OnBoarding.Domain.Common;

/// <summary>
/// A validation failure with the error messages of each invalid field, keyed by field name.
/// </summary>
public sealed record ValidationError(IReadOnlyDictionary<string, string[]> Errors)
    : Error("Validation.Failed", "One or more validation errors occurred.", ErrorType.Validation);
