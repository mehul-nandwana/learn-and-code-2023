using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaRecomendationEngineClient.DTO
{
    public class UserProfile
    {
        public int UserId { get; set; }
        public string IsVegeterian { get; set; }
        public int SpiceLevel { get; set; }
        public bool Sweet { get; set; }
        public string CuisineType { get; set; }
    }
}
