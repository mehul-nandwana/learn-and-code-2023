using CafeteriaManagementSystemServer.Interfaces;
using CafeteriaManagementSystemServer.Models;
using CafeteriaRecomendationEngineClient;

namespace CafeteriaManagementSystemServer.Repository
{

    public class RecommendationRepository: IRecommendationRepository
    {
        public CafeteriaMangagementSystemContext _DbContext;
        public readonly FoodRepository _foodRepository;
        public RecommendationRepository()
        {
            _DbContext = new CafeteriaMangagementSystemContext();
            _foodRepository = new FoodRepository();
        }

        public Dictionary<int, string> getRecommendation(RecommendatioData data)
        {
            Dictionary<int, string> food = new Dictionary<int, string>();
            var topDishes = _DbContext.Feedbacks
            .Where(f => f.MealId == data.MealTypeId) 
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
            foreach (var item in topDishes)
            {
                FoodItem foodItemdata = _foodRepository.GetFood(item.MenuId);
                food.Add(item.MenuId, foodItemdata.Name);
            }
            return food;
        }
    }
}
