using CafeteriaManagementSystemServer.Models;

namespace CafeteriaManagementSystemServer.DTOs
{
    public class ShowMenuItemData
    {
        public List<FoodItemData> FoodItem { get; set; }
        public int UserId { get; set; }
    }
}
