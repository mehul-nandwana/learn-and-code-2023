using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using CafeteriaRecomendationEngineClient;
using System.Text.Json;

namespace CafeteriaManagementSystemServer.Controller
{
    public class RecommendationController : ICommonController
    {
        private readonly RecommendationService _recommendationService;
        private readonly JSonSerializer _jsonSerializer;

        public RecommendationController()
        {
            _recommendationService = new RecommendationService();
            _jsonSerializer = new JSonSerializer();

        }

        public override CustomProtocolParameters CallMethod(CustomProtocolParameters requestData)
        {
            try
            {
                string parameter = requestData.Obj.ToString();
                RecommendatioData data = _jsonSerializer.DeserializeObject<RecommendatioData>(parameter);
                return _recommendationService.GetRecommendation(data);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Deserialization error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
