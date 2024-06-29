using System;
using System.Collections.Generic;

namespace CafeteriaManagementSystemServer.Models
{
    public partial class FoodItem
    {
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int Id { get; set; }
        public int MealType { get; set; }
    }
}
