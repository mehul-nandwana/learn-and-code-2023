using System;
using System.Collections.Generic;

namespace CafeteriaManagementSystemServer.Models
{
    public partial class DetailFeedback
    {
        public int Id { get; set; }
        public string FeedBack { get; set; } = null!;
        public int MenuId { get; set; }

        public virtual FoodItem Menu { get; set; } = null!;
    }
}
