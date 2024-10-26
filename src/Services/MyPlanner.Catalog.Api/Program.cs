using MyCatalog.Shared.Exceptions.Handlers;
using MyCatalog.Shared.Extensions;
using MyCatalog.Shared.Mediator.Behaviors;
using MyPlanner.Catalog.API.Data;


var builder = WebApplication.CreateBuilder(args);

var databseConnectionString = builder.Configuration.GetConnectionString("Database")!;

builder.AddServicesDefaults();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddNpgSql(databseConnectionString);

builder.Services.AddCarter();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
});

builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Services.AddExceptionHandler<ApiCustomExceptionHandler>();

builder.Services.AddMarten(options =>
{
    options.Connection(databseConnectionString);
}).UseLightweightSessions().InitializeWith<CatalogInitialData>();


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(opt => {});

app.MapCarter();

//TODO: Merge use of HealthChecks and HealthChecksUI into Common DefaultServiceExtensions
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
}); 

app.Run();
