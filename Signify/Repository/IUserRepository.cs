using Signify.Models;

namespace Signify.Repository
{
    public interface IUserRepository
    {
        public ResponseBase<UserInformation> saveUser(UserInformation userInformation);
        public UserInformation getUser(int userId);
        public ResponseBase<UserInformation> updateUser(int id, string password, UserViewModel user);


    }
}
