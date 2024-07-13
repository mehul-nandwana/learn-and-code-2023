using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.DTOs
{
    public class NotificationData
    {
        public List<string> NotificationMessage {  get; set; }
        public int userId {  get; set; }
    }
}
