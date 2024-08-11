using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Interfaces
{
    internal interface IAuthenticationService
    {
        public Response Logout(int user,string role);
        public Response Login(UserModel user);

    }
}
