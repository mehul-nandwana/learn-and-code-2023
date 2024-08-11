using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.DTOs
{
    public class CustomProtocolParameters
    {
        public string StatusMessage { get; set; }
        public object Obj { get; set; }
        public string Method { get; set; }
    }
}
