using System;
using System.Collections.Generic;

namespace CafeteriaManagementSystemServer.Models
{
    public partial class FoodItem
    {
        public FoodItem()
        {
            CurrentDayItemMenuIdForBreakfastNavigations = new HashSet<CurrentDayItem>();
            CurrentDayItemMenuIdForDinnerNavigations = new HashSet<CurrentDayItem>();
            CurrentDayItemMenuIdForLunchNavigations = new HashSet<CurrentDayItem>();
            Feedbacks = new HashSet<Feedback>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public bool Availability { get; set; }
        public int MealTypeId { get; set; }

        public virtual ICollection<CurrentDayItem> CurrentDayItemMenuIdForBreakfastNavigations { get; set; }
        public virtual ICollection<CurrentDayItem> CurrentDayItemMenuIdForDinnerNavigations { get; set; }
        public virtual ICollection<CurrentDayItem> CurrentDayItemMenuIdForLunchNavigations { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
