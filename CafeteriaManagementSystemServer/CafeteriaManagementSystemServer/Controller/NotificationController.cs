using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Controller
{
    public class NotificationController
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
    }
}
