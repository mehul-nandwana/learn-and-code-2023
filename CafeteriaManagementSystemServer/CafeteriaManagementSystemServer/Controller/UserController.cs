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
            string requestData = serializedRequest.Obj.ToString();

            try
            {
                return method switch
                {
                    Constant.UPDATE_USER_PROFILE => UpdateUserProfile(requestData),
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

        private CustomProtocolParameters UpdateUserProfile(string userTaste)
        {
            UserProfile userProfile = _jsonSerializer.DeserializeObject<UserProfile>(userTaste);
            return _userService.UpdateUserProfile(userProfile);
        }
    }
}
