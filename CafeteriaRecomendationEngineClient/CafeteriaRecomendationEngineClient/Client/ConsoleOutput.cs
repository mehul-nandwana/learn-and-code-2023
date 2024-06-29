using CafeteriaManagementSystemServer.Models;
using CafeteriaRecomendationEngineClient.DTO;
using System.Text.Json;

namespace CafeteriaRecomendationEngineClient.Client
{
    public class ConsoleOutput
    {
        JSonSerializer _jsonSerializer =  new JSonSerializer();  
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
            List<FoodItem> items = _jsonSerializer.DeserializeObject<List<FoodItem>>(parameters.Obj);
            foreach (var item in items)
            {
                Console.WriteLine($"Item Name: {item.Id}  Item Price: {item.Price}  Meal Id: {item.MealType}");
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

        public void DisplayWrongInputMessage()
        {
            Console.WriteLine("Enter correct value");
        }

        
    }
}
