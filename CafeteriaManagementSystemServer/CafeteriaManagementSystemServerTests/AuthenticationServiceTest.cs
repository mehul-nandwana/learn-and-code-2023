using Xunit;
using System.Collections.Generic;
using System.Linq;
using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using CafeteriaManagementSystemServer.Services;

namespace CafeteriaManagementSystemServer.Tests
{
    public class AuthenticationServiceTests
    {
        [Fact]
        public void TrimmedFoodItems_ShouldContainTrimmedDish()
        {
            // Arrange
            var discardItemData = new
            {
                FoodItem = new List<string> { "  apple  ", " banana", "  carrot " }
            };
            string dish = "  apple  ";
            string trimmedDish = dish.Trim();

            // Act
            List<string> trimmedFoodItems = discardItemData.FoodItem
                .Select(item => item.Trim())
                .ToList();

            // Assert
            Assert.Contains(trimmedDish, trimmedFoodItems);
        }

        [Fact]
        public void TrimmedFoodItems_ShouldNotContainNonExistentDish()
        {
            // Arrange
            var discardItemData = new
            {
                FoodItem = new List<string> { "  apple  ", " banana", "  carrot " }
            };
            string dish = "  orange  ";
            string trimmedDish = dish.Trim();

            // Act
            List<string> trimmedFoodItems = discardItemData.FoodItem
                .Select(item => item.Trim())
                .ToList();

            // Assert
            Assert.DoesNotContain(trimmedDish, trimmedFoodItems);
        }
    }
}
