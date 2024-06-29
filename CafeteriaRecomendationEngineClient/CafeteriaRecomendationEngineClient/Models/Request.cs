using CafeteriaManagementSystemServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaRecomendationEngineClient.Models
{
    public class Request:CustomProtocolParameters
    {
        public Request(string methodToCall, object obj)
        {
            this.StatusMessage = "Ok";
            this.Obj = obj;
            this.Method = methodToCall;
        }
    }
}
