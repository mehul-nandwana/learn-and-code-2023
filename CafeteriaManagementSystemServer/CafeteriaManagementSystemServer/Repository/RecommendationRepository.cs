using CafeteriaManagementSystemServer.Models;
using CafeteriaRecomendationEngineClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Repository
{

    public class RecommendationRepository
    {
        public CafeteriaMangagementSystemContext _DbContext;
        List<Feedback> feedbacks = new List<Feedback>
        {
            new Feedback { Id = 1, UserId = 1, MenuId = 101, MealId = 1, Rating = 4.5, Feedbackdate = DateTime.Now, Comment = "Great", Sentimentscore = 8 },
            new Feedback { Id = 2, UserId = 2, MenuId = 101, MealId = 1, Rating = 4.0, Feedbackdate = DateTime.Now, Comment = "Good", Sentimentscore = 7 },
            new Feedback { Id = 3, UserId = 3, MenuId = 102, MealId = 1, Rating = 3.5, Feedbackdate = DateTime.Now, Comment = "Okay", Sentimentscore = 6 },
            new Feedback { Id = 4, UserId = 4, MenuId = 102, MealId = 2, Rating = 3.0, Feedbackdate = DateTime.Now, Comment = "Average", Sentimentscore = 5 },
            new Feedback { Id = 5, UserId = 5, MenuId = 103, MealId = 2, Rating = 5.0, Feedbackdate = DateTime.Now, Comment = "Excellent", Sentimentscore = 9 },
            new Feedback { Id = 6, UserId = 6, MenuId = 101, MealId = 1, Rating = 4.5, Feedbackdate = DateTime.Now, Comment = "Nice", Sentimentscore = 8 }
        };
        public RecommendationRepository()
        {
            _DbContext = new CafeteriaMangagementSystemContext();
        }
        public void getRecommendation(RecommendatioData data)
        {
            var topDishes = feedbacks
            .Where(f => f.MealId == data.MealTypeId) // Filter by MealId
            .GroupBy(f => f.MenuId)
            .Select(g => new
            {
                MenuId = g.Key,
                AverageRating = g.Average(f => f.Rating),
                AverageSentimentScore = g.Average(f => f.Sentimentscore)
            })
            .OrderByDescending(g => g.AverageRating)
            .ThenByDescending(g => g.AverageSentimentScore)
            .Take(data.NumberOfDishes)
            .ToList();
            List < Feedback > dish = topDishes;

        }
    }
}
