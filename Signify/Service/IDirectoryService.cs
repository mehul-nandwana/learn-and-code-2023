using Signify.Models;

namespace Signify.Service
{
    public interface IDirectoryService
    {
        public void addUserInDirectory(UserInformation userInformation);
        public string CreateUserDirectory(string username);
        public void AddUserInfoToFile(UserInformation user, string directory);

    }
}
