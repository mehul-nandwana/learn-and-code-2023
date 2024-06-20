using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Models
{
    public class Request : CustomProtocolParameters<object>
    {
        private Dictionary<string, string> headerParameters = new Dictionary<string, string>();

        public Request(string method, object obj, string protocolFormat, int size, int port, int port2, IPAddress ip)
        {
            headerParameters.Add("method", method);
            Headers = headerParameters;
            this.obj = obj;
            Size = size;
            SourceIp = ip.ToString();
            DestPort = port;
            SourcePort = port2;
            DestIp = "";
        }

    }
}
