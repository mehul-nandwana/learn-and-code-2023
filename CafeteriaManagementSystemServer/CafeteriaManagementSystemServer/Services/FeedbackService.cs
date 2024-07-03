using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using CafeteriaRecomendationEngineClient.DTO;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CafeteriaManagementSystemServer.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly FeedbackRepository _feedbackRepository;
        private readonly FoodRepository _foodRepository;

        public FeedbackService()
        {
            _feedbackRepository = new FeedbackRepository();
            _foodRepository = new FoodRepository();
        }

        public Response AddFeedback(FeedBackData feedbackData)
        {
            Feedback feedback = SetFeedbackParameters(feedbackData);
            try
            {
                string status = _feedbackRepository.AddFeedback(feedback);
                return new Response(status, feedback.UserId, Constant.EMPLOYEE_LOGIN);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, feedback.UserId, Constant.USER_LOGIN);
            }
        }

        private Feedback SetFeedbackParameters(FeedBackData feedbackData)
        {
            var feedback = new Feedback
            {
                Sentimentscore = GetSentimentScore(feedbackData.Comment).Result,
                Rating = feedbackData.Rating,
                Comment = feedbackData.Comment,
                MenuId = feedbackData.MenuId,
                UserId = feedbackData.Id,
                Feedbackdate = DateTime.Now,
                MealId = _foodRepository.GetMealTypeId(feedbackData.MenuId)
            };
            return feedback;
        }

        public Response GetFeedback()
        {
            try
            {
                List<Feedback> feedbacks = _feedbackRepository.GetFeedbacks();
                return new Response(Constant.SUCCESSFULLY_GET_FEEDBACK, feedbacks, Constant.SHOW_fEEDBACK);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, Constant.EMPTY_STRING, Constant.USER_LOGIN);
            }
        }

        public Response AddDetailFeedback(DetailFeedbackData detailFeedbackData)
        {
            DetailFeedback detailFeedback = SetDetailFeedback(detailFeedbackData);
            _feedbackRepository.AddDetailFeedback(detailFeedback);
            return new Response(Constant.SUCCESSFULLY_SET_FEEDBACK, detailFeedbackData.UserId, Constant.EMPLOYEE_LOGIN);
        }

        private DetailFeedback SetDetailFeedback(DetailFeedbackData detailFeedbackData)
        {
            FoodItem food = _foodRepository.GetFood(detailFeedbackData.FoodItemName);
            return new DetailFeedback
            {
                FeedBack = detailFeedbackData.FoodItemFeedback,
                MenuId = food.Id
            };
        }

        private async Task<int> GetSentimentScore(string Comment)
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

            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<SentimentResult>(body);
            return result.sentiment;
        }
    }
}
