using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signify.Models;
using Signify.Service;

namespace Signify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewerController : ControllerBase
    {
        UserService userService = new UserService();
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
