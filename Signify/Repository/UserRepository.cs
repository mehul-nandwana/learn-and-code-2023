using Azure;
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
            try
            {
                UsersDbContext.UserInformations.Add(user);
                UsersDbContext.SaveChanges();
                response.value = user;
                response.errorCode = 200;
                response.message = "Data saved Successfully";
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }
        }

        public UserInformation getUser(int userId)
        {
            try
            {
                UserInformation user = UsersDbContext.UserInformations.Where(x => x.UserId == userId).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw ex;

            }
        }

        public ResponseBase<UserInformation> updateUser(int id, string password, UserViewModel user)
        {
            ResponseBase<UserInformation> response = new ResponseBase<UserInformation>();

            try
            {
                UserInformation userdetails = getUser(id);
                userdetails.Gender = user.Gender;
                userdetails.UserName = user.UserName;
                userdetails.Password = user.Password;
                userdetails.Email = user.Email;
                userdetails.Phone = user.Phone;
                UsersDbContext.SaveChanges();
                response.value = this.getUser(id);
                response.errorCode = 200;
                response.message = "Data saved Successfully";
            }         
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw ex;                

            }
            return response;
        }
    }
}
