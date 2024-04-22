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
        UserService userService = new UserService();
        [HttpPost("signUp")]
        public IActionResult addNewUser(int id,UserViewModel user)
        {
            UserInformation userInformation= userService.getUserInfo(id);

            ResponseBase<UserInformation> response = new ResponseBase<UserInformation>();
            try
            {

                    response = userService.addUser(user);
                    if (response.errorCode == 200)
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
            UserInformation userInformation = userService.getUserInfo(id);
            {
                ResponseBase<UserInformation> response = new ResponseBase<UserInformation>();
                try
                {

                        response = userService.updateUser(id, password, user);
                        if (response.errorCode == 200)
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
