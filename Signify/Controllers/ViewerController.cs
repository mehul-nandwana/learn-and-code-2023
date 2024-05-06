using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Signify.Models;
using Signify.Service;

namespace Signify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewerController : ControllerBase, IUser
    {
        private readonly IUserService userService;
        
        public ViewerController(IUserService _userService)
        {
            userService = _userService;
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
