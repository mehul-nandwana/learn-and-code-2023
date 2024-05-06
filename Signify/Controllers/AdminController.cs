using Azure;
using Microsoft.AspNetCore.Mvc;
using Signify.Models;
using Signify.Service;

namespace Signify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase, IUser
    {
        private readonly IUserService userService;

        public AdminController(IUserService _userService)
        {
            userService = _userService;
        }
        [HttpPost("signUp")]
        public IActionResult addNewUser(int id,UserViewModel user)
        {

            ResponseBase<UserInformation> response = new ResponseBase<UserInformation>();
            try
            {
                    response = userService.addUser(user);
                    return Ok(response.message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update")]
        public IActionResult updateUser(int id, string password, UserViewModel user)
        {
            {
                ResponseBase<UserInformation> response = new ResponseBase<UserInformation>();
                try
                {

                    response = userService.updateUser(id, password, user);
                    return Ok(response.message);

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }

        }

        [HttpPost("getUserInfo")]
        public IActionResult getUserInfo(int id)
        {
            try
            {
                return Ok(userService.getUserInfo(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
