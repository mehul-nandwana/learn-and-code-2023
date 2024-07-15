using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Interfaces;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using System.Text.Json;
using Constant = CafeteriaManagementSystemServer.Models.Constant;

namespace CafeteriaManagementSystemServer.Services
{
    public class NotificationService : INotificationService
    {
        NotificationRepository _notificationrepository = new NotificationRepository();
        FoodRepository _foodrepository = new FoodRepository();

        public void AddNotification(string notificationType, string notificationMessage)
        {
            Notification notification = SetNotification(notificationType, notificationMessage);
            _notificationrepository.AddNotification(notification);
        }

        public Response GetAllNotification(CustomProtocolParameters requestData)
        {
            Response response;
            try
            {
                string requestObj = requestData.Obj.ToString();
                int userId = Convert.ToInt32(requestObj);
                List<string> notificationMessage = new List<string>();
                DataItem notificationData = new DataItem();
                List<Notification> notifications = _notificationrepository.GetAllNotifications();
                foreach (Notification notification in notifications)
                {
                    notificationMessage.Add(notification.NotificationMessage);
                }
                notificationData.Message = notificationMessage;
                notificationData.userId = userId;
                response = new Response(Constant.SUCCESS_MESSAGE, notificationData, Constant.SEE_NOTIFICATION);
                return response;
            }
            catch (Exception ex)
            {
                response = new Response(ex.Message, Constant.EMPTY_STRING, Constant.EMPLOYEE_LOGIN);
                return response;
            }
        }

        public Notification SetNotification(string notificationType, string notificaitonMessage)
        {
            Notification notification = new Notification();
            notification.NotificationType = notificationType;
            notification.NotificationMessage = notificaitonMessage;
            notification.Date = DateTime.Now;
            return notification;
        }

        public Response AddDetailFeedbackNotification(CustomProtocolParameters requestData)
        {
            Notification notification = new Notification();
            notification.NotificationType = "ADDED_DISCARD_ITEM";
            notification.NotificationMessage = "Some Item are added to Improve the food";
            notification.Date = DateTime.Now;
            _notificationrepository.AddNotification(notification);
            Response response = new Response(Constant.SUCCESS_MESSAGE, Constant.EMPTY_STRING, Constant.LOGIN);
            JsonElement json = (JsonElement)requestData.Obj;
            _foodrepository.CollectFeedbackOnDiscard(json.GetInt32());
            return response;
        }
    }
}