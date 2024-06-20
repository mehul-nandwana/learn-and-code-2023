using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace CafeteriaRecommendationEngine
{
    public class JSonSerializer
    {
        public CustomProtocolParameters<object> DeSerializeObject(string data)
        {
            CustomProtocolParameters<object> communicationProtocol = JsonSerializer.Deserialize<CustomProtocolParameters<object>>(data);
            return communicationProtocol;
        }
        public string SerializeObject(CustomProtocolParameters<object> communicationProtocol)
        {
            string data = JsonSerializer.Serialize(communicationProtocol);
            return data;
        }
    }
}
