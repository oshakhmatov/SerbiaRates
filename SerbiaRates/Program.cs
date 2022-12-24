using Microsoft.EntityFrameworkCore;
using SerbiaRates.Data;
using SerbiaRates.Data.Repos;
using SerbiaRates.Handlers;
using SerbiaRates.HostedServices;
using SerbiaRates.Services.Helpers;
using SerbiaRates.Services.HttpService;
using SerbiaRates.Services.ParserCreator;
using SerbiaRates.Services.RateBuilder;
using SerbiaRates.Services.RatesUpdater;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>();

// Repos
builder.Services.AddScoped<IRepo, Repo>();

// Services
builder.Services.AddHttpClient();
builder.Services.AddScoped<IRatesUpdater, RatesUpdater>();
builder.Services.AddScoped<IWebProvider, WebProvider>();


// Handlers
builder.Services.AddScoped<GetRatesHandler>();
builder.Services.AddScoped<GetChartsHandler>();

// Singletons
builder.Services.AddSingleton<IParserCreator, ParserCreator>();
builder.Services.AddScoped<IRateBuilder, RateBuilder>();

// HostedServices
builder.Services.AddHostedService<RatesUpdaterHostedService>();

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

public partial class Program { }