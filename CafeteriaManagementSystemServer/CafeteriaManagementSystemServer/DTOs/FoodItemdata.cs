using System;
using System.Collections.Generic;

namespace CafeteriaManagementSystemServer.Models
{
    public partial class FoodItemData
    {

        public string Name { get; set; }
        public decimal Price { get; set; }
       
        public int Id {  get; set; }
        public int MealType {  get; set; }

    }
}
