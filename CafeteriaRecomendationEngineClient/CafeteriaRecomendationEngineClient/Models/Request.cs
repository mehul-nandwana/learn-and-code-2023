using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaRecomendationEngineClient
{
    public class Request: CustomProtocolParameters<object>
    {
        private Dictionary<string, string> headerParameters = new Dictionary<string, string>();

            public Request(string method, object obj )
            {
                this.ProtocolType = "request";
                this.Response = "response";
                headerParameters.Add("method", method);
                this.Headers = headerParameters;
                this.obj = obj;
                this.SourceIp = "127.0.0.1";
                this.DestPort = 8080;
                this.SourcePort = 8080;
                this.DestIp = "";
            }
        
    }
}
