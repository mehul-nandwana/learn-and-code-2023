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
            var userPreference = _dbContext.UserPreferences.FirstOrDefault(x => x.UserId == userId);
            var currentDate = DateTime.Now.Date;
            var itemsForCurrentDate = _dbContext.Menus
                .Where(item => item.Date.Date == currentDate)
                .ToList();

            if (!itemsForCurrentDate.Any())
                throw new NoMenuItem();

            var foodItems = itemsForCurrentDate
                .Select(item => _foodRepository.GetFood(item.FoodId))
                .ToList();

            var orderedFoodItems = foodItems
                .OrderByDescending(food =>
                    (userPreference.IsVegeterian == food.DietaryPreference ? 1 : 0) +
                    (userPreference.SpiceLevel >= food.Spicelevel ? 1 : 0) +
                    (userPreference.Sweet == food.Sweet ? 1 : 0) +
                    (userPreference.CuisineType == food.CuisineType ? 1 : 0))
                .ToList();

            return orderedFoodItems.Select(food => food.Name).ToList();
        }

        public string AddChoice(Choice choice)
        {
            var mealId = _foodRepository.GetMealTypeId(choice.Id);
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
