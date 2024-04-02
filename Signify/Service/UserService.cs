using Signify.Models;
using Signify.Repository;

namespace Signify.Service
{
    public class UserService:IUserService
    {
        UserRepository User = new UserRepository();
        public ResponseBase<UserInformation> addUser(UserViewModel userInfo)
        {
            ResponseBase<UserInformation> response ;
            UserInformation userInformation = new UserInformation();
            userInformation.UserName = userInfo.UserName;
            userInformation.Email = userInfo.Email;
            userInformation.Gender = userInfo.Gender;
            userInformation.Phone = userInfo.Phone;
            userInformation.Password = userInfo.Password;
            userInformation.Role = userInfo.Role;
            response = User.saveUser(userInformation);
            return response;
        }

        public ResponseBase<UserInformation> updateUser(int id, string password, UserViewModel userInfo)
        {
            ResponseBase<UserInformation> response;
            response = User.updateUser(id, password, userInfo);
            return response;
        }
        public UserInformation getUserInfo(int id)
        {          
                UserInformation userInfo = User.getUser(id);
                return userInfo;
                     
        }
    }
}
