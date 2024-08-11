using CafeteriaRecomendationEngineClient.DTO;
using System.Text.Json;

namespace CafeteriaRecomendationEngineClient.Client
{
    public class JSonSerializer
    {
        public CustomProtocolParameters DeserializeObject(string data)
        {
            CustomProtocolParameters communicationProtocol = JsonSerializer.Deserialize<CustomProtocolParameters>(data);
            return communicationProtocol;
        }
         
        public string SerializeObject(object communicationProtocol)
        {
            string data = JsonSerializer.Serialize(communicationProtocol);
            return data;
        }

        public T DeserializeObject<T>(object obj)
        {
            return JsonSerializer.Deserialize<T>(obj.ToString());
        }
    }
}
