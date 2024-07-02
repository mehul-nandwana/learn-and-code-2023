using System;
using System.Collections.Generic;

namespace CafeteriaManagementSystemServer.Models
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MenuId { get; set; }
        public double Rating { get; set; }
        public DateTime Feedbackdate { get; set; }
        public string Comment { get; set; } = null!;
        public int Sentimentscore { get; set; }
        public int MealId { get; set; }

        public virtual FoodItem Menu { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
