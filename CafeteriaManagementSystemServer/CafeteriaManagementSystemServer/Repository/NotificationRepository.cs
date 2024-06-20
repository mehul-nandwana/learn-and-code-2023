using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeteriaManagementSystemServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeteriaManagementSystemServer.Repository
{
    public class NotificationRepository
    {
        
        public CafeteriaMangagementSystemContext _DbContext;
        public NotificationRepository() {
            _DbContext = new CafeteriaMangagementSystemContext();
        }

        public void AddNotification(Notification notification)
        {
            _DbContext.Notifications.Add(notification);

        }
        public List<Notification> GetAllNotifications()
        {
            return _DbContext.Notifications.ToList();
        }

    }
}
