using Vault.Services.OnBoarding.Domain.Customer.Enums;
namespace Vault.Services.OnBoarding.Application.Features.Customers.Models;

public sealed record CustomerProfileDto(Guid Id, Guid UserId, string FirstName, string LastName, DateOnly DateOfBirth, KycState KycStatus, DateTimeOffset? VerifiedAt);
