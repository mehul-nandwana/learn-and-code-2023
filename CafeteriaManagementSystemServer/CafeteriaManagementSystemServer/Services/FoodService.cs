using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using System.Globalization;

namespace CafeteriaManagementSystemServer.Services
{
    public class FoodService : IFoodService
    {
        private readonly NotificationService _notificationService;
        private readonly FeedbackRepository _feedbackRepository;
        private readonly FoodRepository _foodRepository;

        public FoodService()
        {
            _notificationService = new NotificationService();
            _feedbackRepository = new FeedbackRepository();
            _foodRepository = new FoodRepository();
        }

        public Response AddFoodItem(Food food)
        {
            var foodItem = SetFoodAttributes(food, new FoodItem());
            try
            {
                _foodRepository.AddFood(foodItem);
                _notificationService.AddNotification(Constant.ADD_FOOD_NOTIFICATION, CreateAddFoodMessage(food.Name));
                return new Response(Constant.ADDED_FOOD_SUCCCESS, foodItem, Constant.ADMIN_LOGIN);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, foodItem, Constant.USER_LOGIN);
            }
        }

        public Response UpdateFoodItem(Food food)
        {
            var foodItem = _foodRepository.GetFood(food.Name);

            foodItem = SetFoodAttributes(food, foodItem);
            try
            {
                _foodRepository.UpdateFoodItem(foodItem);
                _notificationService.AddNotification(Constant.UPDATE_FOOD_NOTIFICATION, CreateUpdateFoodMessage(food.Name));
                return new Response(Constant.UPDATE_FOOD_SUCCESS, foodItem, Constant.ADMIN_LOGIN);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, foodItem, Constant.USER_LOGIN);
            }
        }

        public Response DeleteFoodItem(int id)
        {
            try
            {
                _foodRepository.DeleteFood(id);
                _notificationService.AddNotification(Constant.DELETE_FOOD_NOTIFICATION, CreateDeleteFoodMessage(id));
                return new Response(Constant.DELETE_FOOD_SUCCESS, id, Constant.ADMIN_LOGIN);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, id, Constant.USER_LOGIN);
            }
        }

        public Response GetDiscardItem()
        {
            try
            {
                var menuIds = _feedbackRepository.GetDiscardItem();
                var foods = new List<Food>();

                if (menuIds.Count ==0) {
                    return new Response(Constant.NO_DATA_FOR_DISCARD_ITEM, foods, Constant.SHOW_DISCARD_ITEM);

                }

                foreach (var id in menuIds)
                {
                    var foodItem = _foodRepository.GetFood(id);
                    if (foodItem != null)
                    {
                        var discardItemData = SetDiscardItem(foodItem);
                        AddDiscardItem(discardItemData);
                        foods.Add(MapFoodItemToFood(foodItem));
                    }
                    else
                    {
                        throw new FoodNotFound();
                    }
                }

                return new Response(Constant.SUCCESSFULL, foods, Constant.SHOW_DISCARD_ITEM);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, Constant.EMPTY_STRING, Constant.USER_LOGIN);
            }
        }

        public Response GetDiscardedItemForAddingFeedback(int userId)
        {
            try
            {
                var discardItemData = new DiscardItemData
                {
                    FoodItem = _foodRepository.GetDiscardItemForFeedback(),
                    UserId = userId
                };

                if(discardItemData.FoodItem.Count == 0)
                    return new Response(Constant.NO_DATA_FOR_DISCARD_ITEM, discardItemData, Constant.CHEF_LOGIN);


                return new Response(Constant.SUCCESSFULL, discardItemData, Constant.SHOW_DISCARD_ITEM_FOR_FEEDBACK);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, userId, Constant.USER_LOGIN);
            }
        }

        private FoodItem SetFoodAttributes(Food food, FoodItem foodItem)
        {
            foodItem.Price = food.Price;
            foodItem.Name = food.Name.ToLower(CultureInfo.InvariantCulture);
            foodItem.Availability = food.Availability;
            foodItem.MealTypeId = food.MealTypeId;
            foodItem.Sweet = food.Sweet;
            foodItem.Spicelevel = food.Spicelevel;
            foodItem.CuisineType = food.CuisineType;
            foodItem.DietaryPreference = food.DietaryPreference;
            return foodItem;
        }

        private DiscardItem SetDiscardItem(FoodItem food)
        {
            return new DiscardItem
            {
                FoodId = food.Id,
                Name = food.Name,
                GetDetailFeedback = false
            };
        }

        private void AddDiscardItem(DiscardItem discardItemData)
        {
            _foodRepository.AddDiscardItem(discardItemData);
        }

        private Food MapFoodItemToFood(FoodItem foodItem)
        {
            return new Food
            {
                Name = foodItem.Name,
                Price = foodItem.Price
            };
        }

        private string CreateAddFoodMessage(string foodName)
        {
            return $"{foodName} has been added to the menu.";
        }

        private string CreateUpdateFoodMessage(string foodName)
        {
            return $"{foodName} has been updated in the menu.";
        }

        private string CreateDeleteFoodMessage(int foodId)
        {
            return $"Food with ID {foodId} has been deleted.";
        }
    }
}
