using CafeteriaManagementSystemServer.DTOs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Reflection;
using System.Security.AccessControl;
using System.Reflection.Metadata;
using CafeteriaManagementSystemServer.Models;

namespace CafeteriaManagementSystemServer.Controller
{
    public class AdminController: ICommonController
    {
        public FoodController _foodController = new FoodController();
        
        public Response AddFoodItem(Food foodItem)
        {
            Response response = _foodController.AddFood(foodItem);
            return response;
        }
        public Response UpdateFoodItem(Food foodItem)
        {
            Response response = _foodController.UpdateFood(foodItem);
            return response;
        }
        public Response DeleteFoodItem(int id)
        {
            Response response = _foodController.DeleteFood(id);
            return response;
        }

       
        public byte[] ExecuteRequest(CustomProtocolParameters<Object> requestData, string method)
        {            
            Response response = CallMethod( method,requestData.obj.ToString());
            string Output = JsonSerializer.Serialize(response);
            byte[] responseData = Encoding.ASCII.GetBytes(Output);
            return responseData;
        }

        public Response CallMethod(string methodName,string par)
        {
            if (methodName == "addfood")
            {
               Food food = JsonSerializer.Deserialize<Food>(par);
                return _foodController.AddFood(food);

            }
            else if(methodName == "updatefood")
            {
                Food food = JsonSerializer.Deserialize<Food>(par);
                return _foodController.UpdateFood(food);

            }
            else
            {
                int id = JsonSerializer.Deserialize<int>(par);
                return _foodController.DeleteFood(id);

            }

        }
    }
}
