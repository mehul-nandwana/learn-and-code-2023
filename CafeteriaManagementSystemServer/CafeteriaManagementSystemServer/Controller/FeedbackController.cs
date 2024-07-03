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
                string parameter = serializedRequest.Obj.ToString();

                return requestedMethod switch
                {
                    Constant.ADD_FEEDBACK => AddFeedback(parameter),
                    Constant.GET_FEEDBACK => _feedbackService.GetFeedback(),
                    Constant.ADD_DETAIL_FEEDBACK => AddDetailFeedback(parameter),
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

        private CustomProtocolParameters AddFeedback(string parameter)
        {
            var feedback = _jsonSerializer.DeserializeObject<FeedBackData>(parameter);
            return _feedbackService.AddFeedback(feedback);
        }

        private CustomProtocolParameters AddDetailFeedback(string parameter)
        {
            var detailFeedbackData = _jsonSerializer.DeserializeObject<DetailFeedbackData>(parameter);
            return _feedbackService.AddDetailFeedback(detailFeedbackData);
        }
    }
}
