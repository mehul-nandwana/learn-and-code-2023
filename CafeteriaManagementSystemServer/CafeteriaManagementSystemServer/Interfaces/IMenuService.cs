using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Interfaces
{
    public interface IMenuService
    {
        public Response AddMenu(int[] menuData);
        public Response AddChoiceForMenu(ChoiceData choiceData);
        public Response GetMenu(int id);
    }
}
