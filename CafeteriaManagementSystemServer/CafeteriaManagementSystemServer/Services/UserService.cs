using CafeteriaManagementSystemServer.Controller;
using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Services
{
    public class UserService: IUser
    {
        public UserRepository _userRepository = new UserRepository();

       
        public Response Login(UserModel user)
        {
            Response response;
            string role = _userRepository.CheckLogin(user);
            int id = _userRepository.GetUserID(user);
            if (role != "nousermatch")
            {
                response = new Response(Constant.SUCCESSFULL, Constant.SUCCESS_STATUS, id.ToString(), id, role.ToLower());
            }
            else
            {
               response = new Response(Constant.FAILURE_MESSAGE, Constant.FAILURE_STATUS, "", id, "");
            }
            return response;
        }
    }
}
