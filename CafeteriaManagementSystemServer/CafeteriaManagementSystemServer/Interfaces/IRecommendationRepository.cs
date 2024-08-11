using CafeteriaRecomendationEngineClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Interfaces
{
    public interface IRecommendationRepository
    {
        public Dictionary<int, string> getRecommendation(RecommendatioData data);
    }
}
