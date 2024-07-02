using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Repository
{
    public class MenuRepository: IMenuRepository
    {
        public CafeteriaMangagementSystemContext DbContext;
        public FoodRepository foodRepository = new FoodRepository();
        public MenuRepository() 
        {
            DbContext = new CafeteriaMangagementSystemContext();
        }

        public void AddMenu(Menu menu)
        {
            DbContext.Menus.Add(menu);
            DbContext.SaveChanges();  
        }

        public List<string> GetMenu(int userId)
        {
            List<int> listOfFoodId = new List<int>();
            List<FoodItem> foodItems = new List<FoodItem>();
            UserPreference userPreference = DbContext.UserPreferences.Where(x=>x.UserId == userId).FirstOrDefault();
            DateTime currentDate = DateTime.Now.Date;
            var itemsForCurrentDate = DbContext.Menus
                .Where(item => item.Date.Date == currentDate )
                .ToList();

            if (itemsForCurrentDate.Count == 0)
                throw new NoMenuItem();
            foreach(Menu item in itemsForCurrentDate)
            {

                listOfFoodId.Add(item.FoodId);
                FoodItem foodItem = foodRepository.GetFood(item.FoodId);
                foodItems.Add(foodItem);
            }

            var orderedFoodItems = foodItems
                 .OrderByDescending(food =>
                     (userPreference.IsVegeterian == food.DietaryPreference ? 1 : 0) +
                     (userPreference.SpiceLevel >= food.Spicelevel ? 1 : 0) +
                     (userPreference.Sweet == food.Sweet ? 1 : 0) +
                     (userPreference.CuisineType == food.CuisineType ? 1 : 0))
                 .ToList();
            return SetFoodItem(orderedFoodItems);
            // Display the filtered food items
            

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
            int mealId = foodRepository.GetMealTypeId(choice.Id);
            choice.MealTypeId = mealId;
            Choice _choice = DbContext.Choices.Where(x => x.Time.Date == DateTime.Now.Date && x.MealTypeId == choice.MealTypeId).FirstOrDefault();
            if (_choice != null)
            {
                UpdateChoice(choice.Id, _choice);
                return "Updated Choice Successfully";
            }
            else
            {
                DbContext.Choices.Add(choice);
                DbContext.SaveChanges();
                return "Added the Choice Successfully";
            }
            
        }
        private void UpdateChoice(int id, Choice choice)
        {
            Choice _choice = DbContext.Choices.FirstOrDefault(x => x.Id == id);
            _choice.MenuId = choice.MenuId;
            DbContext.Choices.Update(_choice);
            DbContext.SaveChanges();
        }
    }
}
