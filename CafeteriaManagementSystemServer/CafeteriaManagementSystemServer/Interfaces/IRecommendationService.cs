using CafeteriaManagementSystemServer.DTOs;
using CafeteriaRecomendationEngineClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Interfaces
{
    public interface IRecommendationService
    {
        public CustomProtocolParameters GetRecommendation(RecommendatioData data);
    }
}
