using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using CafeteriaRecomendationEngineClient.DTO;
using Xunit;

namespace CafeteriaManagementSystemServerTests
{
    public class NotificationAndUserServiceTests
    {
        [Theory]
        [InlineData(CafeteriaManagementSystemServer.Models.Constant.ADD_FOOD_NOTIFICATION, "food is Added to the Menu")]
        public void AddFoodNotificationReturnsNotification(string notificationType, string notificaitonMessage)
        {
            //arrange
            TestContext testContext = new TestContext();
            Notification notification = testContext.GetAddFoodNotification();
            NotificationService notificationService = new NotificationService();

            //act
            Notification resultNotification = notificationService.SetNotification(notificationType, notificaitonMessage);

            //assure
            Assert.Equal(notification.NotificationType, resultNotification.NotificationType);
            Assert.Equal(notification.NotificationMessage, resultNotification.NotificationMessage);
        }

        [Fact]
        public void SetUserProfileReturnsUserPreference()
        {
            //arrange
            TestContext testContext = new TestContext();
            UserPreference preference = testContext.GetUserPreference();
            UserService userService = new UserService();
            UserProfile userProfile = new UserProfile()
            {
                UserId = 1,
                IsVegeterian = "vegetarian",
                CuisineType = "indian",
                SpiceLevel = 2,
                Sweet = false
            };


            //act
            UserPreference resultUserPreference = userService.SetUserProfileData(userProfile);

            //assure
            Assert.Equal(preference.UserId, resultUserPreference.UserId);
            Assert.Equal(preference.IsVegeterian, resultUserPreference.IsVegeterian);
            Assert.Equal(preference.CuisineType, resultUserPreference.CuisineType);
            Assert.Equal(preference.SpiceLevel, resultUserPreference.SpiceLevel);
            Assert.Equal(preference.Sweet, resultUserPreference.Sweet);
        }
    }
}