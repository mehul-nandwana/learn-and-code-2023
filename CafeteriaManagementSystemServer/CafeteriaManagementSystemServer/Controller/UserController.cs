using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using CafeteriaRecomendationEngineClient.DTO;
using System.Reflection.Metadata;
using System.Text.Json;

namespace CafeteriaManagementSystemServer.Controller
{
    public class UserController : ICommonController
    {
        public UserService _user = new UserService();
        JSonSerializer _jsonSerializer = new JSonSerializer();
        public override CustomProtocolParameters CallMethod(CustomProtocolParameters serializedRequest)
        {
            string method = serializedRequest.Method;
            string parameter = serializedRequest.Obj.ToString();

            if (method == "login")
            {
                UserModel userModel = _jsonSerializer.DeserializeObject<UserModel>(parameter);
                return _user.Login(userModel);
            }
            else if(method == "updateuserprofile")
            {
                UserProfile userProfile = _jsonSerializer.DeserializeObject<UserProfile>(parameter);
                return _user.UpdateUserProfile(userProfile);
            }
            else
                throw new ControllerNotfound();
        }
    }
}
