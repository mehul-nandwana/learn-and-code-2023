using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Controller
{
    public class FoodController:IFoodController,ICommonController
    {
        public FoodController foodController;
        public FoodService _foodService = new FoodService();
        public FoodController() { }
        //public FoodController(IFoodService foodService) {
        //    _foodService = foodService;
        //}

        public Response AddFood(Food food)
        {
            Response result;
            result = _foodService.AddFoodItem(food);
            return result;
                

        }
        public Response UpdateFood(Food food)
        {
            Response result;
            result = _foodService.UpdateFoodItem(food);
            return result;
        }
        public Response DeleteFood(int id)
        {
            Response result;
            result = _foodService.DeleteFoodItem(id);
            return result;

        }
        public byte[] ExecuteRequest(CustomProtocolParameters<Object> requestData, string method)
        {
         
            Response response = CallMethod(foodController, method, requestData.obj);
            string Output = JsonSerializer.Serialize(response);
            byte[] responseData = Encoding.ASCII.GetBytes(Output);
            return responseData;
        }

        public static Response CallMethod(object obj, string methodName, object par)
        {
            Type type = obj.GetType();
            MethodInfo method = type.GetMethod(methodName);

            if (method != null)
            {
                return (Response)method.Invoke(methodName, new[] { par });
            }
            else
            {
                Console.WriteLine($"Method '{methodName}' not found in type '{type.FullName}'");
                return null;
            }
        }
    }
}
