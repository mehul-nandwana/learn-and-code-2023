using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.ExceptionHandler
{
    public class ControllerNotfound:Exception
    {
        public override string Message
        {
            get
            {
                return "Invalid Request Recieved. Please try again after with valid request";
            }
        }

    }
}
