using CafeteriaManagementSystemServer.Models;
using CafeteriaRecomendationEngineClient;

namespace CafeteriaManagementSystemServer.Repository
{

    public class RecommendationRepository: IRecommendationRepository
    {
        public CafeteriaMangagementSystemContext _DbContext;
     
        public RecommendationRepository()
        {
            _DbContext = new CafeteriaMangagementSystemContext();
        }

        public List<int> getRecommendation(RecommendatioData data)
        {
            List<int> menuIds = new List<int>();
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
                menuIds.Add(item.MenuId);
            }
            return menuIds;
        }
    }
}
