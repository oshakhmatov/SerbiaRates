using IntegrationTests.Fixtures;
using SerbiaRates.Data;
using SerbiaRates.Models;
using SerbiaRates.Services.Helpers;

namespace IntegrationTests.RepoTests;

public sealed class GetAverageRatesTests : RepoTestBase
{
    public GetAverageRatesTests(DbFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task ShouldReturnAverageRatesInCorrectOrder()
    {
        // Arrange
        var rate1 = new AverageRate
        {
            Euro = 321,
            Date = Date.Today(),
            Dollar = 123,
        };
        var rate2 = new AverageRate
        {
            Euro = 321,
            Date = Date.Today(-1),
            Dollar = 123,
        };
        var rate3 = new AverageRate
        {
            Euro = 321,
            Date = Date.Today(-2),
            Dollar = 123,
        };

        var expected = new[] { rate2, rate1 };

        dbContext.AddRange(rate1, rate2, rate3);
        await dbContext.SaveChangesAsync();

        var cut = new Repo(dbContext);

        // Act
        var actual = await cut.GetAverageRates(take: 2, CancellationToken.None);

        // Assert
        Assert.Equivalent(expected, actual);
    }
}
