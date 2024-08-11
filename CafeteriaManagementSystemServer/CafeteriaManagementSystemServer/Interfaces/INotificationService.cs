using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Interfaces
{
    public interface INotificationService
    {
        public void AddNotification(string notificationType, string notificationMessage);
        public Response GetAllNotification(CustomProtocolParameters requestData);
    }
}
