using Microsoft.EntityFrameworkCore;
using SerbiaRates.Data;
using SerbiaRates.Data.Abstractions;
using SerbiaRates.Models;

namespace IntegrationTests.Mocks;

public sealed class MockDbInitializer : IDbInitializer
{
    private readonly AppDbContext dbContext;

    public MockDbInitializer(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task MigrateAndSeedDatabase()
    {
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        dbContext.AddRange(
            new Company()
            {
                Id = 1,
                Name = "Test company 1",
                Url = "test url 1"
            },
            new Company()
            {
                Id = 2,
                Name = "Test company 2",
                Url = "test url 2"
            },
            new AverageRate()
            {
                Date = Date.Today(-2),
                Dollar = 123,
                Euro = 321
            },
            new AverageRate()
            {
                Date = Date.Today(-1),
                Dollar = 432,
                Euro = 234
            },
            new AverageRate()
            {
                Date = Date.Today(),
                Dollar = 543,
                Euro = 345
            },
            new ExchangeRate()
            {
                CompanyId = 1,
                Date = Date.Today(),
                DollarBuy = 1,
                DollarSell = 2,
                EuroBuy = 3,
                EuroSell = 4
            },
            new ExchangeRate()
            {
                CompanyId = 2,
                Date = Date.Today(),
                DollarBuy = 2,
                DollarSell = 3,
                EuroBuy = 4,
                EuroSell = 5
            });

        await dbContext.SaveChangesAsync();
    }
}
