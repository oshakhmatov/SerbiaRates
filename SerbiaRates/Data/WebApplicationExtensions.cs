using Microsoft.EntityFrameworkCore;
using SerbiaRates.Domain;
using SerbiaRates.Modules.ExchangeRates;
using SerbiaRates.Modules.Shared;

namespace SerbiaRates.Data;

public static class WebApplicationExtensions
{
    public static async Task MigrateAndSeedDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await dbContext.Database.MigrateAsync();

        if (!await dbContext.Currencies.AnyAsync())
        {
            dbContext.AddRange(new[]
            {
                new Currency 
                { 
                    Id = Const.EuroId, 
                    Code = "EUR",
                    Name = "Euro"
                },
                new Currency 
                { 
                    Id = Const.DollarId, 
                    Code = "USD", 
                    Name = "United States Dollar"
                }
            });

            await dbContext.SaveChangesAsync();
        }

        if (!await dbContext.Companies.AnyAsync())
        {
            dbContext.AddRange(new[]
            {
                new Company
                {
                    Id = Const.GagaId,
                    Name = "Gaga",
                    Url = "https://menjacnicegaga.rs/"
				},
                new Company
                {
                    Id = Const.PostanskaId, 
                    Name = "Postal Savings Bank",
                    Url = "https://www.posted.co.rs/testang/phpremote/jsonall.php"
				},
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
