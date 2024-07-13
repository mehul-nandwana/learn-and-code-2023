using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaRecomendationEngineClient.DTO;
using CafeteriaRecomendationEngineClient.Models;
using System;
using System.Collections.Generic;

namespace CafeteriaRecomendationEngineClient.Client
{
    public class UserInput
    {
        private readonly ChoiceData choiceData = new ChoiceData();

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
                Price = GetValidIntInput("Add Food price:")
            };

            bool availability;
            while (!TryGetBoolInput("Is it available (true if Yes, false if No):", out availability))
            {
                Console.WriteLine("Invalid input. Please enter either 'true' or 'false'.");
            }
            food.Availability = availability;

            int mealTypeId;
            while (!TryGetIntInput("Enter the meal Type id (1 for Breakfast, 2 for Lunch, 3 for Dinner):", out mealTypeId) || !IsValidMealTypeId(mealTypeId))
            {
                Console.WriteLine($"Invalid input. Please enter a value between {MinInputValue} and {MaxInputValue}.");
            }
            food.MealTypeId = mealTypeId;

            bool isSweet;
            while (!TryGetBoolInput("Is it sweet (true if Yes, false if No):", out isSweet))
            {
                Console.WriteLine("Invalid input. Please enter either 'true' or 'false'.");
            }
            food.Sweet = isSweet;

            int spiceLevel;
            while (!TryGetIntInput("Provide Spicy level (1 for High, 2 for Medium, 3 for Low):", out spiceLevel) || !IsValid(spiceLevel))
            {
                Console.WriteLine($"Invalid input. Please enter a value between {MinInputValue} and {MaxInputValue}.");
            }
            food.Spicelevel = spiceLevel;

            int isVegetarian;
            while (!TryGetIntInput("Please select one:\n1. Vegetarian\n2. Non Vegetarian\n3. Eggetarian\nEnter the number corresponding to your choice: ", out isVegetarian) || !IsValid(isVegetarian))
            {
                Console.WriteLine($"Invalid input. Please enter a value between {MinInputValue} and {MaxInputValue}.");
            }
            food.DietaryPreference = isVegetarian switch
            {
                1 => "vegetarian",
                2 => "nonvegetarian",
                3 => "eggetarian",
                _ => food.DietaryPreference
            };

            int cuisine;
            while (!TryGetIntInput("Please select one:\n1. NorthIndian\n2. South Indian\n3. Other\nEnter the number corresponding to your choice: ", out cuisine) || !IsValid(cuisine))
            {
                Console.WriteLine($"Invalid input. Please enter a value between {MinInputValue} and {MaxInputValue}.");
            }
            food.CuisineType = cuisine switch
            {
                1 => "northindian",
                2 => "southindian",
                3 => "other",
                _ => food.CuisineType
            };

            return food;
        }

        public int GetEmployeeChoice()
        {
            Console.WriteLine("Welcome Employee\nEnter the Choice\n1) Add feedback for Food Item\n2) Get Food Notification\n3) Get Menu item for tomorrow\n4) Update My taste\n5) Add Feedback for Discarded item\n6) Logout");
            return GetValidChoiceInput("Enter choice:", 1, 6);
        }

        public FeedBackData GetFeedbackData(NotificationData notificationData)
        {
            FeedBackData feedbackData = new FeedBackData();
            string foodItem = string.Empty;
            List<string> foodItems = notificationData.NotificationMessage;

            do
            {
                Console.WriteLine("Enter the valid foodItem:");
                foodItem = Console.ReadLine();
            } while (!foodItems.Contains(foodItem));

            feedbackData.FoodName = foodItem;
            feedbackData.Id = notificationData.userId;

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
            Console.WriteLine("Enter the dish:");
            string dish = Console.ReadLine().ToLower();

            if (discardItemData.FoodItem.Contains(dish))
            {
                Console.WriteLine("Add Your Mom's Recipe or anything you like or dislike about the food item:");
                detailFeedbackData.FoodItemName = dish;
                detailFeedbackData.FoodItemFeedback = Console.ReadLine();
                detailFeedbackData.UserId = discardItemData.UserId;
            }
            else
            {
                Console.WriteLine("Please enter the food mentioned in the list.");
                return GetDetailFeedback(discardItemData);
            }
            return detailFeedbackData;
        }

        public ChoiceData GetChoiceData(int id, List<int> menuIds)
        {
            do
            {
                Console.WriteLine("Enter the valid foodid:");
                choiceData.MenuId = GetValidIntInput("");
            } while (!menuIds.Contains(choiceData.MenuId));
            choiceData.UserId = id;
            return choiceData;
        }

        public int GetChefChoice()
        {
            Console.WriteLine("Welcome Chef\nEnter the Choice\n1) Add menu item for next day\n2) Get Recommendation\n3) Get Feedback For food items\n4) Get the Item which can be Discarded\n5) Logout");
            return GetValidChoiceInput("Enter choice:", 1, 5);
        }

        public int GetAdminChoice()
        {
            Console.WriteLine("Welcome Admin\nEnter the Choice\n1) Add Food item\n2) Update Food item\n3) Delete Food item\n4) Logout");
            return GetValidChoiceInput("Enter choice:", 1, 4);
        }

        public int GetId()
        {
            return GetValidIntInput("Enter the id:");
        }

        public RecommendatioData GetRecommendationData()
        {
            return new RecommendatioData
            {
                NumberOfDishes = GetValidIntInput("Number of items:"),
                MealTypeId = GetValidChoiceInput("Provide Mealtypeid:\n1) Breakfast\n2) Lunch\n3) Dinner", 1, 3)
            };
        }

        public int[] GetRolloutIds()
        {
            int numberOfItems = GetValidIntInput("Enter the number of items to rollout:");
            int[] menuIds = new int[numberOfItems];
            for (int i = 0; i < numberOfItems; i++)
            {
                menuIds[i] = GetValidIntInput("Enter the Food Id:");
            }
            return menuIds;
        }

        private string GetInput(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }

        public int GetValidIntInput(string prompt)
        {
            int result;
            while (!TryGetIntInput(prompt, out result))
            {
                Console.WriteLine("Invalid input. Please enter a valid integer value.");
            }
            return result;
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

        private int GetValidChoiceInput(string prompt, int minValue, int maxValue)
        {
            int choice;
            do
            {
                Console.WriteLine(prompt);
                if (int.TryParse(Console.ReadLine(), out choice) && choice >= minValue && choice <= maxValue)
                {
                    return choice;
                }
                Console.WriteLine($"Invalid input. Please enter a value between {minValue} and {maxValue}.");
            } while (true);
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

        public CustomProtocolParameters GetChoiceAfterReceivingDiscardItem()
        {
            RequestProcessor processor = new RequestProcessor();
            int choice;
            do
            {
                choice = GetValidChoiceInput("1) Delete the item\n2) Get Detailed Feedback\nEnter choice:", 1, 2);
                if (choice == 1)
                {
                    return processor.CreateDeleteFoodRequest();
                }
                else if (choice == 2)
                {
                    return processor.ProcessGetDetailFeedback();
                }
            } while (true);
        }

        public UserProfile UpdateUserProfile(int id)
        {
            UserProfile userProfile = new UserProfile
            {
                UserId = id
            };

            int isVegetarian;
            while (!TryGetIntInput("Please select the Dietary preference:\n1. Vegetarian\n2. Non Vegetarian\n3. Eggetarian\nEnter the number corresponding to your choice: ", out isVegetarian) || !IsValid(isVegetarian))
            {
                Console.WriteLine($"Invalid input. Please enter a value between {MinInputValue} and {MaxInputValue}.");
            }
            userProfile.IsVegeterian = isVegetarian switch
            {
                1 => "vegetarian",
                2 => "nonvegetarian",
                3 => "eggetarian",
                _ => userProfile.IsVegeterian
            };

            int cuisine;
            while (!TryGetIntInput("Please select Cuisine Type:\n1. NorthIndian\n2. South Indian\n3. Other\nEnter the number corresponding to your choice: ", out cuisine) || !IsValid(cuisine))
            {
                Console.WriteLine($"Invalid input. Please enter a value between {MinInputValue} and {MaxInputValue}.");
            }
            userProfile.CuisineType = cuisine switch
            {
                1 => "northindian",
                2 => "southindian",
                3 => "other",
                _ => userProfile.CuisineType
            };

            int spiceLevel;
            while (!TryGetIntInput("Please select Spice level:\n1. High\n2. Medium\n3. Low\nEnter the number corresponding to your choice: ", out spiceLevel) || !IsValid(spiceLevel))
            {
                Console.WriteLine($"Invalid input. Please enter a value between {MinInputValue} and {MaxInputValue}.");
            }
            userProfile.SpiceLevel = spiceLevel;

            bool isSweet;
            while (!TryGetBoolInput("Do you like Sweet one (true if Yes, false if No):", out isSweet))
            {
                Console.WriteLine("Invalid input. Please enter either 'true' or 'false'.");
            }
            userProfile.Sweet = isSweet;
            return userProfile;
        }
    }
}
