using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.ExceptionHandler
{
    public class ItemAlreadyExists:Exception
    {
        public override string Message
        {
            get
            {
                return "Item already exists in discard item list";
            }
        }
    }
}
