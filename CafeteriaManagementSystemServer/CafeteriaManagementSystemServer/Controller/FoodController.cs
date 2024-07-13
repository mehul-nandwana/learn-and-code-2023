using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using System.Reflection.Metadata;
using System.Text.Json;
using Constant = CafeteriaManagementSystemServer.Models.Constant;

namespace CafeteriaManagementSystemServer.Controller
{
    public class FoodController : ICommonController
    {
        private readonly FoodService _foodService;
        private readonly JSonSerializer _jsonSerializer;

        public FoodController()
        {
            _foodService = new FoodService();
            _jsonSerializer = new JSonSerializer();
        }

        public override Response CallMethod(CustomProtocolParameters serializedRequest)
        {
            string requestedMethod = serializedRequest.Method;
            string requestData = serializedRequest.Obj.ToString();

            try
            {
                return requestedMethod switch
                {
                    Constant.ADD_FOOD => AddFood(requestData),
                    Constant.UPDATE_FOOD => UpdateFood(requestData),
                    Constant.DELETE_FOOD => DeleteFood(requestData),
                    Constant.GET_FOOD => GetFood(requestData),
                    Constant.GET_DISCARD_ITEM_LIST => GetDiscardItemList(),
                    Constant.GET_DISCARD_ITEM_LIST_FOR_WHICH_FEEDBACK_CAN_BE_ADDED => GetDiscardItemListForFeedback(requestData),
                    _ => throw new ControllerNotfound(),
                };
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Deserialization error: {ex.Message}");
                throw;
            }
            catch (ControllerNotfound ex)
            {
                Console.WriteLine($"Controller not found: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        private Response AddFood(string foodItem)
        {
            Food food = _jsonSerializer.DeserializeObject<Food>(foodItem);
            return _foodService.AddFoodItem(food);
        }

        private Response UpdateFood(string foodItem)
        {
            Food food = _jsonSerializer.DeserializeObject<Food>(foodItem);
            return _foodService.UpdateFoodItem(food);
        }

        private Response DeleteFood(string foodItemId)
        {
            int foodId = JsonSerializer.Deserialize<int>(foodItemId);
            return _foodService.DeleteFoodItem(foodId);
        }

        private Response GetDiscardItemList()
        {
            return _foodService.GetDiscardItem();
        }

        private Response GetDiscardItemListForFeedback(string parameter)
        {
            int userId = Convert.ToInt32(parameter);
            return _foodService.GetDiscardedItemForAddingFeedback(userId);
        }

        private Response GetFood(string parameter)
        {
            int userId = Convert.ToInt32(parameter);
            return _foodService.GetFoodItems(userId);
        }
    }
}
