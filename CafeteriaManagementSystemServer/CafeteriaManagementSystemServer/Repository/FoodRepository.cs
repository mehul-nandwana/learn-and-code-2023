using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeteriaManagementSystemServer.Repository
{
    public class FoodRepository:IFoodRepository
    {
        public CafeteriaMangagementSystemContext DbContext;

        public FoodRepository()
        {
            DbContext = new CafeteriaMangagementSystemContext();
        }

        public int AddFood(FoodItem food)
        {
            DbContext.FoodItems.Add(food);
           int areChangesSaved = DbContext.SaveChanges();
           return areChangesSaved;
        }

        public FoodItem GetFood(string name)
        {
            FoodItem foodItem = DbContext.FoodItems.Where(f=>f.Name  == name).FirstOrDefault();
            if (foodItem == null)
            {
                throw new FoodNotFound();
            }
            return foodItem;
        }
        public FoodItem GetFood(int id)
        {
            FoodItem foodItem = DbContext.FoodItems.Where(f => f.Id == id).FirstOrDefault();
            if (foodItem == null)
            {
                throw new FoodNotFound();
            }
            return foodItem;
        }
        public int UpdateFoodItem(FoodItem foodItem)
        {
            FoodItem food = DbContext.FoodItems.Where(f => f.Name == foodItem.Name).FirstOrDefault();
            if (food == null)
                throw new FoodNotFound();

            food.Price = foodItem.Price;
            food.Availability = foodItem.Availability;
            DbContext.FoodItems.Update(food);
            DbContext.SaveChanges();
            return 1;
        }

        public int DeleteFood(int id)
        {
            var foodItem = DbContext.FoodItems.FirstOrDefault(f => f.Id == id);
            int areChangesSaved = 0;
            if (foodItem != null)
            {
                DbContext.FoodItems.Remove(foodItem);
                areChangesSaved = DbContext.SaveChanges();
            }
            else
            {
                throw new FoodNotFound();
            }
            return areChangesSaved;
        }

        public int GetMealTypeId(int foodId)
        {
            FoodItem food = DbContext.FoodItems.Where(x=>x.Id== foodId).FirstOrDefault();
            if (food == null)
                throw new FoodNotFound();
            else
            return food.MealTypeId;
        }

        public List<FoodItem> GetFoodItems(List<string> foodnames)
        {
            List<FoodItem> fooditems = new List<FoodItem>();
            foreach(string foodname in foodnames)
            {
                FoodItem food = DbContext.FoodItems.SingleOrDefault(x => x.Name == foodname);
                fooditems.Add(food);
            }
            return fooditems;
        }

        public void CollectFeedbackOnDiscard(int id)
        {
            DiscardItem discardItem = DbContext.DiscardItems.SingleOrDefault(x => x.Id == id);
            if (discardItem != null)
            {
                discardItem.GetDetailFeedback = true;
                DbContext.SaveChanges();
            }
        }

        public void AddDiscardItem(DiscardItem discardItem)
        {
            DiscardItem discardedItem = DbContext.DiscardItems.FirstOrDefault(x => x.FoodId == discardItem.FoodId);
            if (discardedItem == null)
            {
                DbContext.DiscardItems.Add(discardItem);
                DbContext.SaveChanges();
            }
        }

        public List<string> GetDiscarditemForFeedback()
        {
            List<DiscardItem> discardfooditems = DbContext.DiscardItems.ToList();
            List<string> listOfFoodItem = new List<string>();
            foreach (var foodItem in discardfooditems)
            {
                if(foodItem.GetDetailFeedback == true)
                    listOfFoodItem.Add(foodItem.Name.ToLower());
            }
            return listOfFoodItem;
        }

    }
}
