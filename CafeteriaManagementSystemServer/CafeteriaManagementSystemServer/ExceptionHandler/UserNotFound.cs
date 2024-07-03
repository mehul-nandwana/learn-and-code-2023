using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.ExceptionHandler
{
    public class UserNotFound:Exception
    {
        public override string Message
        {
            get
            {
                return "No user Found. Please try Again";
            }
        }
    }
}
