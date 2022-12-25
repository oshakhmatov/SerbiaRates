using SerbiaRates.ViewModels;
using System.Net.Http.Json;

namespace IntegrationTests.ApiTests;

public sealed class GetChartsTests : ApiTestBase
{
    [Fact]
    public async Task ShouldReturnCorrectResult()
    {
        // Arrange
        var expected = new ChartsViewModel()
        {
            Points = new []
            {
                new Point() 
                {
                    Date = Date.Today(-2).ToString("MMMM dd"),
                    USD = 123,
                    EUR = 321
                },
                new Point()
                {
                    Date = Date.Today(-1).ToString("MMMM dd"),
                    USD = 432,
                    EUR = 234
                },
                new Point()
                {
                    Date = Date.Today().ToString("MMMM dd"),
                    USD = 543,
                    EUR = 345
                },
            }
        };

        // Act
        var actual = await client.GetFromJsonAsync<ChartsViewModel>("/charts");

        // Assert
        Assert.Equivalent(expected, actual);
    }
}
