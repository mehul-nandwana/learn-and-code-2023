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
        private JSonSerializer _jSonSerializer = new JSonSerializer();
        private FeedbackService _feedbackService = new FeedbackService();

        public override CustomProtocolParameters CallMethod(CustomProtocolParameters serializedRequest)
        {
            try
            {           
                string method = serializedRequest.Method;
                string parameter = serializedRequest.Obj.ToString();

                if (method == "addfeedback")
                {
                    FeedBackData feedback = _jSonSerializer.DeserializeObject<FeedBackData>(parameter);
                    return _feedbackService.AddFeedback(feedback);
                }
                else if (method == "getfeedback")
                {
                    return _feedbackService.GetFeedback();
                }
                else if(method == "adddetailfeedback")
                {
                    DetailFeedbackData detailFeedbackData = _jSonSerializer.DeserializeObject<DetailFeedbackData>(parameter);
                    return _feedbackService.AddDetailFeedback(detailFeedbackData);

                }
                else
                {
                    throw new ControllerNotfound();
                }
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
    }
}
