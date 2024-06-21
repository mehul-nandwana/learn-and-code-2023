using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Services
{
    public class NotificationService
    {
        NotificationRepository _notification = new NotificationRepository();
        public const string SUCCESS_NOTIFICATION_STATUS = "success";
        public const string FAILURE_NOTIFICATION_STATUS = "FAIL_TO_GET_NOTIFICATION";


        public void AddNotification(string notificationType, string notificationMessage )
        {
            Notification notification = SetNotification(notificationType, notificationMessage);
         _notification.AddNotification(notification);
        }
        public Response GetAllNotication()
        {
            Response response;
            try
            {
                List<string> notificationMessage = new List<string>();
                List<Notification> notifications = _notification.GetAllNotifications();
                foreach ( Notification notification in notifications )
                {
                    notificationMessage.Add(notification.NotificationMessage);
                }
                 response = new Response(Constant.SUCCESS_MESSAGE, Constant.SUCCESS_STATUS, SUCCESS_NOTIFICATION_STATUS, notificationMessage, "seenotification");
                return response;
            }
            catch(Exception ex)
            {
                response = new Response(ex.Message, Constant.FAILURE_STATUS, FAILURE_NOTIFICATION_STATUS, "", "employee");
                return response;
            }
        }
        public Notification SetNotification(string notificationType, string notificaitonMessage)
        {
            Notification notification = new Notification();
            notification.NotificationType = notificationType;
            notification.NotificationMessage = notificaitonMessage;
            notification.Date =DateTime.Now;
            return notification;
        }
    }
}
