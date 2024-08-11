using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using CafeteriaRecomendationEngineClient.DTO;
using System;

namespace CafeteriaManagementSystemServer.Controller
{
    public class FeedbackController : ICommonController
    {
        private readonly JSonSerializer _jsonSerializer;
        private readonly FeedbackService _feedbackService;

        public FeedbackController()
        {
            _jsonSerializer = new JSonSerializer();
            _feedbackService = new FeedbackService();
        }

        public override CustomProtocolParameters CallMethod(CustomProtocolParameters serializedRequest)
        {
            try
            {
                string requestedMethod = serializedRequest.Method;
                string requestData = serializedRequest.Obj.ToString();

                return requestedMethod switch
                {
                    Constant.ADD_FEEDBACK => AddFeedback(requestData),
                    Constant.GET_FEEDBACK => _feedbackService.GetFeedback(),
                    Constant.ADD_DETAIL_FEEDBACK => AddDetailFeedbackForDiscardedItem(requestData),
                    _ => throw new ControllerNotfound(),
                };
            }
            catch (ControllerNotfound ex)
            {
                Console.WriteLine($"Controller not found: {ex.Message}");
                throw;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Argument null error: {ex.Message}");
                throw;
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Operation not supported: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        private CustomProtocolParameters AddFeedback(string foodItemFeedback)
        {
            var feedback = _jsonSerializer.DeserializeObject<FeedBackData>(foodItemFeedback);
            return _feedbackService.AddFeedback(feedback);
        }

        private CustomProtocolParameters AddDetailFeedbackForDiscardedItem(string discardedFoodItemFeedback)
        {
            var detailFeedbackData = _jsonSerializer.DeserializeObject<DetailFeedbackData>(discardedFoodItemFeedback);
            return _feedbackService.AddDetailFeedback(detailFeedbackData);
        }
    }
}
