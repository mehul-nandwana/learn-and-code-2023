using System;
using System.Collections.Generic;

namespace CafeteriaManagementSystemServer.Models
{
    public partial class DiscardItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int FoodId { get; set; }
        public bool GetDetailFeedback { get; set; }

        public virtual FoodItem Food { get; set; } = null!;
    }
}
