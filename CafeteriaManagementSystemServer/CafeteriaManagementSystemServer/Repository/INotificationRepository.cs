using CafeteriaManagementSystemServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Repository
{
    public interface INotificationRepository
    {
        public void AddNotification(Notification notification);
        public List<Notification> GetAllNotifications();
    }
}
