using CafeteriaManagementSystemServer.Models;

namespace CafeteriaManagementSystemServer.Repository
{
    public class NotificationRepository:INotificationRepository
    {
        
        public CafeteriaMangagementSystemContext _DbContext;

        public NotificationRepository() 
        {
            _DbContext = new CafeteriaMangagementSystemContext();
        }

        public void AddNotification(Notification notification)
        {
            _DbContext.Notifications.Add(notification);
            _DbContext.SaveChanges();
        }

        public List<Notification> GetAllNotifications()
        {
            return _DbContext.Notifications.ToList();
        }

    }
}
