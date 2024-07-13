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
            Response response = new Response(Constant.SUCCESS_MESSAGE, recommendedItem, "showrecommendation");
            return response;
        }
    }
}
