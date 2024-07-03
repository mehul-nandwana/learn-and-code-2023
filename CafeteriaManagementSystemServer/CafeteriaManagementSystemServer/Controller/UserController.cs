using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using CafeteriaRecomendationEngineClient.DTO;
using System.Text.Json;
using Constant = CafeteriaManagementSystemServer.Models.Constant;

namespace CafeteriaManagementSystemServer.Controller
{
    public class UserController : ICommonController
    {
        private readonly UserService _userService;
        private readonly JSonSerializer _jsonSerializer;

        public UserController()
        {
            _userService = new UserService();
            _jsonSerializer = new JSonSerializer();
        }

        public override CustomProtocolParameters CallMethod(CustomProtocolParameters serializedRequest)
        {
            string method = serializedRequest.Method;
            string parameter = serializedRequest.Obj.ToString();

            try
            {
                return method switch
                {
                    Constant.LOGIN => Login(parameter),
                    Constant.UPDATE_USER_PROFILE => UpdateUserProfile(parameter),
                    _ => throw new ControllerNotfound(),
                };
            }
            catch (ControllerNotfound ex)
            {
                Console.WriteLine($"Controller not found: {ex.Message}");
                throw;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Deserialization error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        private CustomProtocolParameters Login(string parameter)
        {
            UserModel userModel = _jsonSerializer.DeserializeObject<UserModel>(parameter);
            return _userService.Login(userModel);
        }

        private CustomProtocolParameters UpdateUserProfile(string parameter)
        {
            UserProfile userProfile = _jsonSerializer.DeserializeObject<UserProfile>(parameter);
            return _userService.UpdateUserProfile(userProfile);
        }
    }
}
