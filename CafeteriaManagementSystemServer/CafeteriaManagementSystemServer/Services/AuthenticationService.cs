using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using NLog;

namespace CafeteriaManagementSystemServer.Services
{
    public class AuthenticationService 
    {
        public UserRepository _userRepository = new UserRepository();
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public Response Login(UserModel user)
        {
            Response response;
            string role = _userRepository.CheckLogin(user);
            int id = _userRepository.GetUserID(user);
            if (role != "nousermatch")
            {
                _logger.Info("User id logged in " + user.username + " Logged in");
                response = new Response(Constant.SUCCESSFULL, id, role);
            }
            else
            {
                response = new Response(Constant.FAILURE_MESSAGE, id, Constant.EMPTY_STRING);
            }
            return response;
        }

        public Response Logout(UserModel user)
        {
            Response response;
            _logger.Info("User id logged in " + user.username + " Logged out");
            string role = _userRepository.CheckLogin(user);
            int id = _userRepository.GetUserID(user);
            response = new Response(Constant.SUCCESSFULL, id, role.ToLower());
            return response;
        }

    }
}
