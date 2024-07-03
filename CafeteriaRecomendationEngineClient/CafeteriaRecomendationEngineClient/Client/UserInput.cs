using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaRecomendationEngineClient.DTO;
using CafeteriaRecomendationEngineClient.Models;
using System;
using System.Reflection.Metadata.Ecma335;

namespace CafeteriaRecomendationEngineClient.Client
{
    public class UserInput
    {
        private const int MaxRating = 5;
        private const int MinRating = 0;
        private const int MinInputValue = 1;
        private const int MaxInputValue = 3;

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
                Console.WriteLine($"Invalid input. Please enter a value between {MinInputValue} and {MaxInputValue}.");
            }
            food.MealTypeId = mealTypeId;

            bool isSweet;

            while (!TryGetBoolInput("Is it sweet(Press 0 if no and 1 if yes", out isSweet))
            {
                Console.WriteLine("Invalid input. Please enter either 'true' or 'false'.");
            }
            food.Sweet = isSweet;

            int spiceLevel;
            while (!TryGetIntInput("Provice Spicy level\n 1) High\n2) Medium\n3) Low", out spiceLevel) || !IsValid(spiceLevel))
            {
                Console.WriteLine($"Invalid input. Please enter a value between {MinRating} and {MaxRating}.");
            }
            food.Spicelevel = spiceLevel;

            int isVegeterian;

            while (!TryGetIntInput("Please select one:\n1. Vegetarian\n2. Non Vegetarian\n3. Eggetarian\nEnter the number corresponding to your choice: ", out isVegeterian) || !IsValid(isVegeterian))
            {
                Console.WriteLine($"Invalid input. Please enter a value between {MinInputValue} and {MaxInputValue}.");
            }
            switch (isVegeterian)
            {
                case 1:
                    food.DietaryPreference = "vegeterian";
                    break;
                case 2:
                    food.DietaryPreference = "nonvegeterian";
                    break;
                case 3:
                    food.DietaryPreference = "eggeterian";
                    break;

            }

            int Cuisine;
            while (!TryGetIntInput("Please select one:\n1. NorthIndian\n2. South indian\n3. Other\nEnter the number corresponding to your choice: ", out Cuisine) || !IsValid(Cuisine))
            {
                Console.WriteLine($"Invalid input. Please enter a value between {MinInputValue} and {MaxInputValue}.");
            }
            switch (Cuisine)
            {
                case 1:
                    food.CuisineType = "northindian";
                    break;
                case 2:
                    food.CuisineType = "southindian";
                    break;
                case 3:
                    food.CuisineType = "other";
                    break;
            }

            return food;
        }

        public int GetEmployeeChoice()
        {
            Console.WriteLine("Welcome Employee\nEnter the Choice\n1) Add feedback for Food Item\n2) Add choice For next Day Food\n3) Get Food Notification\n4) Get Menu item for tommorow\n5) Update My taste\n6) Add Feedback for Discarded item\n7) Logout");
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

        public DetailFeedbackData GetDetailFeedback(DiscardItemData discardItemData)
        {
            DetailFeedbackData detailFeedbackData = new DetailFeedbackData();
            Console.WriteLine("Enter the dish");
            string dish = Console.ReadLine().ToLower();
            bool isFoodPresent = false;
            foreach(string item in discardItemData.FoodItem)
            {
                if(String.Equals(item.Trim(),dish))
                    isFoodPresent = true;
            }
            if(isFoodPresent)
            {
                Console.WriteLine("Add Your Moms Recipie or Anything u like or dislike about the food item");
                detailFeedbackData.FoodItemName = dish;
                detailFeedbackData.FoodItemFeedback = Console.ReadLine();
                detailFeedbackData.UserId = discardItemData.UserId;
            }
            else
            {
                Console.WriteLine("Please Enter the food menttioned in the list");
                GetDetailFeedback(discardItemData);
            }
            return detailFeedbackData;
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
            Console.WriteLine("Welcome Chef\nEnter the Choice\n1) Add menu item\n2) Get Recommendation\n3) Get Feedback\n4) Provide Feedback for Discarded Item\n5) Logout");
            return GetIntInput("Enter choice:");
        }

        public int GetAdminChoice()
        {
            Console.WriteLine("Welcome Admin\nEnter the Choice\n1) Add Food item\n2) Update Food item\n3) Delete Food item\n4) Logout");
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

        public int GetIntInput(string prompt)
        {
            Console.WriteLine(prompt);
            int choice = Convert.ToInt32(Console.ReadLine());
            return choice;
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

        private bool IsValid(int value)
        {
            return value >= MinInputValue && value <= MaxInputValue;
        }

        private bool IsValidMealTypeId(int mealTypeId)
        {
            return mealTypeId >= MinInputValue && mealTypeId <= MaxInputValue;
        }

        public CustomProtocolParameters GetChoiceAfterRecievingDiscardItem()
        {
            ClientHandler clientHandler = new ClientHandler();
            RequestProcessor processor = new RequestProcessor();
            CustomProtocolParameters request;
            int choice = GetIntInput("1) Delete the item 2) Get Detailed Feedback");
            if (choice == 1)
            {
                request = processor.CreateDeleteFoodRequest();
                return request;
                //clientHandler.SendRequest(request);
            }
            else if (choice == 2)
            {
                request = processor.ProcessGetDetailFeedback();
                return request;
                //clientHandler.SendRequest(request);
            }
            else
            {
                Console.WriteLine("Enter valid choice");
                return GetChoiceAfterRecievingDiscardItem();
            }
        }

        public UserProfile UpdateUserProfile(int id)
        {
            UserProfile userProfile = new UserProfile();
            userProfile.UserId = id;
            int isVegeterian;

            while (!TryGetIntInput("Please select one:\n1. Vegetarian\n2. Non Vegetarian\n3. Eggetarian\nEnter the number corresponding to your choice: ", out isVegeterian) || !IsValid(isVegeterian))
            {
                Console.WriteLine($"Invalid input. Please enter a value between {MinInputValue} and {MaxInputValue}.");
            }
            switch (isVegeterian)
            {
                case 1:
                    userProfile.IsVegeterian = "vegeterian";
                    break;
                case 2:
                    userProfile.IsVegeterian = "nonvegeterian";
                    break;
                case 3:
                    userProfile.IsVegeterian = "eggeterian";
                    break;

            }
            int Cuisine;

            while (!TryGetIntInput("Please select one:\n1. NorthIndian\n2. South indian\n3. Other\nEnter the number corresponding to your choice: ", out Cuisine) || !IsValid(Cuisine))
            {
                Console.WriteLine($"Invalid input. Please enter a value between {MinInputValue} and {MaxInputValue}.");
            }
            switch (Cuisine)
            {
                case 1:
                    userProfile.CuisineType = "northindian";
                    break;
                case 2:
                    userProfile.CuisineType = "southindian";
                    break;
                case 3:
                    userProfile.CuisineType = "other";
                    break;

            }
            int spiceLevel;
            while (!TryGetIntInput("Please select Spice level:\n1. High\n2. Medium\n3. Low\nEnter the number corresponding to your choice: ", out spiceLevel) || !IsValid(spiceLevel))
            {
                Console.WriteLine($"Invalid input. Please enter a value between {MinInputValue} and {MaxInputValue}.");
            }
            userProfile.SpiceLevel = spiceLevel;

            bool isSweet;
            while (!TryGetBoolInput("Is it available (true if Yes, false if No):", out isSweet))
            {
                Console.WriteLine("Invalid input. Please enter either 'true' or 'false'.");
            }
            userProfile.Sweet = isSweet;
            return userProfile;

        }

    }
}
