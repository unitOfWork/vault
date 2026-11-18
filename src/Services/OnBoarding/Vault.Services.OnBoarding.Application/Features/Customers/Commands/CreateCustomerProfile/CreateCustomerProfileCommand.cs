using MediatR;
using Vault.Services.OnBoarding.Application.Features.Customers.Models;
using Vault.Services.OnBoarding.Domain.Common;

namespace Vault.Services.OnBoarding.Application.Features.Customers.Commands.CreateCustomerProfile;

public sealed record CreateCustomerProfileCommand(Guid UserId, string FirstName, string LastName, DateOnly DateOfBirth) : IRequest<Result<CustomerProfileDto>>;
