using Moq;
using SerbiaRates.Data.Repos;
using SerbiaRates.Handlers;
using SerbiaRates.Models;
using SerbiaRates.ViewModels;

namespace UnitTests.HandlerTests;

public sealed class GetChartsHandlerTests
{
    [Fact]
    public async Task ShouldReturnChartWithTwoPoints()
    {
        // Arrange
        var firstDate = new DateOnly(2022, 12, 22);
        var secondDate = new DateOnly(2022, 12, 23);

        var expected = new ChartsViewModel()
        {
            Points = new[]
            {
                new Point()
                {
                    EUR = 109,
                    USD = 108,
                    Date = firstDate.ToString("MMMM dd")
                },
                new Point()
                {
                    EUR = 111,
                    USD = 110,
                    Date = secondDate.ToString("MMMM dd")
                }
            }
        };

        var mock = new Mock<IRepo>();
        mock.Setup(x => x.GetAverageRates(It.IsAny<int>(), CancellationToken.None))
            .ReturnsAsync(new AverageRate[]
            {
                new AverageRate()
                {
                    Date = firstDate,
                    Euro = expected.Points[0].EUR,
                    Dollar = expected.Points[0].USD
                },
                new AverageRate()
                {
                    Date = secondDate,
                    Euro = expected.Points[1].EUR,
                    Dollar = expected.Points[1].USD
                }
            });

        var cut = new GetChartsHandler(mock.Object);

        // Act
        var actual = await cut.Handle(CancellationToken.None);

        // Assert
        Assert.NotNull(actual);
        Assert.Equivalent(expected, actual);
    }
}
