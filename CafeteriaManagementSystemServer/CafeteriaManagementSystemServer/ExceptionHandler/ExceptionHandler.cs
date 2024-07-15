using CafeteriaManagementSystemServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.ExceptionHandler
{
    public class ExceptionHandler
    {
        JSonSerializer _jSonSerializer = new JSonSerializer();
        public byte[] HandleUnknownException(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Response response = new Response(ex.Message, Constant.EMPTY_STRING, Constant.USER_LOGIN);
            string Output = _jSonSerializer.SerializeObject(response);
            byte[] CustomProtocolParametersData = Encoding.ASCII.GetBytes(Output);
            return CustomProtocolParametersData;
        }
    }
}
