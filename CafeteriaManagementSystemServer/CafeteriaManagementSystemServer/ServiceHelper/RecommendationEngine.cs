using System.Net.Http.Headers;
using CafeteriaManagementSystemServer.Models;

namespace CafeteriaManagementSystemServer.ServiceHelper
{
    public class RecommendationEngine
    {
        public async Task<int> GetSentimentScore(string Comment)
        {
            string sentiment = await GetSentiment(Comment);
            return sentiment.ToLower() switch
            {
                "positive" => 1,
                "negative" => -1,
                _ => 0
            };
        }

        private static async Task<string> GetSentiment(string sentiment)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://text-sentiment-analyzer-api1.p.rapidapi.com/sentiment"),
                Headers =
                {
                    { "x-rapidapi-key", "e69dfed3b7mshd7478c248a82524p182c5ajsnaec2bf08ef77" },
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

            JSonSerializer _jsonSerializer = new JSonSerializer();
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var result = _jsonSerializer.DeserializeObject<SentimentResult>(body);
            return result.sentiment;
        }
    }
}
