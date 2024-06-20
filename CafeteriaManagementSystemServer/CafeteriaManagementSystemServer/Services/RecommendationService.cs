using CafeteriaManagementSystemServer.Controller;
using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using CafeteriaRecomendationEngineClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Services
{
    public class RecommendationService
    {
        RecommendationRepository recommendationRepository = new RecommendationRepository();
        public Response GetRecommendation(RecommendatioData data)
        {

            List<string> recommendedItem =  recommendationRepository.getRecommendation(data);
            Response response = new Response(Constant.SUCCESS_MESSAGE, Constant.SUCCESS_STATUS, Constant.SUCCESSFULLY_ADDED_FEEDBACK, recommendedItem, "chef");
            return response;
        }
    }
}
