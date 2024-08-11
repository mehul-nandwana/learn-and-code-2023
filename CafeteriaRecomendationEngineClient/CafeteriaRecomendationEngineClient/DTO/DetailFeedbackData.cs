using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaRecomendationEngineClient.DTO
{
    public class DetailFeedbackData
    {
        public string FoodItemName { get; set; }
        public string FoodItemFeedback { get; set; }
        public int UserId { get; set; }
    }
}
