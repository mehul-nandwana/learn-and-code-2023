using CafeteriaManagementSystemServer.Models;
using System.Text;
using System.Text.Json;

namespace CafeteriaManagementSystemServer.Controller
{
    public abstract class ICommonController
    {
        JSonSerializer jsonSerializer = new JSonSerializer();
        public byte[] ExecuteRequest(CustomProtocolParameters requestData, ICommonController controller)
        {
            CustomProtocolParameters CustomProtocolParameters = controller.CallMethod(requestData);
            string Output = jsonSerializer.SerializeObject(CustomProtocolParameters);
            byte[] CustomProtocolParametersData = Encoding.ASCII.GetBytes(Output);
            return CustomProtocolParametersData;
        }

        public abstract CustomProtocolParameters CallMethod(CustomProtocolParameters serializedRequest);
    }
}
