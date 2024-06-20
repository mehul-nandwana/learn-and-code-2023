using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer
{


     class RecommendationEngine
    {
        public void Getsentiment(string comment)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://twinword-sentiment-analysis.p.rapidapi.com/analyze/"),
                Headers =
            {
                { "x-rapidapi-key", "e69dfed3b7mshd7478c248a82524p182c5ajsnaec2bf08ef77" },
                { "x-rapidapi-host", "twinword-sentiment-analysis.p.rapidapi.com" },
            },
            Content = new MultipartFormDataContent
            {
            new StringContent(comment)
            {
                Headers =
                {
                    ContentDisposition = new ContentDispositionHeaderValue("form-data")
                    {
                        Name = "text",
                    }
                }
            },
        },
                };
                using (var response =  client.Send(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body =  response.Content.ReadAsStringAsync();
                    Console.WriteLine(body);
                }
            }
    }
}
