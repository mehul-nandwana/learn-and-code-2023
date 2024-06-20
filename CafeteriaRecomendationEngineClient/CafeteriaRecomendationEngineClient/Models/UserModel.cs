using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaRecomendationEngineClient.Models
{
    [Serializable]
    public class UserModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
