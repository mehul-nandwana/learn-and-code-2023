using System;
using System.Collections.Generic;

namespace CafeteriaManagementSystemServer.Models
{
    public partial class Notification
    {
        public Notification()
        {
            UserNotifications = new HashSet<UserNotification>();
        }

        public int Id { get; set; }
        public string NotificationType { get; set; } = null!;
        public string NotificationMessage { get; set; } = null!;
        public DateTime Date { get; set; }

        public virtual ICollection<UserNotification> UserNotifications { get; set; }
    }
}
