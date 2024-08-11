using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CafeteriaManagementSystemServer.ServiceHelper;
using Moq;
using Moq.Protected;
using Xunit;

public class RecommendationEngineTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly RecommendationEngine _recommendationEngine;

    public RecommendationEngineTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object);
        _recommendationEngine = new RecommendationEngine(_httpClient);
    }

    [Theory]
    [InlineData("This is amazing!", "positive", 1)]
    [InlineData("This is terrible!", "negative", -1)]
    [InlineData("It's okay.", "neutral", 0)]
    public async Task GetSentimentScore_ShouldReturnCorrectScoreBasedOnSentiment(string comment, string apiSentiment, int expectedScore)
    {
        // Arrange
        var mockResponse = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent($"{{ \"sentiment\": \"{apiSentiment}\" }}")
        };

        _mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(mockResponse);

        // Act
        var result = await _recommendationEngine.GetSentimentScore(comment);

        // Assert
        Assert.Equal(expectedScore, result);
    }
}
