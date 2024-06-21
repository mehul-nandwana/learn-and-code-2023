using CafeteriaManagementSystemServer;
using CafeteriaManagementSystemServer.Models;
using CafeteriaRecomendationEngineClient.Models;
using CafeteriaRecommendationEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CafeteriaRecomendationEngineClient.Client
{
    public class RequestProcessor
    {
        Request request;
        public Request UserLogin()
        {
        initialPage:
            UserModel user = GetUserCredentials();
            string json = JsonSerializer.Serialize(user);
            byte[] JSONdata = Encoding.ASCII.GetBytes(json);
            request = new Request(Constant.LOGIN, user);
            return request;
        }
        public Request ProcessRequest(string message)
        {
            CafeteriaRecommendationEngine.JSonSerializer jsonSerializer = new JSonSerializer();
            CustomProtocolParameters<object> serializedRequest = jsonSerializer.DeSerializeObject(message);
            //if(serializedRequest.Response== Constants.FAILURE_MESSAGE)
            if (serializedRequest.Method.ToLower() == Constant.ADMIN_LOGIN)
            {
                return ProcessAdminLogin();
            }
            else if (serializedRequest.Method.ToLower() == Constant.CHEF_LOGIN)
            {
                return ProcessChefLogin();
            }
            else if (serializedRequest.Method.ToLower() == "employee")
            {
                
                int id = Convert.ToInt32(serializedRequest.Headers["error-message"]);
                return ProcessEmployeeLogin(id);
            }
            else if(serializedRequest.Method.ToLower() == "showmenu")
            {
                List<FoodItem> items = JsonSerializer.Deserialize<List<FoodItem>>(serializedRequest.obj.ToString());
                foreach (var item in items)
                    Console.WriteLine("Item Name " + item.Id + "  " + "item Price " + item.Price + "  " + "Meal Id " + item.MealType);
                   
                //PrintTable(items);
                // FoodItem menu = (FoodItem)serializedRequest.obj;
                return UserLogin();
            }
            else if (serializedRequest.Method.ToLower() == "seenotification")
            {
                List<string> notificationMessage = JsonSerializer.Deserialize<List<string>>(serializedRequest.obj.ToString());

                //List<string> notificationMessage = (List<string>)serializedRequest.obj;
                foreach(var notification in notificationMessage)
                {
                    Console.WriteLine(notification);
                }
                return UserLogin();
            }
            else if(serializedRequest.Method.ToLower() == "showrecommendation")
            {
                List<int> menuIds = JsonSerializer.Deserialize<List<int>>(serializedRequest.obj.ToString());

                //List<string> notificationMessage = (List<string>)serializedRequest.obj;
                foreach (var menuId in menuIds)
                {
                    Console.WriteLine("MenuId = " + menuId + "\n");
                }
                return UserLogin();
            }
            else
            {
                Console.WriteLine("User does not exists");
                return UserLogin();
            }
        }

        public Request ProcessEmployeeLogin(int id)
        {
            ProcessEmployeeLogin:
            FeedBackData feedbackData = new FeedBackData(); 
            Console.WriteLine("Welcome Employee\n" +
                              "Enter the Choice\n" +
                              "1) Add feedback \n" +
                              "2) Add choice\n" +
                              "3) Get Notification\n" +
                              "4) Get Menu \n" +
                              "\n");
            Console.WriteLine("Enter choice");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                Console.WriteLine("Enter food Id");
                feedbackData.MenuId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter rating");
                feedbackData.Rating = Convert.ToInt32(Console.ReadLine());
                //if(feedbackData.Rating < 0 || feedbackData.Rating > 5)
                //{
                //    Console.WriteLine("Enter correct value");
                //    goto startEntry;
                //}
                Console.WriteLine("Enter comment");
                feedbackData.Comment = Console.ReadLine();
                feedbackData.Id =id;
                string json = JsonSerializer.Serialize(feedbackData);
                byte[] JSONdata = Encoding.ASCII.GetBytes(json);
                request = new Request("addfeedback", feedbackData);
                return request;
                  
                case 2:
                    ChoiceData choiceData = new ChoiceData();
                    Console.WriteLine("Enter the mealId and menuId");
                    choiceData.MealId = Convert.ToInt32( Console.ReadLine());
                    choiceData.MenuId = Convert.ToInt32(Console.ReadLine());
                    choiceData.UserId = id;
                    json = JsonSerializer.Serialize(choiceData);
                     JSONdata = Encoding.ASCII.GetBytes(json);
                    request = new Request("addchoice", choiceData);
                    return request;
                case 3:
                    request = new Request("getNotification", feedbackData);
                    return request;
                case 4:
                    request = new Request("getmenu", "");
                    
                    return request;
                default:
                    goto ProcessEmployeeLogin;

            }
        }

        public Request ProcessChefLogin()
        {
        initial:
            Console.WriteLine("Welcome Chef\n" +
                              "Enter the Choice\n" +
                              "1) Add menu item \n" +
                              "2) Get Recommendation\n" +
                              "3) Get Feedback\n" +
                              "\n");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Enter the number of items to rollout");
                    int noOfItems = Convert.ToInt32(Console.ReadLine());
                    int[] arr = new int[noOfItems];
                    for (int i = 0; i < noOfItems; i++)
                    {
                        Console.WriteLine("Enter the Food Id");
                        arr[i] = Convert.ToInt32(Console.ReadLine());

                    }
                    string json = JsonSerializer.Serialize(arr);
                    byte[] JSONdata = Encoding.ASCII.GetBytes(json);
                    request = new Request("setmenu", arr);
                    return request;
                case 2:
                    Console.WriteLine("Provide Mealtypeid\n");
                    Console.WriteLine("Number of items\n");
                    RecommendatioData recommendationData = new RecommendatioData();
                    recommendationData.NumberOfDishes = Convert.ToInt32(Console.ReadLine());
                    recommendationData.MealTypeId = Convert.ToInt32(Console.ReadLine());
                    json = JsonSerializer.Serialize(recommendationData);
                    JSONdata = Encoding.ASCII.GetBytes(json);
                    request = new Request("getrecommendation", recommendationData);
                    return request;
                case 3:
                    request = new Request("getfeedback", "");
                    return request;
                
                default:
                    Console.WriteLine("Enter correct response");
                    goto initial;

            }
        }
        private Request ProcessAdminLogin()
        {
        ProcessAdmin:
            Console.WriteLine("Welcome Admin\n" +
                              "Enter the Choice\n" +
                              "1) Add Food item\n" +
                              "2) Update Food item\n" +
                              "3) Delete Food item\n");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Food food = GetFoodEntries();
                    string json = JsonSerializer.Serialize(food);
                    byte[] JSONdata = Encoding.ASCII.GetBytes(json);
                    request = new Request("addfood", food);
                    return request;

                case 2:

                    Console.WriteLine("Add Food name,Food price, FoodStatus, mealtype(Press 1 For Breakfast and 2 for Lunch and Dinner)\n");
                    Food fooditem = GetFoodEntries();
                    json = JsonSerializer.Serialize(fooditem);
                    JSONdata = Encoding.ASCII.GetBytes(json);
                    request = new Request("updatefood", fooditem);
                    return request;

                case 3:
                    Console.WriteLine("Enter the id");
                    int id = Convert.ToInt32(Console.ReadLine());
                    json = JsonSerializer.Serialize(id);
                    JSONdata = Encoding.ASCII.GetBytes(json);
                    request = new Request("deletefood", id);
                    return request;

                default:
                    Console.WriteLine("Enter correct value");
                    goto ProcessAdmin;
                    break;
            }
        }
        private UserModel GetUserCredentials()
        {
            Console.WriteLine("Enter the login credentials");
            UserModel userModel = new UserModel();
            userModel.username = Console.ReadLine();
            userModel.password = Console.ReadLine();
            return userModel;
        }
        private void PrintTable(List<FoodItem> items)
        {
            Console.WriteLine("{0,-5} {1,-15} {2,-10} {3,-12} {4,-10}", "Id", "Name", "Price", "Availability", "MealTypeId");

            // Print table rows
            foreach (var item in items)
            {
                Console.WriteLine("{0,-5} {1,-15} {2,-10} {3,-12} {4,-10}",
                    item.Id, item.Name, item.Price,  item.MealType);
            }
            //// Print table headers
            //Console.WriteLine("{0,-5} {1,-15} ", "Name", "Price");

            //// Print table rows
            //foreach (var item in items)
            //{
            //    Console.WriteLine("{0,-5} {1,-15} ",
            //         item.Name, item.Price);
            //}
        }

        private Food GetFoodEntries()
        {
            AddFoodEntries:
            Console.WriteLine("Add Food name,Food price, FoodStatus, mealtype(Press 1 For Breakfast and 2 for Lunch and Dinner)\n");
            Food food = new Food();
            food.Name = Console.ReadLine();
            food.Price = Convert.ToInt32(Console.ReadLine());
            string input = Console.ReadLine().Trim().ToLower();

            bool Availability;
            if (bool.TryParse(input, out Availability))
            {
                food.Availability = Availability;

            }
            else
            {
                Console.WriteLine("Invalid input. Please enter either 'true' or 'false'.");
                goto AddFoodEntries;
            }
            Console.WriteLine("Enter the meal Type id");

            food.MealTypeId = Convert.ToInt32(Console.ReadLine());
            return food;
        }
    }
    
}
