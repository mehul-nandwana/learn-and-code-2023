using Signify.Models;
using Signify.Repository;

namespace Signify.Service
{
    public class UserService:IUserService
    {
        private readonly IUserRepository user;
        private readonly IDirectoryService directoryService;
        private readonly INotificationService notificationService;
        public UserService(IUserRepository _user, IDirectoryService _directoryService, INotificationService _notificationService)
        {
            user = _user;
            directoryService = _directoryService;
            notificationService = _notificationService;
        }
        public ResponseBase<UserInformation> addUser(UserViewModel userInfo)
        {
            try
            {
                ResponseBase<UserInformation> response;
                UserInformation userInformation = this.mapUserData(userInfo);
                response = user.saveUser(userInformation);
                directoryService.addUserInDirectory(userInformation);
                notificationService.sendNotification(userInformation.Email);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }
        }

        public ResponseBase<UserInformation> updateUser(int id, string password, UserViewModel userInfo)
        {
            try
            {
                ResponseBase<UserInformation> response;
                response = user.updateUser(id, password, userInfo);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }
        }
        public UserInformation getUserInfo(int id)
        {
            try
            {
                UserInformation userInfo = user.getUser(id);
                return userInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }
        }

        public UserInformation mapUserData(UserViewModel userInfo)
        {
            try
            {
                UserInformation userInformation = new UserInformation();
                userInformation.UserName = userInfo.UserName;
                userInformation.Email = userInfo.Email;
                userInformation.Gender = userInfo.Gender;
                userInformation.Phone = userInfo.Phone;
                userInformation.Password = userInfo.Password;
                userInformation.Role = userInfo.Role;
                return userInformation;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw ex;
            }

        }
    }
}
