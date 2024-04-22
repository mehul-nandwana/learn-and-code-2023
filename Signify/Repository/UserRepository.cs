using Microsoft.Data.Sqlite;
using Signify.Models;
using System.Globalization;

namespace Signify.Repository
{
    public class UserRepository : IUserRepository
    {
        public UsersDbContext UsersDbContext;
        public UserRepository()
        {
            UsersDbContext = new UsersDbContext();
        }

        public ResponseBase<UserInformation> saveUser(UserInformation user)
        {
            ResponseBase<UserInformation> response = new ResponseBase<UserInformation>();
            UsersDbContext.UserInformations.Add(user);
            int isSaved = UsersDbContext.SaveChanges();
            if(isSaved == 0)
            {
                response.value = null;
                response.errorCode = 500;
                response.message = "Data Not saved Successfully";
            }
            else
            {
                response.value = user;
                response.errorCode = 200;
                response.message = "Data saved Successfully";
            }
            return response;
        }

        public UserInformation getUser(int userId)
        {
            UserInformation user = UsersDbContext.UserInformations.Where(x => x.UserId == userId ).FirstOrDefault();
            return user;
        }

        public ResponseBase<UserInformation> updateUser(int id, string password, UserViewModel user)
        {
            ResponseBase<UserInformation> response = new ResponseBase<UserInformation>();
            UserInformation userdetails = getUser(id);
            userdetails.Gender = user.Gender;
            userdetails.UserName = user.UserName;
            userdetails.Password = user.Password;
            userdetails.Email = user.Email;
            userdetails.Phone = user.Phone;
            int isSaved = UsersDbContext.SaveChanges();
            if(isSaved == 0)
            {
                response.value = null;
                response.errorCode = 500;
                response.message = "Data Not saved Successfully";
            }
            else
            {
                response.value = this.getUser(id);
                response.errorCode = 200;
                response.message = "Data saved Successfully";
            }
            return response;
        }
    }
}
