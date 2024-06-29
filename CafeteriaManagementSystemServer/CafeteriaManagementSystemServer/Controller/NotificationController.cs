using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Controller
{
    public class NotificationController : ICommonController
    {
        NotificationService _notificationService = new NotificationService();
        
        public override CustomProtocolParameters CallMethod(CustomProtocolParameters requestData)
        {       
            return _notificationService.GetAllNotication();
        }
    }
}
