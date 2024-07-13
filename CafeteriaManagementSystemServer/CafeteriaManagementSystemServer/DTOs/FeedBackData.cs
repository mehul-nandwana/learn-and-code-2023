using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.DTOs
{
    public class FeedBackData
    {
        public string FoodName { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; } = null!;
        public int Id { get; set; }

    }
}
