using MediatR;
using Vault.Services.OnBoarding.Api.Models;

namespace Vault.Services.OnBoarding.Api.Endpoints;

internal static class CustomerEndpoints
{
    internal static void MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/customers").WithTags("Customers");

        group.MapPost("/", async (CreateCustomerProfileRequest request, HttpRequest http, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new CreateCustomerProfileCommand(request.UserId, request.FirstName, request.LastName, request.DateOfBirth), ct);
            return result.IsFailure ? result.Error.ToProblem() : TypedResults.Created($"{http.Path}/{result.Value.Id}", result.Value);
        })
        .WithName("CreateCustomerProfile")
        .WithSummary("Create a customer profile")
        .WithDescription("Admin only (403). 409 if the customer is already registered. (no 404 since we can't verify existence of customers)")
        .Produces<CustomerProfileDto>(StatusCodes.Status201Created)
        .ProducesValidationProblem() // Status 400
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .ProducesProblem(StatusCodes.Status403Forbidden)
        .ProducesProblem(StatusCodes.Status409Conflict);
    }
}
