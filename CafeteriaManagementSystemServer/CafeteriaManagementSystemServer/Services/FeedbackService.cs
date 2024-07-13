using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using CafeteriaManagementSystemServer.ServiceHelper;
using CafeteriaRecomendationEngineClient.DTO;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CafeteriaManagementSystemServer.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly FeedbackRepository _feedbackRepository;
        private readonly FoodRepository _foodRepository;
        private readonly RecommendationEngine _recommendationEngine;

        public FeedbackService()
        {
            _feedbackRepository = new FeedbackRepository();
            _foodRepository = new FoodRepository();
            _recommendationEngine = new RecommendationEngine();
        }

        public Response AddFeedback(FeedBackData feedbackData)
        {
            try
            {
                Feedback feedback = SetFeedbackParameters(feedbackData);
                string addFeedbackStatus = _feedbackRepository.AddFeedback(feedback);
                return new Response(addFeedbackStatus, feedback.UserId, Constant.EMPLOYEE_LOGIN);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, feedbackData.Id, Constant.EMPLOYEE_LOGIN);
            }
        }

        private Feedback SetFeedbackParameters(FeedBackData feedbackData)
        {
            FoodItem food = _foodRepository.GetFood(feedbackData.FoodName);
            var feedback = new Feedback
            {
                Sentimentscore = _recommendationEngine.GetSentimentScore(feedbackData.Comment).Result,
                Rating = feedbackData.Rating,
                Comment = feedbackData.Comment,
                MenuId = food.Id,
                UserId = feedbackData.Id,
                Feedbackdate = DateTime.Now,
                MealId = _foodRepository.GetMealTypeId(food.Id),
            };
            return feedback;
        }

        public Response GetFeedback()
        {
            try
            {
                List<Feedback> feedback = _feedbackRepository.GetFeedbacks();
                return new Response(Constant.SUCCESSFULLY_GET_FEEDBACK, feedback, Constant.SHOW_fEEDBACK);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, Constant.EMPTY_STRING, Constant.CHEF_LOGIN);
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
    }
}
