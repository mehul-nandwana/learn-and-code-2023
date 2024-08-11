using CafeteriaManagementSystemServer.DTOs;
using CafeteriaRecomendationEngineClient.DTO;
using System.Text.Json;

namespace CafeteriaRecomendationEngineClient.Client
{
    public class ConsoleOutput
    {
        JSonSerializer _jsonSerializer =  new JSonSerializer();  
        UserInput _userInput = new UserInput();
        public int ShowNotification(CustomProtocolParameters parameters)
        {
            DataItem dataItem = _jsonSerializer.DeserializeObject<DataItem>(parameters.Obj);
            List<string> notifications = dataItem.Message;
            int userId = dataItem.userId;
            foreach (var notification in notifications)
            {
                Console.WriteLine(notification);
            }
            return userId;
        }

        public void ShowRecommendation(CustomProtocolParameters parameters)
        {
            Dictionary<int, string> foodItems = _jsonSerializer.DeserializeObject<Dictionary<int, string>>(parameters.Obj);
            foreach (var menuId in foodItems)
            {
                Console.WriteLine($"FoodId = {menuId.Key} Food Name = {menuId.Value}");
            }
        }

        public CustomProtocolParameters ShowMenu(CustomProtocolParameters parameters)
        {
            List<int> menuId = new List<int>();
            RequestProcessor requestProcessor = new RequestProcessor();
            ShowMenuItemData items = _jsonSerializer.DeserializeObject<ShowMenuItemData>(parameters.Obj);
            foreach (var item in items.FoodItem)
            {
                Console.WriteLine($" Food Name: {item.Name} Average Rating: {item.AverageRating}  Food Id: {item.Id} Average Sentiment: {item.AverageSentiment} Food Price: {item.Price}");
                menuId.Add(item.Id);
            }
            if (items.FoodItem.Count != 0)
               return requestProcessor.CreateAddChoiceRequest(items.UserId,menuId);
            else
                return requestProcessor.ProcessEmployeeLogin(items.UserId);
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
            return requestProcessor.ProcessChefLogin(); 
            if (foodItems.Count != 0)
                return _userInput.GetChoiceAfterReceivingDiscardItem();
            else
                return requestProcessor.ProcessChefLogin();
        }

        public void DisplayWrongInputMessage()
        {
            Console.WriteLine("Enter correct value");
        }

        public DataItem ShowFoodItem(CustomProtocolParameters parameters)
        {
            DataItem dataItem = _jsonSerializer.DeserializeObject<DataItem>(parameters.Obj);
            List<string> notifications = dataItem.Message;
            int userId = dataItem.userId;
            if(notifications.Count != 0)
            {
                Console.WriteLine("Food Items Are\n");
            }
            else
            {
                Console.WriteLine("No items in the food list\n");
                return dataItem;
            }
            foreach (var notification in notifications)
            {
                Console.WriteLine(notification);
            }
            return dataItem;
        }

    }
}
