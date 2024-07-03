using CafeteriaManagementSystemServer.Models;
using CafeteriaRecomendationEngineClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Services
{
    public interface IRecommendationService
    {
        public CustomProtocolParameters GetRecommendation(RecommendatioData data);
    }
}
