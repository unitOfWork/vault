using Vault.Services.OnBoarding.Application;
using Vault.Services.OnBoarding.Api.Middlewares;
using Vault.Services.OnBoarding.Infrastructure;

// 1. INITIALIZE THE HOST ENVIRONMENT
var builder = WebApplication.CreateBuilder(args);

// 2. CONFIGURE SERVICES (DEPENDENCY INJECTION CONTAINER)
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure("OnBoardingConnection");
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

// 3. BUILD THE APPLICATION EXECUTABLE
var app = builder.Build();

// 4. CONFIGURE THE MIDDLEWARE PIPELINE
app.UseExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// 5. EXECUTE THE PROCESS
app.Run();
