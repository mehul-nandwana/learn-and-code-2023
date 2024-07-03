using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeteriaManagementSystemServer.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly CafeteriaMangagementSystemContext _dbContext;
        private readonly FoodRepository _foodRepository;
        private readonly FeedbackRepository _feedbackRepository;

        public MenuRepository()
        {
            _dbContext = new CafeteriaMangagementSystemContext();
            _foodRepository = new FoodRepository();
            _feedbackRepository = new FeedbackRepository();
        }

        public void AddMenu(Menu menu)
        {
            _dbContext.Menus.Add(menu);
            _dbContext.SaveChanges();
        }

        public List<string> GetMenu(int userId)
        {
            List<int> listOfFoodId = new List<int>();
            List<FoodItem> foodItemList = new List<FoodItem>();
            List<string> foodList = new List<string>(); 
           var userPreference = _dbContext.UserPreferences.FirstOrDefault(x => x.UserId == userId);
            var currentDate = DateTime.Now.Date;
            var itemsForCurrentDate = _dbContext.Menus
                .Where(item => item.Date.Date == currentDate)
                .ToList();

            if (itemsForCurrentDate.Count==0)
                return foodList;
            foreach (Menu item in itemsForCurrentDate)
            {

                listOfFoodId.Add(item.FoodId);
                FoodItem foodItem = _foodRepository.GetFood(item.FoodId);
                foodItemList.Add(foodItem);
            }



            var orderedFoodItems = foodItemList
    .Where(food => food != null && food.DietaryPreference != null && food.Spicelevel != null && food.Sweet != null && food.CuisineType != null) // Ensure food and its properties are not null
    .OrderByDescending(food =>
        (userPreference.IsVegeterian == food.DietaryPreference ? 1 : 0) +
        (userPreference.SpiceLevel >= food.Spicelevel ? 1 : 0) +
        (userPreference.Sweet == food.Sweet ? 1 : 0) +
        (userPreference.CuisineType == food.CuisineType ? 1 : 0))
    .ToList();//var orderedFoodItems = foodItemList
            //     .OrderByDescending(food =>
            //         (userPreference.IsVegeterian == food.DietaryPreference ? 1 : 0) +
            //         (userPreference.SpiceLevel >= food.Spicelevel ? 1 : 0) +
            //         (userPreference.Sweet == food.Sweet ? 1 : 0) +
            //         (userPreference.CuisineType == food.CuisineType ? 1 : 0))
            //     .ToList();
            return SetFoodItem(orderedFoodItems);      
        }

        public List<string> SetFoodItem(List<FoodItem> preferredFoodItems)
        {
            List<string> foodItem = new List<string>();
            foreach (var food in preferredFoodItems)
            {
                foodItem.Add(food.Name);
            }
            return foodItem;
        }

        public string AddChoice(Choice choice)
        {
            var mealId = _foodRepository.GetMealTypeId(choice.MenuId);
            choice.MealTypeId = mealId;
            var existingChoice = _dbContext.Choices
                .FirstOrDefault(x => x.Time.Date == DateTime.Now.Date && x.MealTypeId == choice.MealTypeId);

            if (existingChoice != null)
            {
                UpdateChoice(existingChoice.Id, choice);
                return Constant.CHOICE_UPDATED_SUCCESSFULLY;
            }
            else
            {
                _dbContext.Choices.Add(choice);
                _dbContext.SaveChanges();
                return Constant.CHOICE_ADDED_SUCCESSFULLY;
            }
        }

        private void UpdateChoice(int id, Choice choice)
        {
            var existingChoice = _dbContext.Choices.FirstOrDefault(x => x.Id == id);
            if (existingChoice != null)
            {
                existingChoice.MenuId = choice.MenuId;
                _dbContext.Choices.Update(existingChoice);
                _dbContext.SaveChanges();
            }
        }
    }
}
