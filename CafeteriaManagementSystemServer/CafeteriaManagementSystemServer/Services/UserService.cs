using CafeteriaManagementSystemServer.Controller;
using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;

namespace CafeteriaManagementSystemServer.Services
{
    public class UserService: IUserService
    {
        public UserRepository _userRepository = new UserRepository();

       
        public Response Login(UserModel user)
        {
            Response response;
            string role = _userRepository.CheckLogin(user);
            int id = _userRepository.GetUserID(user);
            if (role != "nousermatch")
            {
                throw new UserNotFound();
            }
            else
            {
                response = new Response(Constant.FAILURE_MESSAGE, id, Constant.EMPTY_STRING);
            }
            return response;
        }
    }
}
