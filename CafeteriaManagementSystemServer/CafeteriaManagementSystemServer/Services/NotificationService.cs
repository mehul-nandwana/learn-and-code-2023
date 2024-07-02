using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Services
{
    public class NotificationService:INotificationService
    {
        NotificationRepository notificationrepository = new NotificationRepository();
        FoodRepository foodrepository = new FoodRepository();
        public void AddNotification(string notificationType, string notificationMessage )
        {
            Notification notification = SetNotification(notificationType, notificationMessage);
            notificationrepository.AddNotification(notification);
        }

        public Response GetAllNotication()
        {
            Response response;
            try
            {
                List<string> notificationMessage = new List<string>();
                List<Notification> notifications = notificationrepository.GetAllNotifications();
                foreach ( Notification notification in notifications )
                {
                    notificationMessage.Add(notification.NotificationMessage);
                }
                response = new Response(Constant.SUCCESS_MESSAGE, notificationMessage, "seenotification");
                return response;
            }
            catch(Exception ex)
            {
                response = new Response(ex.Message, Constant.EMPTY_STRING, "employee");
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

        public Response AddDetailFeedbackNotification(CustomProtocolParameters requestData)
        {
            Notification notification = new Notification();
            notification.NotificationType = "ADDED_DISCARD_ITEM";
            notification.NotificationMessage = "Some Item are added to Improve the food";
            notification.Date = DateTime.Now;
            notificationrepository.AddNotification(notification);
            Response response = new Response(Constant.SUCCESS_MESSAGE, Constant.EMPTY_STRING, "user");
            JsonElement json = (JsonElement)requestData.Obj;
            foodrepository.CollectFeedbackOnDiscard(json.GetInt32());
            return response;
        }
    }
}
