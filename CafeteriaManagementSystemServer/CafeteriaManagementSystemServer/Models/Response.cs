using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Models
{
    public class Response:CustomProtocolParameters
    {
        public Response(string statusMessage, object obj, string methodToCall)
        {
            this.StatusMessage = statusMessage;
            this.Obj = obj;
            this.Method = methodToCall;
        }
    }
}
