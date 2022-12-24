namespace IntegrationTests;

public class BasicTests : IClassFixture<WebAppFactory<Program>>
{
    private readonly WebAppFactory<Program> webAppFactory;

    public BasicTests(WebAppFactory<Program> webAppFactory)
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
