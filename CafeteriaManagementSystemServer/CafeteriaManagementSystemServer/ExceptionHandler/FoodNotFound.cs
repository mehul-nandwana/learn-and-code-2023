using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.ExceptionHandler
{
    public class FoodNotFound:Exception
    {
        public override string Message
        {
            get
            {
                return "Food Does not exist. Please try with valid food item";
            }
        }
    }
}
