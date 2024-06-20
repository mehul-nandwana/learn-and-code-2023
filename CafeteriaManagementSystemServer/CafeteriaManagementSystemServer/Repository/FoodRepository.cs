using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            return foodItem;
        }

        public int UpdateFoodItem(FoodItem foodItem)
        {
            FoodItem food = DbContext.FoodItems.Where(f => f.Name == foodItem.Name).FirstOrDefault();
            food.Price = foodItem.Price;
            food.Availability = foodItem.Availability;
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
            return areChangesSaved;

        }
    }
}
