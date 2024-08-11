using CafeteriaRecomendationEngineClient.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaRecomendationEngineClient.Client
{
    public class Request : CustomProtocolParameters
    {
        public Request(string methodToCall, object obj)
        {
            StatusMessage = "Ok";
            Obj = obj;
            Method = methodToCall;
        }
    }
}
