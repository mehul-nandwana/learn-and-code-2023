using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.DTOs
{
    public class DiscardItemData
    {
        public List<string> FoodItem { get; set; }
        public int UserId { get; set; }
    }
}
