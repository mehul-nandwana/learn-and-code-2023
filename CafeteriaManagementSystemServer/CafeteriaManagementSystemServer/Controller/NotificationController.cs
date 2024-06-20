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
    public class NotificationController:ICommonController
    {
        NotificationService _notificationService = new NotificationService();
        public void AddNotification(string notificationType, string notificationMessage)
        {
            _notificationService.AddNotification(notificationType, notificationMessage);
        }
        public Response GetAllNotification() 
        {
            return _notificationService.GetAllNotication();
        }
        public byte[] ExecuteRequest(CustomProtocolParameters<Object> requestData, string method)
        {
            Response response = _notificationService.GetAllNotication(); 
            string Output = JsonSerializer.Serialize(response);
            byte[] responseData = Encoding.ASCII.GetBytes(Output);
            return responseData;
        }
    }
}
