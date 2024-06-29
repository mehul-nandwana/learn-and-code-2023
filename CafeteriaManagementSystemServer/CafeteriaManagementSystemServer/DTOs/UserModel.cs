using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.DTOs
{
    [Serializable]
    public class UserModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
}
