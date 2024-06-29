using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using CafeteriaRecomendationEngineClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Controller
{
    public class RecommendationController:ICommonController
    {
        RecommendationService _recommendationService = new RecommendationService();

        public override CustomProtocolParameters CallMethod(CustomProtocolParameters requestData)
        {
            string parameter = requestData.Obj.ToString();
            RecommendatioData data = JsonSerializer.Deserialize<RecommendatioData>(parameter);
            return _recommendationService.GetRecommendation(data);
        }
    }
}
