using System;
using System.Text.Json;

namespace CafeteriaManagementSystemServer.Models
{
    public class JSonSerializer
    {
        public CustomProtocolParameters DeSerializeObject(string data)
        {
            try
            {
                if (data == null)
                {
                    throw new ArgumentNullException(nameof(data), "Input data cannot be null.");
                }

                CustomProtocolParameters communicationProtocol = JsonSerializer.Deserialize<CustomProtocolParameters>(data);
                return communicationProtocol;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Argument null error: {ex.Message}");
                throw ; 
            }
            catch (NotSupportedException ex)
            {
                Console.WriteLine($"Not supported error: {ex.Message}");
                throw; 
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON deserialization error: {ex.Message}");
                throw; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw; 
            }
        }

        public string SerializeObject(CustomProtocolParameters communicationProtocol)
        {
            try
            {
                if (communicationProtocol == null)
                {
                    throw new ArgumentNullException(nameof(communicationProtocol), "Communication protocol object cannot be null.");
                }

                string data = JsonSerializer.Serialize(communicationProtocol);
                return data;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Argument null error: {ex.Message}");
                throw; 
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON serialization error: {ex.Message}");
                throw; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                throw; 
            }
        }

        public T DeserializeObject<T>(object obj)
        {
            return JsonSerializer.Deserialize<T>(obj.ToString());
        }
    }
}
