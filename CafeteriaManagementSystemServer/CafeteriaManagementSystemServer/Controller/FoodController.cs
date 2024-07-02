using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using System.Text.Json;

namespace CafeteriaManagementSystemServer.Controller
{
    public class FoodController : ICommonController,IFoodController
    {
        public FoodController foodController;
        public FoodService _foodService = new FoodService();
        //public FoodController(IFoodService foodService) {
        //    _foodService = foodService;
        //}

        public Response AddFoodItem(Food food)
        {
           return _foodService.AddFoodItem(food);
        }

        public override Response CallMethod(CustomProtocolParameters customProtocolParameters)
        {
            string method = customProtocolParameters.Method;
            string parameter = customProtocolParameters.Obj.ToString();

            if (method == "addfood")
            {
                Food food = JsonSerializer.Deserialize<Food>(parameter);
                return _foodService.AddFoodItem(food);
            }
            else if (method == "updatefood")
            {
                Food food = JsonSerializer.Deserialize<Food>(parameter);
                return _foodService.UpdateFoodItem(food);
            }
            else if (method == "deletefood")
            {
                int id = JsonSerializer.Deserialize<int>(parameter);
                return _foodService.DeleteFoodItem(id);
            }
            else if(method == "getdiscardItem")
            {
                return _foodService.GetDiscardItem();

            }
            else if(method == "getdiscardItemforwhichfeedbackcanbeadded")
            {
                int id = Convert.ToInt32((parameter));
                return _foodService.GetDiscardedItemForAddingFeedback(id);
            }
            else
                throw new ControllerNotfound();
        }
    }
}
