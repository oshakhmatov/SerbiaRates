namespace IntegrationTests;

public class BasicTests : IClassFixture<WebAppFactory>
{
    private readonly WebAppFactory webAppFactory;

    public BasicTests(WebAppFactory webAppFactory)
    {
        this.webAppFactory = webAppFactory;
    }

    [Theory]
    [InlineData("/rates")]
    [InlineData("/charts")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        var client = webAppFactory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
    }
}
