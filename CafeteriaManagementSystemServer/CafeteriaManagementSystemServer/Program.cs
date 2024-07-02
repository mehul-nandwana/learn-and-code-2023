using System;
using System.ComponentModel.Design;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CafeteriaManagementSystemServer.Controller;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using CafeteriaManagementSystemServer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CafeteriaManagementServer
{
     public static class Program
    {
        private static ServiceProvider serviceProvider;

        public static void Main(string[] args)
        { 
            Server server =new Server();
            
            server.StartServer();
        }
        public static ServiceProvider ConfigureServices()
        {
            serviceProvider = new ServiceCollection()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IFeedbackRepository, FeedbackRepository>()
            .AddScoped<IMenuRepository, MenuRepository>()
            .AddScoped<INotificationRepository, NotificationRepository>()
            .AddScoped<IRecommendationRepository, RecommendationRepository>()
            .AddScoped<IFeedbackService, FeedbackService>()
            .AddScoped<INotificationService, NotificationService>()
            .AddScoped<IFoodService, FoodService>()
            .AddScoped<IFoodController, FoodController>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IFoodRepository,FoodRepository>()
            .BuildServiceProvider();
            return serviceProvider;
        }
    }
}