using Vault.Services.OnBoarding.Api.Endpoints;
using Vault.Services.OnBoarding.Api.Middleware;

namespace Vault.Services.OnBoarding.Api;

/// <summary>
/// Registers the HTTP layer: OpenAPI, ProblemDetails (with traceId) and the global exception handler.
/// Configures the HTTP pipeline: exception handling, OpenAPI document and endpoints.
/// </summary>
public static class ApiServiceExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddProblemDetails(options => options.CustomizeProblemDetails = ProblemDetailsMapping.AddTraceId);
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        app.UseExceptionHandler();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.MapCustomerEndpoints();

        return app;
    }
}
