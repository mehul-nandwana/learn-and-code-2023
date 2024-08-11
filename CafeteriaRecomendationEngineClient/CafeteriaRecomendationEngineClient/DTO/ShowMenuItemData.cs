using CafeteriaRecomendationEngineClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.DTOs
{
    public class ShowMenuItemData
    {
        public List<FoodItemData> FoodItem { get; set; }
        public int UserId { get; set; }
    }
}
