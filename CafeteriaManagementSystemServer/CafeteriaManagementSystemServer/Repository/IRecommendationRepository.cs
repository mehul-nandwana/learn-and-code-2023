using CafeteriaRecomendationEngineClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Repository
{
    public interface IRecommendationRepository
    {
        public List<int> getRecommendation(RecommendatioData data);
    }
}
