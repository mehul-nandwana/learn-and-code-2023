using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Interfaces;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;

namespace CafeteriaManagementSystemServer.Services
{
    public class MenuService : IMenuService
    {
        private readonly MenuRepository _menuRepository;
        private readonly FoodRepository _foodRepository;
        private readonly FeedbackRepository _feedbackRepository;
        private readonly RecommendationService _recommendationService;

        public MenuService()
        {
            _menuRepository = new MenuRepository();
            _foodRepository = new FoodRepository();
            _feedbackRepository = new FeedbackRepository();
            _recommendationService = new RecommendationService();
        }

        public Response AddMenu(int[] menuData)
        {
            try
            {
                foreach (var menuId in menuData)
                {
                    var menu = CreateMenu(menuId);
                    _menuRepository.AddMenu(menu);
                }

                return new Response(Constant.ADD_FOOD_SUCCESS_MESSAGE, menuData, Constant.CHEF_LOGIN);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, menuData, Constant.CHEF_LOGIN);
            }
        }

        public Response GetMenu(int userId)
        {
            try
            {
                var foodIds = _menuRepository.GetMenu(userId);
                int size = foodIds.Count;
                if(size == 0) {
                    return new Response(Constant.NO_DATA_FOR_MENU_ITEM, userId, Constant.EMPLOYEE_LOGIN);
                }
                var foodItems = _foodRepository.GetFoodItems(foodIds);
                var foodItemData = MapToFoodItemData(foodItems,userId);

                return new Response(Constant.SHOW_MENU_ITEMS, foodItemData, Constant.SHOW_MENU);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, Constant.EMPTY_STRING, Constant.EMPLOYEE_LOGIN);
            }
        }

        public Response AddChoiceForMenu(ChoiceData choiceData)
        {
            try
            {
                var choice = MapToChoice(choiceData);
                var addChoiceResponseStatus = _menuRepository.AddChoice(choice);

                return new Response(addChoiceResponseStatus, choiceData.UserId, Constant.EMPLOYEE_LOGIN);
            }
            catch (Exception ex)
            {
                return new Response(ex.Message, Constant.EMPTY_STRING, Constant.EMPLOYEE_LOGIN);
            }
        }

        public Response GetItemsForFeedback(int userId)
        {
            var menuItems = _menuRepository.GetItemsForFeedback();
            DataItem itemsForFeedback = new DataItem();
            itemsForFeedback.Message = menuItems;
            itemsForFeedback.userId = userId;
            return new Response(Constant.SUCCESSFULL, itemsForFeedback, Constant.ADD_FEEDBACK);
        }

        private Choice MapToChoice(ChoiceData choiceData)
        {
            return new Choice
            {
                IsChoiceAdded = true,
                UserId = choiceData.UserId,
                MenuId = choiceData.MenuId,
                MealTypeId = choiceData.MealId,
                Time = DateTime.Now
            };
        }

        private Menu CreateMenu(int foodId)
        {
            var mealTypeId = _foodRepository.GetMealTypeId(foodId);
            return new Menu
            {
                MealTypeId = mealTypeId,
                FoodId = foodId,
                Date = DateTime.Now,
                IsPrepared = false
            };
        }

        private ShowMenuItemData MapToFoodItemData(List<FoodItem> foods, int id)
        {
            ShowMenuItemData showMenuItemData = new ShowMenuItemData();
            List<FoodItemData> foodItemDataList = new List<FoodItemData>();

            foreach (var food in foods)
            {
                var averageRating = _feedbackRepository.AverageFeedback(food.Id);
                var averageSentiment = _feedbackRepository.AverageSentiment(food.Id);
                string sentiment = _recommendationService.GetSentiment(averageSentiment);
                var foodItemData = new FoodItemData
                {
                    Name = food.Name,
                    Price = food.Price,
                    Id = food.Id,
                    AverageRating = averageRating,
                    MealType = food.MealTypeId,
                    AverageSentiment = sentiment,
                };

                foodItemDataList.Add(foodItemData);
            }
            showMenuItemData.FoodItem = foodItemDataList;
            showMenuItemData.UserId = id;
            return showMenuItemData;
        }
    }
}
