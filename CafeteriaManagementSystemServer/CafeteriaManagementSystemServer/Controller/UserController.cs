using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using CafeteriaManagementSystemServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Controller
{
    public class UserController : ICommonController
    {
        public UserService _user =new UserService();

        //public UserController(IUser user)
        //{ 
        //    _user = user;
        //}
       

        public byte[] ExecuteRequest(CustomProtocolParameters<Object> requestData, string method)
        {
            Response response = CallMethod("Login", requestData.obj.ToString());
            string Output = JsonSerializer.Serialize(response);
            byte[] responseData = Encoding.ASCII.GetBytes(Output);
            return responseData;
        }

        public Response CallMethod( string methodName, string par)
        {
            try
            {
                // UserModel obj = JsonSerializer.Deserialize<UserModel>(par);
                //UserModel userModel = JsonSerializer.Deserialize<UserModel>(par);
                UserModel userModel = JsonSerializer.Deserialize<UserModel>(par);
                //UserModel userModel = (UserModel)par;
                return _user.Login(userModel);
            }
            catch(Exception e)
            {
                UserModel userModel = JsonSerializer.Deserialize<UserModel>(par);
                //UserModel userModel = (UserModel)par;
                return _user.Login(userModel);
            }

        }
    }
}
