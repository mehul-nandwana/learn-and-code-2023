using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;

namespace CafeteriaManagementSystemServer.Controller
{
    public class NotificationController : ICommonController
    {
        NotificationService _notificationService = new NotificationService();

        public override CustomProtocolParameters CallMethod(CustomProtocolParameters requestData)
        {
            if (requestData.Method == "getnotification")
            {
                return _notificationService.GetAllNotication();
            }
            else if (requestData.Method == "addgetdetailfeedbacknotification")
            {
                return _notificationService.AddDetailFeedbackNotification(requestData);
            }
            else
            {
                throw new ControllerNotfound();
            }
        }

    }
}
