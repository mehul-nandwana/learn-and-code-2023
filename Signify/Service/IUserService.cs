using Signify.Models;

namespace Signify.Service
{
    public interface IUserService
    {
        public ResponseBase<UserInformation> addUser(UserViewModel userInfo);
        public ResponseBase<UserInformation> updateUser(int id, string password, UserViewModel userInfo);
        public UserInformation getUserInfo(int id);

    }
}
