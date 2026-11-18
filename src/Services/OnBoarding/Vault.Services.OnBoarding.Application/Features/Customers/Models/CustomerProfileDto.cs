using Vault.Services.OnBoarding.Domain.Customer.Enums;
namespace Vault.Services.OnBoarding.Application.Features.Customers.Models;

/// <summary>
/// Read model of a customer profile returned to callers.
/// </summary>
public sealed record CustomerProfileDto(Guid Id, Guid UserId, string FirstName, string LastName, DateOnly DateOfBirth, KycState KycStatus, DateTimeOffset? VerifiedAt);
