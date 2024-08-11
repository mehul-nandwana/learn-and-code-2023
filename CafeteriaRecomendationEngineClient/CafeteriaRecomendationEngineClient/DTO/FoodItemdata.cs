using System;
using System.Collections.Generic;

namespace CafeteriaRecomendationEngineClient.DTO
{
    public partial class FoodItemData
    {
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public int Id { get; set; }
        public double AverageRating { get; set; }
        public string AverageSentiment { get; set; }

    }
}
