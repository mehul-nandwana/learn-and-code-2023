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
    public class MenuService: IMenuService
    {

        public MenuRepository menuRepository =new MenuRepository(); 
        public FoodRepository foodRepository =new FoodRepository();

        public Response AddMenu(int[] menuData)
        {
            Response response;
            try
            {
                foreach (var menuId in menuData)
                {
                    Menu menu = SetMenuItems(menuId);
                    menuRepository.AddMenu(menu);
                }
                response = new Response(Constant.ADD_FOOD_SUCCESS_MESSAGE, menuData,Constant.CHEF_LOGIN);
            }
            catch (Exception ex)
            {
                 response = new Response(ex.Message, menuData, Constant.USER_LOGIN);
            }
            return response;
        }

        public Response GetMenu(int userId)
        {
            Response response;
            try
            {
                List<string> foodIds = menuRepository.GetMenu(userId);
                List<FoodItem> foods = foodRepository.GetFoodItems(foodIds);
               List< FoodItemData> foodItemData = SetFoodData(foods);
                response = new Response(Constant.SUCCESS_MESSAGE, foodItemData, Constant.SHOW_MENU);
                return response;
            }
            catch (Exception ex)
            {
                response = new Response(ex.Message, Constant.EMPTY_STRING, Constant.USER_LOGIN);
                return response;
            }
        }

        public Response AddChoiceForMenu(ChoiceData choiceData)
        {
            Response response;
            try
            {
                Choice choice = setchoice(choiceData);
                string status = menuRepository.AddChoice(choice);
                response = new Response(status, choiceData.UserId, Constant.EMPLOYEE_LOGIN);
            }
            catch(Exception ex)
            {
                response = new Response(ex.Message, Constant.EMPTY_STRING, Constant.USER_LOGIN);
            }
            return response;
        }

        private Choice setchoice(ChoiceData choiceData) 
        {
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
            int mealType = foodRepository.GetMealTypeId(foodId);
            menu.MealTypeId = mealType;
            menu.FoodId = foodId;
            DateTime currentDateTime = DateTime.Now;
            DateTime nextDay = currentDateTime;
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
