using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Controller
{
    public class FeedbackController:ICommonController
    {
        public FeedbackService feedbackService = new FeedbackService();
        public byte[] ExecuteRequest(CustomProtocolParameters<Object> requestData, string method)
        {

            Response response = CallMethod( method, requestData.obj.ToString());
            string Output = JsonSerializer.Serialize(response);
            byte[] responseData = Encoding.ASCII.GetBytes(Output);
            return responseData;
        }

        public Response CallMethod( string methodName, string par)
        {
            if (methodName == "addfeedback")
            {
                FeedBackData feedback = JsonSerializer.Deserialize<FeedBackData>(par);
                return feedbackService.AddFeedback(feedback);

            }
            else 
            {
                Food food = JsonSerializer.Deserialize<Food>(par);
                return feedbackService.GetFeedback();
            }

        }
    }
}
