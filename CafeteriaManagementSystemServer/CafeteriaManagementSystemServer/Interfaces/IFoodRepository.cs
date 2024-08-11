using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeteriaManagementSystemServer.Models;

namespace CafeteriaManagementSystemServer.Interfaces
{
    public interface IFoodRepository
    {
        public int AddFood(FoodItem food);
        public FoodItem GetFood(string name);
        public int UpdateFoodItem(FoodItem foodItem);
        public int DeleteFood(int id);

    }
}
