using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Services
{
    public class FeedbackService
    {
        FeedbackRepository _feedbackRepository = new FeedbackRepository();
        public Response AddFeedback(FeedBackData feedbackData)
        {
            try
            {

                Feedback feedback = SetFeedback(feedbackData);
                _feedbackRepository.AddFeedback(feedback);
                Response response = new Response(Constant.SUCCESS_MESSAGE, Constant.SUCCESS_STATUS,Constant.SUCCESSFULLY_ADDED_FEEDBACK , feedbackData, "employee");
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(Constant.SUCCESS_MESSAGE, Constant.SUCCESS_STATUS, Constant.FAILURE_ADDED_FEEDBACK, feedbackData, "employee");
                return response;
            }
        }
        public Response GetFeedback( )
        {
            try
            {
                List<Feedback> feedbacks = _feedbackRepository.GetFeedbacks();
                Response response = new Response(Constant.SUCCESS_MESSAGE, Constant.SUCCESS_STATUS, Constant.SUCCESSFULLY_GET_FEEDBACK, "", "employee");
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(Constant.SUCCESS_MESSAGE, Constant.SUCCESS_STATUS, Constant.FAILURE_GET_FEEDBACK, "", "employee");
                return response;
            }
        }

        public Feedback SetFeedback(FeedBackData feedbackData)
        {
            SentimentResult result;
            Feedback feedback = new Feedback();
            string comment = GetSentiment().Result;
            if (comment.ToLower() == "Positive")
                feedback.Sentimentscore = 1;
            else if (comment.ToLower() == "negative")
                feedback.Sentimentscore = -1;
            else
                feedback.Sentimentscore = 0;

            //feedback.Feedbackdate = DateTime.Now.AddDays(1);
            feedback.Rating = feedbackData.Rating;
            feedback.Comment = feedbackData.Comment;
            feedback.MenuId = feedbackData.MenuId;
            return feedback;
        }

        public static async Task<string> GetSentiment()
        {
            SentimentResult result;
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://text-sentiment-analyzer-api1.p.rapidapi.com/sentiment"),
                Headers =
                {
                    { "x-rapidapi-key", "e69dfed3b7mshd7478c248a82524p182c5ajsnaec2bf08ef77" },
                    { "x-rapidapi-host", "text-sentiment-analyzer-api1.p.rapidapi.com" },
                },
                Content = new MultipartFormDataContent
            {
                new StringContent("I've been using this API for some time now. I must say that its performance its excellent. I will recommend this tool")
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
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                 result = JsonSerializer.Deserialize<SentimentResult>(body);
            }
            return result.sentiment;

        }
    }
}
