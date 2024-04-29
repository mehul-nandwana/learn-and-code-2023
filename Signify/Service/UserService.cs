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
            ResponseBase<UserInformation> response ;
            UserInformation userInformation = this.mapUserData(userInfo);
            response = user.saveUser(userInformation);
            if (response.errorCode == 200)
            {
                directoryService.addUserInDirectory(userInformation);
                notificationService.sendNotification(userInformation.Email);
            }
            return response;
        }

        public ResponseBase<UserInformation> updateUser(int id, string password, UserViewModel userInfo)
        {
            ResponseBase<UserInformation> response;
            response = user.updateUser(id, password, userInfo);
            return response;
        }
        public UserInformation getUserInfo(int id)
        {          
                UserInformation userInfo = user.getUser(id);
                return userInfo;                    
        }

        public UserInformation mapUserData(UserViewModel userInfo)
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
    }
}
