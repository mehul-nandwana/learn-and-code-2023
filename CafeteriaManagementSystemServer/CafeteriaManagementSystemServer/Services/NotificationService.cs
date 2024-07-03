using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace CafeteriaManagementSystemServer.Services
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationRepository _notificationRepository;
        private readonly FoodRepository _foodRepository;

        public NotificationService()
        {
            _notificationRepository = new NotificationRepository();
            _foodRepository = new FoodRepository();
        }

        public void AddNotification(string notificationType, string notificationMessage)
        {
            var notification = CreateNotification(notificationType, notificationMessage);
            _notificationRepository.AddNotification(notification);
        }

        public Response GetAllNotification()
        {
            try
            {
                var notifications = _notificationRepository.GetAllNotifications();
                var notificationMessages = notifications.ConvertAll(n => n.NotificationMessage);

                return new Response(Constant.SUCCESS_MESSAGE, notificationMessages, Constant.SEE_NOTIFICATION);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, Constant.EMPTY_STRING, Constant.EMPLOYEE_LOGIN);
            }
        }

        public Response AddDetailFeedbackNotification(CustomProtocolParameters requestData)
        {
            try
            {
                var notification = CreateNotification(Constant.ADDED_DISCARD_ITEM, Constant.ADD_DETAIL_FEEDBACK_NOTIFICATION);
                _notificationRepository.AddNotification(notification);
                var userId = ((JsonElement)requestData.Obj).GetInt32();
                _foodRepository.CollectFeedbackOnDiscard(userId);

                return new Response(Constant.SUCCESS_MESSAGE, Constant.EMPTY_STRING, Constant.LOGIN);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, Constant.EMPTY_STRING, Constant.LOGIN);
            }
        }

        private Notification CreateNotification(string notificationType, string notificationMessage)
        {
            return new Notification
            {
                NotificationType = notificationType,
                NotificationMessage = notificationMessage,
                Date = DateTime.Now
            };
        }
    }
}
