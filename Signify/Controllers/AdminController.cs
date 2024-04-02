using Azure;
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
        public IActionResult addNewUser(UserViewModel user)
        {
            ResponseBase<UserInformation> response = new ResponseBase<UserInformation>();
            try
            {
                response = userService.addUser(user);
                if(response.errorCode==200)
                  return Ok("User Added Successfully");
                else
                  return BadRequest("Some error occured");

            }
            catch (Exception ex)
            {              
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("update")]
        public IActionResult updateUser(int id, string password, UserViewModel user)
        {
            ResponseBase<UserInformation> response = new ResponseBase<UserInformation>();
            try
            {
                response = userService.updateUser(id, password, user);
                if(response.errorCode == 200)
                return Ok(user);
                else
                return BadRequest("Some error occured");



            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
