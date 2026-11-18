namespace Vault.Services.OnBoarding.Api.Models;

public sealed record CreateCustomerProfileRequest(Guid UserId, string FirstName, string LastName, DateOnly DateOfBirth);
