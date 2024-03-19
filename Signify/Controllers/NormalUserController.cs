using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signify.Models;
using Signify.Service;

namespace Signify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NormalUserController : ControllerBase
    {
        UserService userService = new UserService();

        [HttpPost("edit")]
        public void UpdateUser(int id, string password, UserViewModel user)
        {
            userService.updateUser(id, password, user);
        }

        [HttpPost("getUserInformation")]
        public UserInformation GetUserInfo(int id)
        {
            return userService.getUserInfo(id);
        }


    }
}
