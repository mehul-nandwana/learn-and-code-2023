using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using System.Text.Json;

namespace CafeteriaManagementSystemServer.Controller
{
    public class UserController : ICommonController
    {
        public UserService _user =new UserService();

        public override CustomProtocolParameters CallMethod(CustomProtocolParameters customProtocolParameters)
        {
            string parameter = customProtocolParameters.Obj.ToString();
            UserModel userModel = JsonSerializer.Deserialize<UserModel>(parameter);
            return _user.Login(userModel);
        }
    }
}
