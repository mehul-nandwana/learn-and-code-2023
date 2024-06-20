using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Models
{
    public class Response : CustomProtocolParameters<object>
    {
        private Dictionary<string, string> headerParameters = new Dictionary<string, string>();
        private static string STATUS = "status";
        private static string STATUS_CODE = "error-code";
        private static string MESSAGE = "error-message";
        //public object obj;

        public Response(string status, string errorcode, string errorMessage, object obj, string response)
        {
            headerParameters.Add(STATUS, status);
            headerParameters.Add(STATUS_CODE, errorcode);
            headerParameters.Add(MESSAGE, errorMessage);
            Headers = headerParameters;
            DestPort = 8080;
            DestIp = "127.0.0.1";
            this.obj = obj;
            this.Method = response;
        }


    }
}
