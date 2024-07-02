using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaRecomendationEngineClient.DTO;
using System.Text.Json;

namespace CafeteriaRecomendationEngineClient.Client
{
    public class ConsoleOutput
    {
        JSonSerializer _jsonSerializer =  new JSonSerializer();  
        UserInput _userInput = new UserInput();
        public void ShowNotification(CustomProtocolParameters parameters)
        {
            List<string> notifications = _jsonSerializer.DeserializeObject<List<string>>(parameters.Obj);
            foreach (var notification in notifications)
            {
                Console.WriteLine(notification);
            }
        }

        public void ShowRecommendation(CustomProtocolParameters parameters)
        {
            List<int> menuIds = _jsonSerializer.DeserializeObject<List<int>>(parameters.Obj);
            foreach (var menuId in menuIds)
            {
                Console.WriteLine($"MenuId = {menuId}\n");
            }
        }

        public void ShowMenu(CustomProtocolParameters parameters)
        {
            List<string> items = _jsonSerializer.DeserializeObject<List<string>>(parameters.Obj);
            foreach (var item in items)
            {
                Console.WriteLine($" Item Name: {item}  ");
            }
        }

        public void ShowFeedback(CustomProtocolParameters parameters)
        {
            List<Feedback> feedbacks = _jsonSerializer.DeserializeObject<List<Feedback>>(parameters.Obj);
            foreach (var feedback in feedbacks)
            {
                Console.WriteLine($"Item Id: {feedback.Id}  Feedback Date: {feedback.Feedbackdate}  Comment: {feedback.Comment}  Sentiment Score: {feedback.Sentimentscore}  Menu Id: {feedback.MenuId}");
            }
        }

        public void ShowDiscardItemsForFeedback(DiscardItemData discardItemData)
        {
            List<string> fooditems = discardItemData.FoodItem;
            Console.WriteLine("Food Item are");

            foreach (var foodItem in fooditems)
            {
                Console.WriteLine(foodItem);
            }
            
        }

        public void ShowDiscardItems(CustomProtocolParameters parameters)
        {
            List<Food> foodItems = _jsonSerializer.DeserializeObject<List<Food>>(parameters.Obj);
            foreach (var foodItem in foodItems)
            {
                Console.WriteLine($" Food Name: {foodItem.Name}  Price: {foodItem.Price} ");
            }
           _userInput.GetChoiceAfterRecievingDiscardItem();
        }

        public void DisplayWrongInputMessage()
        {
            Console.WriteLine("Enter correct value");
        }

        
    }
}
