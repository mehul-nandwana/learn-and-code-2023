using CafeteriaManagementSystemServer;
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
            request = new Request(Constant.LOGIN, user, JSONdata.Length);
            return request;
        }
        public Request ProcessRequest(string message)
        {
            JSonSerializer jsonSerializer = new JSonSerializer();
            CustomProtocolParameters<object> serializedRequest = jsonSerializer.DeSerializeObject(message);
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
            else
            {
                Console.WriteLine("User does not exists");
                return UserLogin();
            }
        }

        public Request ProcessEmployeeLogin(int id)
        {
            startEntry:
            FeedBackData feedbackData = new FeedBackData(); 
            Console.WriteLine("Welcome Employee\n" +
                              "Enter the Choice\n" +
                              "1) Add feedback \n" +
                              "2) Get Recommendation\n" +
                              "3) Get Feedback\n" +
                              "\n");
            Console.WriteLine("Enter choice");
            int choice = Convert.ToInt32(Console.ReadLine());
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
            request = new Request("addfeedback", feedbackData, JSONdata.Length);
            return request;
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
                    Console.WriteLine("Add food Id\n");
                    Console.WriteLine("Add Meal Id\n");
                    MenuData menu = new MenuData();
                    menu.FoodId = Convert.ToInt32(Console.ReadLine());
                    menu.MeaId = Convert.ToInt32(Console.ReadLine());
                    string json = JsonSerializer.Serialize(menu);
                    byte[] JSONdata = Encoding.ASCII.GetBytes(json);
                    request = new Request("addfood", menu, JSONdata.Length);
                    return request;
                case 2:
                    Console.WriteLine("Provide Mealtypeid\n");
                    Console.WriteLine("Number of items\n");
                    RecommendatioData recommendationData = new RecommendatioData();
                    recommendationData.NumberOfDishes = Convert.ToInt32(Console.ReadLine());
                    recommendationData.MealTypeId = Convert.ToInt32(Console.ReadLine());
                    json = JsonSerializer.Serialize(recommendationData);
                    JSONdata = Encoding.ASCII.GetBytes(json);
                    request = new Request("getrecommendation", recommendationData, JSONdata.Length);
                    return request;
                case 3:
                    request = new Request("getfeedback", "", 0);
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
                        goto case 1;
                    }
                    string json = JsonSerializer.Serialize(food);
                    byte[] JSONdata = Encoding.ASCII.GetBytes(json);
                    request = new Request("addfood", food, JSONdata.Length);
                    return request;

                case 2:

                    Console.WriteLine("Add Food name,Food price, FoodStatus, mealtype(Press 1 For Breakfast and 2 for Lunch and Dinner)\n");
                    food = new Food();
                    food.Name = Console.ReadLine();
                    food.Price = Convert.ToInt32(Console.ReadLine());
                    input = Console.ReadLine().Trim().ToLower();

                    if (bool.TryParse(input, out Availability))
                    {
                        food.Availability = Availability;

                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter either 'true' or 'false'.");
                    }
                    json = JsonSerializer.Serialize(food);
                    JSONdata = Encoding.ASCII.GetBytes(json);
                    request = new Request("updatefood", food, JSONdata.Length);
                    return request;

                case 3:
                    Console.WriteLine("Enter the id");
                    int id = Convert.ToInt32(Console.ReadLine());
                    json = JsonSerializer.Serialize(id);
                    JSONdata = Encoding.ASCII.GetBytes(json);
                    request = new Request("deletefood", id, JSONdata.Length);
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

    }
}
