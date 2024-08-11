using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using System.Text;
using System.Text.Json;

namespace CafeteriaManagementSystemServer.Controller
{
    public abstract class ICommonController
    {
        JSonSerializer _jsonSerializer = new JSonSerializer();
        public byte[] ExecuteRequest(CustomProtocolParameters requestData, ICommonController controller)
        {
            CustomProtocolParameters CustomProtocolParameters = controller.CallMethod(requestData);
            string SerializedResponse = _jsonSerializer.SerializeObject(CustomProtocolParameters);
            byte[] CustomProtocolParametersData = Encoding.ASCII.GetBytes(SerializedResponse);
            return CustomProtocolParametersData;
        }

        public abstract CustomProtocolParameters CallMethod(CustomProtocolParameters serializedRequest);
    }
}
