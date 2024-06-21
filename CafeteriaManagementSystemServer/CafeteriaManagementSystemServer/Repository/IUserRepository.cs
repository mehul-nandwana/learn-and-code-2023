using CafeteriaManagementSystemServer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Repository
{
    public interface IUserRepository
    {
        public string CheckLogin(UserModel userModel);

    }
}
