using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using CafeteriaRecomendationEngineClient;

namespace CafeteriaManagementSystemServer.Services
{
    public class RecommendationService
    {
        RecommendationRepository recommendationRepository = new RecommendationRepository();

        public CustomProtocolParameters GetRecommendation(RecommendatioData data)
        {
            Dictionary<int,string> recommendedItem =  recommendationRepository.getRecommendation(data);
            Response response = new Response(Constant.SUCCESS_MESSAGE, recommendedItem, Constant.SHOW_RECOMMENDATION);
            return response;
        }

        public string GetSentiment(double averageSentiment)
        {
            if (averageSentiment > 0 && averageSentiment < 1)
            {
                return Constant.GOOD;
            }
            else if (averageSentiment == 1)
            {
                return Constant.EXCELLENT;
            }
            else if (averageSentiment == 0)
            {
                return Constant.NOT_BAD;
            }
            else if (averageSentiment < 0 || averageSentiment > -1)
            {
                return Constant.BAD;
            }
            else
            {
                return Constant.VERY_BAD;
            }
        }
    }
}
