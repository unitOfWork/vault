using Vault.Services.OnBoarding.Api;
using Vault.Services.OnBoarding.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi()
    .AddApplication();

var app = builder.Build();

app.UseApi();

app.Run();
