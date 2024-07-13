using CafeteriaManagementSystemServer.Interfaces;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using CafeteriaRecomendationEngineClient.DTO;

namespace CafeteriaManagementSystemServer.Services
{
    public class UserService: IUserService
    {
        public UserRepository _userRepository = new UserRepository();
     

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
