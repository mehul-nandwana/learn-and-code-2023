using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CafeteriaManagementSystemServer.Repository
{
    public class UserRepository: IUserRepository
    {
        public CafeteriaMangagementSystemContext DbContext;
        public UserRepository()
        {
            DbContext = new CafeteriaMangagementSystemContext();
        }
        public string CheckLogin(UserModel userModel)
        {
            User user = DbContext.Users.SingleOrDefault(u => u.Username == userModel.username && u.Password == userModel.password);
            if (user != null)
            {
                string role = DbContext.Roles.Where(x => x.Id == user.Roleid).Select(x => x.RoleType).FirstOrDefault();
                return role;
            }
            else
                return "nousermatch";
        }
        public int GetUserID(UserModel userModel)
        {
            User user = DbContext.Users.SingleOrDefault(u => u.Username == userModel.username && u.Password == userModel.password);
            return user.Id;
        }
    }
}
