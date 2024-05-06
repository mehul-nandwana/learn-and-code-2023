using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signify.Models;
using Signify.Service;

namespace Signify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NormalUserController : ControllerBase  , IUser
    {
        private readonly IUserService userService;

        public NormalUserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpPost("edit")]
        public IActionResult updateUser(int id, string password, UserViewModel user)
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

        [HttpPost("getUserInformation")]
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
