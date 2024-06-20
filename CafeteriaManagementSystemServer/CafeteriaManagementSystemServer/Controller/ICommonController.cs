using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Controller
{
    public interface ICommonController
    {
            public byte[] ExecuteRequest(CustomProtocolParameters<Object> requestData,string method);
        
    }
}
