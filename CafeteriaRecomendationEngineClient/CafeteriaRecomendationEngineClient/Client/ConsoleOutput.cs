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

        public CustomProtocolParameters ShowMenu(CustomProtocolParameters parameters)
        {
            ClientHandler _clientHandler = new ClientHandler();
            List<int> menuId = new List<int>();
            RequestProcessor requestProcessor = new RequestProcessor();
            ShowMenuItemData items = _jsonSerializer.DeserializeObject<ShowMenuItemData>(parameters.Obj);
            foreach (var item in items.FoodItem)
            {
                Console.WriteLine($" Item Name: {item.Name} Average Rating: {item.AverageRating} ");
                menuId.Add(item.Id);
            }
            if (items.FoodItem.Count != 0)
               return requestProcessor.CreateAddChoiceRequest(items.UserId,menuId);
            else
                return requestProcessor.ProcessEmployeeLogin(items.UserId);
           // _clientHandler.SendRequest(requestProcessor.CreateAddChoiceRequest(items.UserId));
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

        public CustomProtocolParameters ShowDiscardItems(CustomProtocolParameters parameters)
        {
            RequestProcessor requestProcessor = new RequestProcessor();
            List<Food> foodItems = _jsonSerializer.DeserializeObject<List<Food>>(parameters.Obj);
            foreach (var foodItem in foodItems)
            {
                Console.WriteLine($" Food Name: {foodItem.Name}  Price: {foodItem.Price} ");
            }
            if (foodItems.Count != 0)
                return _userInput.GetChoiceAfterRecievingDiscardItem();
            else
                return requestProcessor.ProcessChefLogin();
        }

        public void DisplayWrongInputMessage()
        {
            Console.WriteLine("Enter correct value");
        }

        
    }
}
