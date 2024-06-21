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
        public Response GetRecommendation(RecommendatioData data)
        {
            return _recommendationService.GetRecommendation(data);
        }
        public byte[] ExecuteRequest(CustomProtocolParameters<Object> requestData, string method)
        {
            Response response = CallMethod(method, requestData.obj.ToString());
            string Output = JsonSerializer.Serialize(response);
            byte[] responseData = Encoding.ASCII.GetBytes(Output);
            return responseData;
        }

        public Response CallMethod(string methodName, string par)
        {          
                RecommendatioData data = JsonSerializer.Deserialize<RecommendatioData>(par);
                return _recommendationService.GetRecommendation(data);
        }
    }
}
