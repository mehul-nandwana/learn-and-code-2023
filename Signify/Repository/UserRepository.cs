using Signify.Models;

namespace Signify.Repository
{
    public class UserRepository: IUserRepository
    {
        public UsersDbContext UsersDbContext;
        public UserRepository() { 
        UsersDbContext = new UsersDbContext();
        }

        public void SaveUser(UserInformation userInformation)
        {

           UsersDbContext.UserInformations.Add(userInformation);
           int m=UsersDbContext.SaveChanges();
        }

        public UserInformation getUser(int userId)
        {
            UserInformation user = UsersDbContext.UserInformations.Where(x=>x.UserId == userId ).FirstOrDefault();
            return user;
        }

        public void updateUser(int id,string password, UserViewModel user)
        {
            UserInformation userdetails = this.getUser(id);
            userdetails.Gender = user.Gender;
            userdetails.UserName = user.UserName;   
            userdetails.Password = user.Password;
            userdetails.Email = user.Email;
            userdetails.Phone = user.Phone;
            int m = UsersDbContext.SaveChanges();
        }
    }
}
