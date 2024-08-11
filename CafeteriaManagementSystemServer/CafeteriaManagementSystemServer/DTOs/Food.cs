using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.DTOs
{
    public class Food
    {
        public string Name { get; set; } 
        public int Price { get; set; }
        public bool Availability { get; set; }
        public int MealTypeId { get; set; }
        public bool Sweet { get; set; }
        public string CuisineType { get; set; }
        public string DietaryPreference { get; set; }
        public int Spicelevel { get; set; }
    }
}
