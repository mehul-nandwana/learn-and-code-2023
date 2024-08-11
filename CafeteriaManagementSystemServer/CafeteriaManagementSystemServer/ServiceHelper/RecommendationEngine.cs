using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using CafeteriaManagementSystemServer.Models;

namespace CafeteriaManagementSystemServer.ServiceHelper
{
    public class RecommendationEngine
    {
        private readonly HttpClient _httpClient;

        public RecommendationEngine(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetSentimentScore(string comment)
        {
            string sentiment = await GetSentiment(comment);
            return sentiment.ToLower() switch
            {
                "positive" => 1,
                "negative" => -1,
                _ => 0
            };
        }

        private async Task<string> GetSentiment(string sentiment)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://text-sentiment-analyzer-api1.p.rapidapi.com/sentiment"),
                Headers =
                {
                    { "x-rapidapi-key", "your-rapidapi-key" },
                    { "x-rapidapi-host", "text-sentiment-analyzer-api1.p.rapidapi.com" }
                },
                Content = new MultipartFormDataContent
                {
                    new StringContent(sentiment)
                    {
                        Headers =
                        {
                            ContentDisposition = new ContentDispositionHeaderValue("form-data")
                            {
                                Name = "text"
                            }
                        }
                    }
                }
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<SentimentResult>(body.ToString());
            return result.sentiment;
        }
    }
}
