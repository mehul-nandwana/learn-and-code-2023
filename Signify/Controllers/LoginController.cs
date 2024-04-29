using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Signify.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Signify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;
        private UsersDbContext _context;

        public LoginController(IConfiguration configuration, UsersDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpPost]
        public IActionResult Login(UserLogin usercCred)
        {
            var user = AuthenticateUser(usercCred);
            try
            {
                if (ModelState.IsValid)
                {
                    if (user != null)
                    {
                       // var token = GenerateToken(user);
                        return Ok("Logged in Successfully");
                    }
                    else
                    {
                        return NotFound("User not found");
                    }
                }
                return BadRequest("User not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        private string GenerateToken(UserInformation user)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("hgfgyuiu7y6trewsasdfrtyuikjhgf23456"));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Email, user.Email)
                };

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                  _configuration["Jwt:Audience"],
                  claims,
                  expires: DateTime.Now.AddMinutes(30),
                  signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private UserInformation AuthenticateUser(UserLogin userLogin)
        {
            try
            {
                var person = _context.UserInformations.FirstOrDefault(o => o.Email == userLogin.Email && o.Password == userLogin.Password);
                if (ModelState.IsValid)
                {
                    if (person != null)
                    {
                        return person;
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Some Exception Occured");

            }

        }
    }
}
