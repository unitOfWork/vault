using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Vault.Services.OnBoarding.Application.Common.Behaviors;

namespace Vault.Services.OnBoarding.Application;

/// <summary>
/// Registers the use cases: MediatR handlers, their validators, the validation pipeline and the mapping profiles.
/// </summary>
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationServiceExtensions).Assembly;

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
            // Behaviors : Middlewares for MediatR requests.
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
        services.AddAutoMapper(_ => { }, assembly);

        return services;
    }
}
