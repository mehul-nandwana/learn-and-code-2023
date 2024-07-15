using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using System.Text.Json;
using Constant = CafeteriaManagementSystemServer.Models.Constant;

namespace CafeteriaManagementSystemServer.Controller
{
    public class AuthenticationController : ICommonController
    {
        private readonly AuthenticationService _authService;
        private readonly JSonSerializer _jsonSerializer;

        public AuthenticationController()
        {
            _authService = new AuthenticationService();
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
                    Constant.LOGIN => Login(requestData),
                    Constant.ADMIN_LOGOUT or Constant.EMPLOYEE_LOGOUT or Constant.CHEF_LOGOUT => Logout(requestData,method),
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

        private CustomProtocolParameters Login(string userCredentials)
        {
            UserModel userModel = _jsonSerializer.DeserializeObject<UserModel>(userCredentials);
            return _authService.Login(userModel);
        }

        private CustomProtocolParameters Logout(string userCredentials, string method)
        {
            int user = _jsonSerializer.DeserializeObject<int>(userCredentials);
            return _authService.Logout(user,method);
        }
    }
}
