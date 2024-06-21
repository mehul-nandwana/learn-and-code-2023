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
    public class FoodController:ICommonController
    {
        public FoodController foodController;
        public FoodService _foodService = new FoodService();
        public FoodController() { }
       
        public byte[] ExecuteRequest(CustomProtocolParameters<Object> requestData, string method)
        {
         
            Response response = CallMethod(method, requestData.obj.ToString());
            string Output = JsonSerializer.Serialize(response);
            byte[] responseData = Encoding.ASCII.GetBytes(Output);
            return responseData;
        }

        public Response CallMethod(string methodName, string par)
        {
            if (methodName == "addfood")
            {
                Food food = JsonSerializer.Deserialize<Food>(par);
                return _foodService.AddFoodItem(food);

            }
            else if (methodName == "updatefood")
            {
                Food food = JsonSerializer.Deserialize<Food>(par);
                return _foodService.UpdateFoodItem(food);

            }
            else
            {
                int id = JsonSerializer.Deserialize<int>(par);
                return _foodService.DeleteFoodItem(id);

            }

        }
    }
}
