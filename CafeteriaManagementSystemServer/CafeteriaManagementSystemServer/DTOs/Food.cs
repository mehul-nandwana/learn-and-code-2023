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
    }
}
