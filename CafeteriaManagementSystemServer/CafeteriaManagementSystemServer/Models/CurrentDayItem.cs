using System;
using System.Collections.Generic;

namespace CafeteriaManagementSystemServer.Models
{
    public partial class CurrentDayItem
    {
        public int Id { get; set; }
        public int MenuIdForBreakfast { get; set; }
        public int MenuIdForLunch { get; set; }
        public int MenuIdForDinner { get; set; }
        public DateTime Date { get; set; }

        public virtual FoodItem MenuIdForBreakfastNavigation { get; set; } = null!;
        public virtual FoodItem MenuIdForDinnerNavigation { get; set; } = null!;
        public virtual FoodItem MenuIdForLunchNavigation { get; set; } = null!;
    }
}
