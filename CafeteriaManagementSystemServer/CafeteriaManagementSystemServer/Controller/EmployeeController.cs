using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Controller
{
    public class EmployeeController:ICommonController
    {
        MenuController _menuController = new MenuController();
        NotificationController _notification =new NotificationController();
        public FeedbackService feedbackService = new FeedbackService();
        public byte[] ExecuteRequest(CustomProtocolParameters<Object> requestData, string method)
        {

            Response response = CallMethod( method, requestData.obj.ToString());
            string Output = JsonSerializer.Serialize(response);
            byte[] responseData = Encoding.ASCII.GetBytes(Output);
            return responseData;
        }

        public  Response CallMethod(string methodName, string par)
        {
             if(methodName == "addchoice")
            {
                return _notification.GetAllNotification();

            }
           else if (methodName == "addfeedback")
            {
                FeedBackData feedback = JsonSerializer.Deserialize<FeedBackData>(par);
                return feedbackService.AddFeedback(feedback);

            }
            else
            {
                int id = JsonSerializer.Deserialize<int>(par);
                return _notification.GetAllNotification();

            }
        }
    }
}
