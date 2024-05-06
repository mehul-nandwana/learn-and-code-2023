using Signify.Models;

namespace Signify.Service
{
    public class DirectoryService:IDirectoryService
    {

        public  void addUserInDirectory(UserInformation userInformation)
        {
            try
            {
                string userDirectory = CreateUserDirectory(userInformation.UserName);
                AddUserInfoToFile(userInformation, userDirectory);
                Console.WriteLine($"New user added: {userInformation.UserName}");
            }
            catch(Exception ex) {
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }

           
        }
        public  string CreateUserDirectory(string username)
        {
            try
            {
                string executablePath = AppDomain.CurrentDomain.BaseDirectory;
                string userDirectory = Path.Combine(executablePath, "UserDirectories", username);

                Directory.CreateDirectory(userDirectory);
                return userDirectory;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }
        }

        public  void AddUserInfoToFile(UserInformation user, string directory)
        {
            try
            {
                string filePath = Path.Combine(directory, "user_info.txt");
                using (StreamWriter writer = File.CreateText(filePath))
                {
                    writer.WriteLine($"Username: {user.UserName}");
                    writer.WriteLine($"Email: {user.Email}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }
        }
    }
}
