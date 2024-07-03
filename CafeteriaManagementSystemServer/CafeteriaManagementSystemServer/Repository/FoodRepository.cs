using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CafeteriaManagementSystemServer.Repository
{
    public class FoodRepository : IFoodRepository
    {
        private readonly CafeteriaMangagementSystemContext _dbContext;

        public FoodRepository()
        {
            _dbContext = new CafeteriaMangagementSystemContext();
        }

        public int AddFood(FoodItem food)
        {
            _dbContext.FoodItems.Add(food);
            return _dbContext.SaveChanges();
        }

        public FoodItem GetFood(string name)
        {
            var foodItem = _dbContext.FoodItems.FirstOrDefault(f => f.Name == name);
            if (foodItem == null)
            {
                throw new FoodNotFound();
            }
            return foodItem;
        }

        public FoodItem GetFood(int id)
        {
            var foodItem = _dbContext.FoodItems.FirstOrDefault(f => f.Id == id);        
            return foodItem;
        }

        public int UpdateFoodItem(FoodItem foodItem)
        {
            var food = _dbContext.FoodItems.FirstOrDefault(f => f.Name == foodItem.Name);
            if (food == null)
            {
                throw new FoodNotFound();
            }

            food.Price = foodItem.Price;
            food.Availability = foodItem.Availability;
            _dbContext.FoodItems.Update(food);
            return _dbContext.SaveChanges();
        }

        public int DeleteFood(int id)
        {
            var feedbacks = _dbContext.Feedbacks.Where(f => f.MenuId == id).ToList();
            var discardItem = _dbContext.DiscardItems.Where(f=>f.FoodId == id).ToList();
            if (discardItem.Any())
            {
                _dbContext.DiscardItems.RemoveRange(discardItem);
                _dbContext.SaveChanges();
            }
            if (feedbacks.Any())
            {
                _dbContext.Feedbacks.RemoveRange(feedbacks);
                _dbContext.SaveChanges();
            }

            var foodItem = _dbContext.FoodItems.FirstOrDefault(f => f.Id == id);
            if (foodItem == null)
            {
                throw new FoodNotFound();
            }

            _dbContext.FoodItems.Remove(foodItem);
            return _dbContext.SaveChanges();
        }

        public int GetMealTypeId(int foodId)
        {
            var food = _dbContext.FoodItems.FirstOrDefault(f => f.Id == foodId);
            if (food == null)
            {
                throw new FoodNotFound();
            }
            return food.MealTypeId;
        }

        public List<FoodItem> GetFoodItems(List<string> foodNames)
        {
            List<FoodItem> fooditems = new List<FoodItem>();
            foreach (string foodname in foodNames)
            {
                FoodItem food = _dbContext.FoodItems.FirstOrDefault(x => x.Name == foodname);
                fooditems.Add(food);
            }
            return fooditems;
        }

        public void CollectFeedbackOnDiscard(int id)
        {
            var discardItem = _dbContext.DiscardItems.FirstOrDefault(d => d.Id == id);
            if (discardItem != null)
            {
                discardItem.GetDetailFeedback = true;
                _dbContext.SaveChanges();
            }
            else
                throw new FoodNotFound();
        }

        public void AddDiscardItem(DiscardItem discardItem)
        {
            var existingDiscardItem = _dbContext.DiscardItems.FirstOrDefault(d => d.FoodId == discardItem.FoodId);
            if (existingDiscardItem == null)
            {
                _dbContext.DiscardItems.Add(discardItem);
                _dbContext.SaveChanges();
            }
          
        }

        public List<string> GetDiscardItemForFeedback()
        {
            return _dbContext.DiscardItems
        .Where(d => d.GetDetailFeedback)
        .AsEnumerable() 
        .Select(d => d.Name.ToLower())
        .ToList();
        }
    }
}
