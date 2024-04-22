using Signify.Models;

namespace Signify.Service
{
    public class DirectoryService
    {

        public  void addUserInDirectory(UserInformation userInformation)
        {
    
                string userDirectory = CreateUserDirectory(userInformation.UserName);
                AddUserInfoToFile(userInformation, userDirectory);
                Console.WriteLine($"New user added: {userInformation.UserName}");         
           
        }
        public  string CreateUserDirectory(string username)
        {
            string executablePath = AppDomain.CurrentDomain.BaseDirectory;
            string userDirectory = Path.Combine(executablePath, "UserDirectories", username);

            Directory.CreateDirectory(userDirectory);            
            return userDirectory;
        }

        public  void AddUserInfoToFile(UserInformation user, string directory)
        {
            string filePath = Path.Combine(directory, "user_info.txt");         
                using (StreamWriter writer = File.CreateText(filePath))
                {
                    writer.WriteLine($"Username: {user.UserName}");
                    writer.WriteLine($"Email: {user.Email}");
                }                  
        }
    }
}
