using SerbiaRates.ViewModels;
using System.Net.Http.Json;

namespace IntegrationTests.ApiTests;

public sealed class GetRatesTests : ApiTestBase
{
    [Fact]
    public async Task ShouldReturnCorrectResult()
    {
        // Arrange
        var expected = new RatesViewModel()
        {
            Date = Date.Today().ToString("dd.MM.yyyy"),
            AverageDollar = 543,
            AverageEuro = 345,
            Rates = new[]
            {
                new CompanyRatesDto()
                {
                    CompanyName = "Test company 1",
                    Dollar = new RateDto()
                    {
                        Buy = 1,
                        Sell = 2
                    },
                    Euro = new RateDto()
                    {
                        Buy = 3,
                        Sell = 4
                    }
                },
                new CompanyRatesDto()
                {
                    CompanyName = "Test company 2",
                    Dollar = new RateDto()
                    {
                        Buy = 2,
                        Sell = 3
                    },
                    Euro = new RateDto()
                    {
                        Buy = 4,
                        Sell = 5
                    }
                }
            }
        };

        // Act
        var actual = await client.GetFromJsonAsync<RatesViewModel>("/rates");

        // Assert
        Assert.Equivalent(expected, actual);
    }
}
