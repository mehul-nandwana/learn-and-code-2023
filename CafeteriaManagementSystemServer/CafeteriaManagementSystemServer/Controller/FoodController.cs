using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using System.Text.Json;

namespace CafeteriaManagementSystemServer.Controller
{
    public class FoodController : ICommonController
    {
        private readonly FoodService _foodService;

        public FoodController()
        {
            _foodService = new FoodService();
        }

        public override Response CallMethod(CustomProtocolParameters serializedRequest)
        {
            string requestedMethod = serializedRequest.Method;
            string parameter = serializedRequest.Obj.ToString();

            try
            {
                return requestedMethod switch
                {
                    Constant.ADD_FOOD => AddFood(parameter),
                    Constant.UPDATE_FOOD => UpdateFood(parameter),
                    Constant.DELETE_FOOD => DeleteFood(parameter),
                    Constant.GET_DISCARD_ITEM_LIST => GetDiscardItemList(),
                    Constant.GET_DISCARD_ITEM_LIST_FOR_WHICH_FEEDBACK_CAN_BE_ADDED => GetDiscardItemListForFeedback(parameter),
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

        private Response AddFood(string parameter)
        {
            Food food = JsonSerializer.Deserialize<Food>(parameter);
            return _foodService.AddFoodItem(food);
        }

        private Response UpdateFood(string parameter)
        {
            Food food = JsonSerializer.Deserialize<Food>(parameter);
            return _foodService.UpdateFoodItem(food);
        }

        private Response DeleteFood(string parameter)
        {
            int foodId = JsonSerializer.Deserialize<int>(parameter);
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
    }
}
