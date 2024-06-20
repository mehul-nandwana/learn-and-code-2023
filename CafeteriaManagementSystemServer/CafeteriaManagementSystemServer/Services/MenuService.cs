using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Services
{
    public class MenuService
    {
        private string ADDED_MENU_SUCCESS = "Menu Item Added Successfully";
        private string GET_MENU_SUCCESS = "Menu Item Added Successfully";
        private string CHOICE_ADDED = "Choice Added";

        public MenuRepository _menuRepository =new MenuRepository(); 
        public Response AddMenu(MenuData menuData)
        {
            Response response;
            try
            {
                Menu menu = SetMenuItems(menuData);
                _menuRepository.AddMenu(menu);
                response = new Response(Constant.SUCCESS_MESSAGE, Constant.SUCCESS_STATUS, ADDED_MENU_SUCCESS, menuData, "LoginPage");
            }
            catch (Exception ex)
            {
                 response = new Response(Constant.FAILURE_MESSAGE, Constant.FAILURE_STATUS, ex.Message, menuData, "LoginPage");

            }
            return response;

        }
        public Response GetMenu(int id)
        {
            Response response;
            try
            {
                List<Menu> menu = _menuRepository.GetMenu(id);
                response = new Response(Constant.SUCCESS_MESSAGE, Constant.SUCCESS_STATUS, GET_MENU_SUCCESS, menu, "employee");
                return response;
            }
            catch (Exception ex)
            {
                response = new Response(Constant.FAILURE_MESSAGE, Constant.FAILURE_STATUS, ex.Message, "", "employee");
                return response;
            }
        }

        public Response AddChoice(ChoiceData choiceData)
        {
            Response response;
            try
            {
                Choice choice = setchoice(choiceData);
                _menuRepository.AddChoice(choice);
                response = new Response(Constant.SUCCESS_MESSAGE, Constant.SUCCESS_STATUS, CHOICE_ADDED, choiceData, "employee");
            }
            catch(Exception ex)
            {
                response = new Response(Constant.FAILURE_MESSAGE, Constant.FAILURE_STATUS, ex.Message, "", "employee");

            }
            return response;
        }
        private Choice setchoice(ChoiceData choiceData) {
        Choice choice = new Choice();
            choice.IsChoiceAdded = true;
            choice.UserId = choiceData.UserId;
            choice.MenuId = choiceData.MenuId;
            choice.MealTypeId = choiceData.MealId;
            choice.Time = DateTime.Now;
            return choice;
        
        }
        private Menu SetMenuItems(MenuData menuData)
        {
            Menu menu = new Menu();
            menu.MealTypeId = menuData.MeaId;
            menu.FoodId = menu.FoodId;
            DateTime currentDateTime = DateTime.Now;
            DateTime nextDay = currentDateTime.AddDays(1);
            menu.Date = nextDay;
            menu.IsPrepared = false;
            return menu;
        }
    }
}
