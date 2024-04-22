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
        UserService userService = new UserService();

        [HttpPost("edit")]
        public IActionResult updateUser(int id, string password, UserViewModel user)
        {
            ResponseBase<UserInformation> response = new ResponseBase<UserInformation>();
            try
            {
                response = userService.updateUser(id, password, user);
                if(response.errorCode==200)
                    return Ok(user);
                else
                    return BadRequest("Some Error occured");

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
