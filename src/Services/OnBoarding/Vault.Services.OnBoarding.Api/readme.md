-  **Minimal APIs (IEndpointRouteBuilder)** : Instantiation of RouteGroupBuilder and registration of endpoints to the group, can support OpenAPI methods.
    1. MapGroup(string prefix) : Give prefix to endpoints, instanciates a RouteGroupBuilder : IEndpointRouteBuilder
    2. RequireAuthorization(policies): Adds authorization with the given policies to the endpoint
    3. Map<Method>(string endpoint, Delegate handler) : Adds endpoint to the RouteGroupBuilder with an HTTP method
    4. WithTags(str), WithName(str), WithSummary(str), WithDescription(str), Produces<T>(int) : OpenApi Specs
- **GlobalExceptionHandler (IProblemDetailsService)** : Interception of the Exception, creation of httpcontext and return new ProblemDetailsContext
    1. Creation of Httpcontext using the exception's data and status code
    2. IProblemDetailsService.TryWriteAsync to create and return ProblemDetailsContext using the HttpContext	
- **Program.cs (WebApplication)** : Entry point of application, Instanciation of a builder, instantiate its services and build the application
    1. Initialization of host web env : WebApplication.CreateBuilder(), to instanciate builder
    2. DI Container configuration : Builder registers the Services(Clean Architecture Layers Extensions, ProblemDetails, OpenAPI ..)
    3. Build the app : Builder builds the application and instanciates the WebApplication.
    4. Middleware pipeline config : App.UseAuthorization, App.UseSwagger() ...
    5. Execute the app : app.run(), starts **Kestrel**
- **appsettings.json** : Environment variables (configuration) of the application
- **launchsettings.json** : Configuration of app launch (url, env, ..)
