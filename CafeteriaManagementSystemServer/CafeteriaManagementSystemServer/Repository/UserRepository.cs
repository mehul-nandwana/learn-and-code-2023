using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Interfaces;
using CafeteriaManagementSystemServer.Models;

namespace CafeteriaManagementSystemServer.Repository
{
    public class UserRepository: IUserRepository
    {
        public CafeteriaMangagementSystemContext DbContext;

        public UserRepository()
        {
            DbContext = new CafeteriaMangagementSystemContext();
        }

        public string CheckLogin(UserModel userModel)
        {
            User user = DbContext.Users.SingleOrDefault(u => u.Username == userModel.username && u.Password == userModel.password);
            if (user != null)
            {
                string role = DbContext.Roles.Where(x => x.Id == user.Roleid).Select(x => x.RoleType).FirstOrDefault();
                return role;
            }
            else
            {
                throw new UserNotFound();
            }
        }

        public int GetUserID(UserModel userModel)
        {
            User user = DbContext.Users.SingleOrDefault(u => u.Username == userModel.username && u.Password == userModel.password);
            if (user == null)
                throw new UserNotFound();
           
            return user.Id;
        }

        public void UpdateUserProfile(UserPreference userPreference)
        {
            UserPreference user = DbContext.UserPreferences.Where(x=>x.UserId == userPreference.UserId).FirstOrDefault();
            if(user != null)
            {
                user.IsVegeterian = userPreference.IsVegeterian;
                user.Sweet = userPreference.Sweet;
                user.SpiceLevel = userPreference.SpiceLevel;
                user.CuisineType = userPreference.CuisineType;
                DbContext.UserPreferences.Update(user);
                DbContext.SaveChanges();
            }
            else
            {
                DbContext.UserPreferences.Add(userPreference);
                DbContext.SaveChanges();

            }
        }
    }
}
