using FluentValidation;

namespace Vault.Services.OnBoarding.Application.Features.Customers.Commands.CreateCustomerProfile;

internal sealed class CreateCustomerProfileValidator : AbstractValidator<CreateCustomerProfileCommand>
{
    private const int MinimumAge = 18;
    private const int NameMaxLength = 100;

    public CreateCustomerProfileValidator()
    {
        RuleFor(command => command.UserId)
            .NotEmpty();

        RuleFor(command => command.FirstName)
            .NotEmpty()
            .MaximumLength(NameMaxLength);

        RuleFor(command => command.LastName)
            .NotEmpty()
            .MaximumLength(NameMaxLength);

        RuleFor(command => command.DateOfBirth)
            .Must(dateOfBirth => dateOfBirth <= DateOnly.FromDateTime(DateTime.UtcNow).AddYears(-MinimumAge))
            .WithMessage($"Customer must be at least {MinimumAge} years old.");
    }
}
