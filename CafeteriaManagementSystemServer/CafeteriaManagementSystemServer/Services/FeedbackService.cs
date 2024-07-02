using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using CafeteriaRecomendationEngineClient.DTO;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CafeteriaManagementSystemServer.Services
{
    public class FeedbackService: IFeedbackService
    {
        FeedbackRepository feedbackRepository = new FeedbackRepository();
        FoodRepository foodRepository =new FoodRepository();
        public Response AddFeedback(FeedBackData feedbackData)
        {
            Feedback feedback = SetFeedbackParameters(feedbackData);
            try
            {
               string status = feedbackRepository.AddFeedback(feedback);
                Response response = new Response(status, feedback.UserId, Constant.EMPLOYEE_LOGIN);
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message, feedback.UserId, Constant.USER_LOGIN);
                return response;
            }
        }

   
        public Feedback SetFeedbackParameters(FeedBackData feedbackData)
        {
            SentimentResult result;
            Feedback feedback = new Feedback();
            string comment = GetSentiment().Result;
            if (comment.ToLower() == "positive")
                feedback.Sentimentscore = 1;
            else if (comment.ToLower() == "negative")
                feedback.Sentimentscore = -1;
            else
                feedback.Sentimentscore = 0;
            feedback.Rating = feedbackData.Rating;
            feedback.Comment = feedbackData.Comment;
            feedback.MenuId = feedbackData.MenuId;
            feedback.UserId = feedbackData.Id;
            feedback.Feedbackdate = DateTime.Now;
            feedback.MealId = 1;
            return feedback;
        }

        public Response GetFeedback( )
        {
            try
            {
                List<Feedback> feedbacks = feedbackRepository.GetFeedbacks();
                Response response = new Response(Constant.SUCCESSFULLY_GET_FEEDBACK, feedbacks, Constant.SHOW_fEEDBACK);
                return response;
            }
            catch (Exception ex)
            {
                Response response = new Response(ex.Message, Constant.EMPTY_STRING, Constant.USER_LOGIN);
                return response;
            }
        }

        public Response AddDetailFeedback(DetailFeedbackData detailFeedbackData)
        {
            DetailFeedback detailFeedback = setDetailFeedback(detailFeedbackData);
            feedbackRepository.AddDetailFeedback(detailFeedback);
            Response response = new Response(Constant.SUCCESSFULLY_SET_FEEDBACK, detailFeedbackData.UserId, Constant.EMPLOYEE_LOGIN);
            return response;
        }

        private DetailFeedback setDetailFeedback(DetailFeedbackData detailFeedbackData)
        {
            DetailFeedback detailFeedback = new DetailFeedback();
            detailFeedback.FeedBack = detailFeedbackData.FoodItemFeedback;
            FoodItem food = foodRepository.GetFood(detailFeedbackData.FoodItemName);
            detailFeedback.MenuId = food.Id;
            return detailFeedback;
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

            using (var CustomProtocolParameters = await client.SendAsync(request))
            {
                CustomProtocolParameters.EnsureSuccessStatusCode();
                var body = await CustomProtocolParameters.Content.ReadAsStringAsync();
                 result = JsonSerializer.Deserialize<SentimentResult>(body);
            }
            return result.sentiment;
        }
    }
}
