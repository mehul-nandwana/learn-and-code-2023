using CafeteriaManagementSystemServer.Controller;
using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Services
{
    public class FoodService:IFoodService
    {
        private string Added_Food_Success = "Food Item Added Successfully";
        private string Added_Food_Failure = "Fail To Add Food Item";
        private string Update_Food_Success = "Food item Updated Successfully";
        private string Update_Food_Failure = "Fail to Update Food Item";
        private string Delete_Food_Failure = "Failed to delete Food Item";
        private string Delete_Food_Success = "Successfully deleted Food Item";
        public string  FOOD_ITEM_DELETED = "Food item Deleted";
         NotificationController _notificationController = new NotificationController();

        public FoodRepository _foodRepository = new FoodRepository();

        public Response AddFoodItem(Food food)
        {
            FoodItem foodItem = new FoodItem();
            Response response;
             foodItem = SetFoodAttributes(food,foodItem);
            _foodRepository.AddFood(foodItem);
            try
            {
                 response = new Response(Models.Constant.SUCCESS_MESSAGE, Models.Constant.SUCCESS_STATUS, Added_Food_Success, foodItem, "admin");
                _notificationController.AddNotification(Models.Constant.ADD_FOOD_NOTIFICATION,food.Name + "Added");

            }
            
            catch (Exception ex)
            {
                 response = new Response(ex.Message, Models.Constant.FAILURE_STATUS, Added_Food_Failure, foodItem, "admin");

            }
            return response;
        }
        private FoodItem SetFoodAttributes(Food food, FoodItem foodItem)
        {
            foodItem.Price = food.Price;
            foodItem.Name = food.Name.ToLower();
            foodItem.Availability = food.Availability;
            foodItem.MealTypeId = food.MealTypeId;
            return foodItem;
        }

        public Response UpdateFoodItem(Food food)
        {
            FoodItem foodItem = _foodRepository.GetFood(food.Name);
            foodItem = SetFoodAttributes(food, foodItem);
            _foodRepository.UpdateFoodItem(foodItem); 
            try
            {
                
                Response response = new Response(Models.Constant.SUCCESS_MESSAGE, Models.Constant.SUCCESS_STATUS, Update_Food_Success,foodItem, "admin");
                _notificationController.AddNotification(Models.Constant.UPDATE_FOOD__NOTIFICATION, food.Name + "Updated");

                return response;
            }
            catch(Exception e)
            {
                Response response = new Response(e.Message, Models.Constant.FAILURE_STATUS, Update_Food_Failure, foodItem, "admin");

                return response;
            }
        }

        public Response DeleteFoodItem(int id)
        {
            try    
            {
                _foodRepository.DeleteFood(id);

                Response response = new Response(Models.Constant.SUCCESS_MESSAGE, Models.Constant.SUCCESS_STATUS, Delete_Food_Success, id, "admin");
                _notificationController.AddNotification(Models.Constant.UPDATE_FOOD__NOTIFICATION, FOOD_ITEM_DELETED);

                return response;
            }
            catch(Exception e) 
            {
                Response response = new Response(e.Message, Models.Constant.FAILURE_STATUS, Delete_Food_Failure, id, "admin");
                return response;
            }
        }
    }
}
