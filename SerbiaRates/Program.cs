using Microsoft.EntityFrameworkCore;
using SerbiaRates.Controllers;
using SerbiaRates.Data;
using SerbiaRates.Data.Repos;
using SerbiaRates.Data.Repos.Abstractions;
using SerbiaRates.Handlers.GetCharts;
using SerbiaRates.Handlers.GetRates;
using SerbiaRates.Services;
using SerbiaRates.Services.RatesUpdater;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddEnvironmentVariables()
    .AddUserSecrets<ApiController>();

// Repos
builder.Services.AddScoped<IRatesRepo, RatesRepo>();

// Handlers
builder.Services.AddScoped<GetRatesHandler>();
builder.Services.AddScoped<GetChartsHandler>();

// HostedServices
builder.Services.AddHostedService<RatesUpdater>();

builder.Services
    .AddDbContext<AppDbContext>(options => options
        .UseNpgsql(builder.Configuration["DbRates"]));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonDateConverter());
    });


var app = builder.Build();

await app.MigrateAndSeedDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x =>
{
	x.AllowAnyMethod();
	x.AllowAnyHeader();
    x.WithOrigins("http://localhost:3000", "http://158.160.36.14", "http://rs-rates.xyz");  
});

app.MapControllers();

app.Run();
