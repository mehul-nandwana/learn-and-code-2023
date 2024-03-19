using Signify.Models;

namespace Signify.Repository
{
    public interface IUserRepository
    {
        public void SaveUser(UserInformation userInformation);
    }
}
