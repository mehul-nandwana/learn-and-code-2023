using CafeteriaManagementSystemServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Interfaces
{
    public interface IMenuRepository
    {
        public string AddChoice(Choice choice);
        public List<string> GetMenu(int userId);
        public void AddMenu(Menu menu);
    }
}
