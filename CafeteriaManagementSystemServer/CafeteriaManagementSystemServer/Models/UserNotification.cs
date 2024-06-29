using System;
using System.Collections.Generic;

namespace CafeteriaManagementSystemServer.Models
{
    public partial class UserNotification
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public int NotificationId { get; set; }

        public virtual Notification Notification { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
