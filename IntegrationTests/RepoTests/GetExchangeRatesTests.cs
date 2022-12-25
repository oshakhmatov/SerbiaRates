using IntegrationTests.Fixtures;
using SerbiaRates.Data;
using SerbiaRates.Models;

namespace IntegrationTests.RepoTests;

public class GetExchangeRatesTests : RepoTestBase
{
    public GetExchangeRatesTests(DbFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldReturnCorrectResultForTwoCompanies()
    {
        // Arrange
        var company1 = new Company()
        {
            Id = 1,
            Name = "Test 1",
            Url = "test",
        };
        var company2 = new Company()
        {
            Id = 2,
            Name = "Test 2",
            Url = "test",
        };
        var exchangeRate1 = new ExchangeRate()
        {
            Company = company1,
            CompanyId = company1.Id,
            Date = Date.Today(),
            DollarBuy = 1,
            DollarSell = 2,
            EuroBuy = 3,
            EuroSell = 4
        };
        
        var exchangeRate2 = new ExchangeRate()
        {
            Company = company2,
            CompanyId = company2.Id,
            Date = Date.Today(),
            DollarBuy = 1,
            DollarSell = 2,
            EuroBuy = 3,
            EuroSell = 4
        };
        var exchangeRate3 = new ExchangeRate()
        {
            Company = company1,
            CompanyId = company1.Id,
            Date = Date.Today(-1),
            DollarBuy = 1,
            DollarSell = 2,
            EuroBuy = 3,
            EuroSell = 4
        };

        var exchangeRate4 = new ExchangeRate()
        {
            Company = company2,
            CompanyId = company2.Id,
            Date = Date.Today(-1),
            DollarBuy = 1,
            DollarSell = 2,
            EuroBuy = 3,
            EuroSell = 4
        };

        var expected = new[] { exchangeRate2, exchangeRate1 };

        dbContext.AddRange(
            company1, 
            company2,
            exchangeRate1,
            exchangeRate2,
            exchangeRate3,
            exchangeRate4);

        await dbContext.SaveChangesAsync();

        var cut = new Repo(dbContext);

        // Act
        var actual = await cut.GetExchangeRates(CancellationToken.None);

        // Assert
        Assert.Equivalent(expected, actual);
    }
}
