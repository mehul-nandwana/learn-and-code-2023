using System;
using System.Collections.Generic;

namespace CafeteriaManagementSystemServer.Models
{
    public partial class Recommendation
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int FoodId { get; set; }
        public int MealTypeId { get; set; }
        public bool IsPrepared { get; set; }

        public virtual MealType MealType { get; set; } = null!;
    }
}
