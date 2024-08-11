using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;

namespace CafeteriaManagementSystemServer.Controller
{
    public class NotificationController : ICommonController
    {
        private readonly NotificationService _notificationService;

        public NotificationController()
        {
            _notificationService = new NotificationService();
        }

        public override CustomProtocolParameters CallMethod(CustomProtocolParameters requestData)
        {
            try
            {
                return requestData.Method switch
                {
                    Constant.GET_NOTIFICATION => _notificationService.GetAllNotification(requestData),
                    Constant.ADD_GET_DETAIL_FEEDBACK_NOTIFICATION => _notificationService.AddDetailFeedbackNotification(requestData),
                    _ => throw new ControllerNotfound(),
                };
            }
            catch (ControllerNotfound ex)
            {
                Console.WriteLine($"Controller not found: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
