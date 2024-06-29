using CafeteriaRecomendationEngineClient.DTO;
using CafeteriaRecomendationEngineClient.Models;
using System;

namespace CafeteriaRecomendationEngineClient.Client
{
    public class UserInput
    {
        private const int MaxRating = 5;
        private const int MinRating = 0;
        private const int MinMealTypeId = 1;
        private const int MaxMealTypeId = 3;

        public UserModel GetUserCredentials()
        {
            UserModel userModel = new UserModel();
            Console.WriteLine("Enter Username:");
            userModel.username = Console.ReadLine();
            Console.WriteLine("Enter Password:");
            userModel.password = Console.ReadLine();
            return userModel;
        }

        public Food GetFoodEntries()
        {
            Food food = new Food
            {
                Name = GetInput("Add Food name:"),
                Price = GetIntInput("Add Food price:")
            };

            bool availability;
            while (!TryGetBoolInput("Is it available (true if Yes, false if No):", out availability))
            {
                Console.WriteLine("Invalid input. Please enter either 'true' or 'false'.");
            }
            food.Availability = availability;

            int mealTypeId;
            while (!TryGetIntInput("Enter the meal Type id (1 for Breakfast, 2 for lunch, 3 for dinner):", out mealTypeId) || !IsValidMealTypeId(mealTypeId))
            {
                Console.WriteLine($"Invalid input. Please enter a value between {MinMealTypeId} and {MaxMealTypeId}.");
            }
            food.MealTypeId = mealTypeId;

            return food;
        }

        public int GetEmployeeChoice()
        {
            Console.WriteLine("Welcome Employee\nEnter the Choice\n1) Add feedback\n2) Add choice\n3) Get Notification\n4) Get Menu\n");
            return GetIntInput("Enter choice:");
        }

        public FeedBackData GetFeedbackData(int id)
        {
            FeedBackData feedbackData = new FeedBackData
            {
                MenuId = GetIntInput("Enter food Id:"),
                Id = id
            };

            int rating;
            while (!TryGetIntInput("Enter rating:", out rating) || !IsValidRating(rating))
            {
                Console.WriteLine($"Invalid input. Please enter a value between {MinRating} and {MaxRating}.");
            }
            feedbackData.Rating = rating;

            feedbackData.Comment = GetInput("Enter comment:");
            return feedbackData;
        }

        public ChoiceData GetChoiceData(int id)
        {
            return new ChoiceData
            {
                MealId = GetIntInput("Enter the mealId:"),
                MenuId = GetIntInput("Enter the menuId:"),
                UserId = id
            };
        }

        public int GetChefChoice()
        {
            Console.WriteLine("Welcome Chef\nEnter the Choice\n1) Add menu item\n2) Get Recommendation\n3) Get Feedback\n");
            return GetIntInput("Enter choice:");
        }

        public int GetAdminChoice()
        {
            Console.WriteLine("Welcome Admin\nEnter the Choice\n1) Add Food item\n2) Update Food item\n3) Delete Food item\n");
            return GetIntInput("Enter choice:");
        }

        public int GetId()
        {
            return GetIntInput("Enter the id:");
        }

        public RecommendatioData GetRecommendationData()
        {
            return new RecommendatioData
            {
                NumberOfDishes = GetIntInput("Number of items:"),
                MealTypeId = GetIntInput("Provide Mealtypeid:")
            };
        }

        public int[] GetRolloutIds()
        {
            int numberOfItems = GetIntInput("Enter the number of items to rollout:");
            int[] menuIds = new int[numberOfItems];
            for (int i = 0; i < numberOfItems; i++)
            {
                menuIds[i] = GetIntInput("Enter the Food Id:");
            }
            return menuIds;
        }

        private string GetInput(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        private int GetIntInput(string prompt)
        {
            Console.WriteLine(prompt);
            return int.Parse(Console.ReadLine());
        }

        private bool TryGetIntInput(string prompt, out int result)
        {
            Console.WriteLine(prompt);
            return int.TryParse(Console.ReadLine(), out result);
        }

        private bool TryGetBoolInput(string prompt, out bool result)
        {
            Console.WriteLine(prompt);
            return bool.TryParse(Console.ReadLine(), out result);
        }

        private bool IsValidRating(int rating)
        {
            return rating >= MinRating && rating <= MaxRating;
        }

        private bool IsValidMealTypeId(int mealTypeId)
        {
            return mealTypeId >= MinMealTypeId && mealTypeId <= MaxMealTypeId;
        }
    }
}
