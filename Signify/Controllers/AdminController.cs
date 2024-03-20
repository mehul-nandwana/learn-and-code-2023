using Microsoft.AspNetCore.Mvc;
using Signify.Models;
using Signify.Service;

namespace Signify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        UserService userService = new UserService();
        [HttpPost("signUp")]
        public void AddNewUser(UserViewModel user)
        {
            userService.addUser(user);
        }

        [HttpPost("update")]
        public void UpdateUser(int id, string password, UserViewModel user)
        {
            userService.updateUser(id, password, user);
        }
    }
}
