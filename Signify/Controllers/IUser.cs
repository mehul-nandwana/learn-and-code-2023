using Microsoft.AspNetCore.Mvc;

namespace Signify.Controllers
{
    internal interface IUser
    {
        public IActionResult getUserInfo(int id);

    }
}