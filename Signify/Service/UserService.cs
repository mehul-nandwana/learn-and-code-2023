using Signify.Models;
using Signify.Repository;

namespace Signify.Service
{
    public class UserService
    {
        UserRepository User = new UserRepository();
        public void addUser(UserViewModel userInfo)
        {
            UserInformation userInformation = new UserInformation();
            userInformation.UserName = userInfo.UserName;
            userInfo.Email = userInfo.Email;
            userInfo.Gender = userInfo.Gender;
            userInfo.Phone = userInfo.Phone;
            userInfo.Password = userInfo.Password;
            User.SaveUser(userInformation);
        }

        public void updateUser(int id, string password,UserViewModel userInfo)
        {
            User.updateUser(id,password,userInfo);

        }
        public  UserInformation getUserInfo(int id)
        {
            UserInformation userInfo = User.getUser(id);
            return userInfo;
        }
    }
}
