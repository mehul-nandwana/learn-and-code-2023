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
        public FoodRepository _foodRepository =new FoodRepository();
        public Response AddMenu(int[] menuData)
        {
            Response response;
            try
            {
                foreach (var menuId in menuData)
                {
                    Menu menu = SetMenuItems(menuId);
                    _menuRepository.AddMenu(menu);
                }
                response = new Response(Constant.SUCCESS_MESSAGE, Constant.SUCCESS_STATUS, ADDED_MENU_SUCCESS, menuData, "chef");
            }
            catch (Exception ex)
            {
                 response = new Response(Constant.FAILURE_MESSAGE, Constant.FAILURE_STATUS, ex.Message, menuData, "chef");

            }
            return response;

        }
        public Response GetMenu()
        {
            Response response;
            try
            {
                List<int> foodIds = _menuRepository.GetMenu();
                List<FoodItem> foods = _foodRepository.GetFoodItems(foodIds);
               List< FoodItemData> foodItemData = SetFoodData(foods);
                response = new Response(Constant.SUCCESS_MESSAGE, Constant.SUCCESS_STATUS, GET_MENU_SUCCESS, foods, "showmenu");
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
        private Menu SetMenuItems(int foodId)
        {
            Menu menu = new Menu();
            int mealType = _foodRepository.GetMealTypeId(foodId);
            menu.MealTypeId = mealType;
            menu.FoodId = foodId;
            DateTime currentDateTime = DateTime.Now;
            DateTime nextDay = currentDateTime.AddDays(1);
            menu.Date = nextDay;
            menu.IsPrepared = false;
            return menu;
        }

        private List<FoodItemData> SetFoodData(List<FoodItem> foods)
        {
            List<FoodItemData> data = new List<FoodItemData>();
            foreach(var datum in foods)
            {
                FoodItemData foodItemData = new FoodItemData();
                foodItemData.Name = datum.Name;
                foodItemData.Price = datum.Price;
                foodItemData.Id = datum.Id;
                foodItemData.MealType = datum.MealTypeId;

                data.Add(foodItemData);
            }
            return data;
        }
    }
}
