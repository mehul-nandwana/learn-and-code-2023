using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using System.Globalization;

namespace CafeteriaManagementSystemServer.Services
{
    public class FoodService : IFoodService
    {

        NotificationService _notificationService = new NotificationService();
        FeedbackRepository _feedbackRepository = new FeedbackRepository();
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
            foodItem.Sweet = food.Sweet;
            foodItem.Spicelevel = food.Spicelevel;
            foodItem.CuisineType = food.CuisineType;
            foodItem.DietaryPreference = food.DietaryPreference;
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

        public Response GetDiscardItem()
        {
            List<int> menuId = new List<int>();
            menuId = _feedbackRepository.GetDiscardItem();
            List<Food> foods = new List<Food>();
            foreach (int id in menuId)
            {
                FoodItem food =_foodRepository.GetFood(id);
                DiscardItem discardItemData = SetDiscardItem(food);
                AddDiscardItem(discardItemData);
                foods.Add(SetFoodAttributes(food));
            }
            response = new Response(Constant.SUCCESSFULL, foods, Constant.SHOW_DISCARD_ITEM);
            return response;
        }

        private DiscardItem SetDiscardItem(FoodItem food)
        {
            DiscardItem discardItemData = new DiscardItem();
            discardItemData.FoodId = food.Id;
            discardItemData.Name = food.Name;
            discardItemData.GetDetailFeedback = false;
            return discardItemData;
        }

        public Response GetDiscardedItemForAddingFeedback(int id)
        {
            DiscardItemData discardItemData = new DiscardItemData();
            List<string> discarditemForFeedback = _foodRepository.GetDiscarditemForFeedback();
            discardItemData.FoodItem = discarditemForFeedback;
            discardItemData.UserId = id;
            response = new Response(Constant.SUCCESSFULL, discardItemData, Constant.SHOW_DISCARD_ITEM_FOR_FEEDBACK);
            return response;
    }

        private void AddDiscardItem(DiscardItem discardItemData)
        {
            _foodRepository.AddDiscardItem(discardItemData);
        }

        private Food SetFoodAttributes(FoodItem fooditem)
        {
            Food food = new Food();
            food.Name = fooditem.Name;
            food.Price = fooditem.Price;
            return food;
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
