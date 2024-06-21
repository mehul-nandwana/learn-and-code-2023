using System;
using System.Collections.Generic;

namespace CafeteriaManagementSystemServer.Models
{
    public partial class User
    {
        public User()
        {
            Choices = new HashSet<Choice>();
            Feedbacks = new HashSet<Feedback>();
            UserNotifications = new HashSet<UserNotification>();
        }

        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Roleid { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Choice> Choices { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<UserNotification> UserNotifications { get; set; }
    }
}
