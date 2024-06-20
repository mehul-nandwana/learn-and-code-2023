using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Controller
{
    public class MenuController
    {
        MenuService menuService =new MenuService();

        public void AddMenu(MenuData menuData)
        {
            menuService.AddMenu(menuData);
        }
        public void AddChoice(ChoiceData choice)
        {
            menuService.AddChoice(choice);

        }
        public void GetMenu(int id)
        {
            menuService.GetMenu(id);
        }
    }
}
