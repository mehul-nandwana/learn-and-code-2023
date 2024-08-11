using System;
using System.Collections.Generic;

namespace CafeteriaManagementSystemServer.Models
{
    public partial class FoodItemData
    {
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int Id { get; set; }
        public int MealType { get; set; }
        public string AverageSentiment { get; set; }
        public double AverageRating { get; set; }

    }
}
