using Microsoft.EntityFrameworkCore;
using SerbiaRates.Data;
using SerbiaRates.Data.Repos;
using SerbiaRates.Modules.ExchangeRates;
using SerbiaRates.Modules.ExchangeRates.RatesUpdater;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IExchangeRateRepo, ExchangeRateRepo>();

builder.Services.AddHostedService<RatesUpdater>();

builder.Services
    .AddDbContext<AppDbContext>(options => options
        .UseNpgsql(builder.Configuration
            .GetConnectionString("Default")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await app.MigrateAndSeedDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
