using CafeteriaManagementSystemServer.Controller;
using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using CafeteriaRecomendationEngineClient.DTO;

namespace CafeteriaManagementSystemServer.Services
{
    public class UserService: IUserService
    {
        public UserRepository _userRepository = new UserRepository();
     
        public Response Login(UserModel user)
        {
            Response response;
            string role = _userRepository.CheckLogin(user);
            int id = _userRepository.GetUserID(user);
            if (role != "nousermatch")
            {
                response = new Response(Constant.SUCCESSFULL, id, role);
            }
            else
            {
                response = new Response(Constant.FAILURE_MESSAGE, id, Constant.EMPTY_STRING);
            }
            return response;
        }

        public Response UpdateUserProfile(UserProfile userprofile)
        {
            UserPreference userpreference = SetUserProfileData(userprofile);
            _userRepository.UpdateUserProfile(userpreference);
           Response response = new Response(Constant.USER_PROFILE_UPDATED_SUCCESSFULLY, userprofile.UserId, Constant.EMPLOYEE_LOGIN);
            return response;
        }
        public UserPreference SetUserProfileData(UserProfile userprofile)
        {
            UserPreference userPreference = new UserPreference();
            userPreference.UserId = userprofile.UserId;
            userPreference.Sweet = userprofile.Sweet;
            userPreference.SpiceLevel = userprofile.SpiceLevel;
            userPreference.IsVegeterian = userprofile.IsVegeterian;
            userPreference.CuisineType = userprofile.CuisineType;
            return userPreference;
        }
    }
}
