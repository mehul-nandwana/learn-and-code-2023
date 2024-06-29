using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using System.Globalization;

namespace CafeteriaManagementSystemServer.Services
{
    public class FoodService : IFoodService
    {

        NotificationService _notificationService = new NotificationService();
        FoodRepository _foodRepository = new FoodRepository();
        Response response;

        public Response AddFoodItem(Food food)
        {
            FoodItem foodItem = new FoodItem();
            foodItem = SetFoodAttributes(food, foodItem);
            _foodRepository.AddFood(foodItem);
            try
            {
                response = new Response(Models.Constant.ADDED_FOOD_SUCCCESS, foodItem, Constant.ADMIN_LOGIN);
                _notificationService.AddNotification(Models.Constant.ADD_FOOD_NOTIFICATION, AddFoodMessage(food.Name));
            }

            catch (Exception ex)
            {
                response = new Response(ex.Message, foodItem, Constant.USER_LOGIN);
            }
            return response;
        }

        private FoodItem SetFoodAttributes(Food food, FoodItem foodItem)
        {
            foodItem.Price = food.Price;
            foodItem.Name = food.Name.ToLower();
            foodItem.Availability = food.Availability;
            foodItem.MealTypeId = food.MealTypeId;
            return foodItem;
        }

        public Response UpdateFoodItem(Food food)
        {
            FoodItem foodItem = _foodRepository.GetFood(food.Name);
            foodItem = SetFoodAttributes(food, foodItem);
            _foodRepository.UpdateFoodItem(foodItem);
            try
            {

                response = new Response(Models.Constant.UPDATE_FOOD_SUCCESS, foodItem, Constant.ADMIN_LOGIN);
                _notificationService.AddNotification(Models.Constant.UPDATE_FOOD__NOTIFICATION, UpdateFoodMessage(food.Name));
                return response;
            }
            catch (Exception e)
            {
                response = new Response(e.Message, foodItem, Constant.USER_LOGIN);
                return response;
            }
        }

        public Response DeleteFoodItem(int id)
        {
            try
            {
                _foodRepository.DeleteFood(id);
                response = new Response(Models.Constant.DELETE_FOOD_SUCCESS, id, Constant.ADMIN_LOGIN);
                _notificationService.AddNotification(Models.Constant.DELETE_FOOD__NOTIFICATION, DeleteFoodMessage(id)); }
            catch (Exception e)
            {
                response = new Response(e.Message, id, Constant.USER_LOGIN);
            }
            return response;
        }

        private string AddFoodMessage(String food)
        {
            return food + " is Added to the Menu";
        }

        private string UpdateFoodMessage(String food)
        {
            return food + " has been been " + " Updated in the Menu";
        }

        private string DeleteFoodMessage(int foodid)
        {
            return foodid + " has been been " + " deleted from the Menu";
        }

    }

}
