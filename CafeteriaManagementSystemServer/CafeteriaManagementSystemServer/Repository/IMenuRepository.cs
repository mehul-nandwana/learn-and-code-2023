using CafeteriaManagementSystemServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Repository
{
    public interface IMenuRepository
    {
        public void AddChoice(Choice choice);
        public List<int> GetMenu();
        public void AddMenu(Menu menu);
    }
}
