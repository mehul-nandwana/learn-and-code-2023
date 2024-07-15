using CafeteriaManagementSystemServer.Interfaces;
using CafeteriaManagementSystemServer.Repository;
using CafeteriaManagementSystemServer.ServiceHelper;
using CafeteriaManagementSystemServer.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;

namespace CafeteriaManagementServer
{
    public static class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private static IServiceProvider _serviceProvider;

        public static void Main(string[] args)
        {
            //var serviceProvider = new ServiceCollection()
            //.AddSingleton<IAuthenticationService, AuthenticationService>()
            //.BuildServiceProvider();

            //var configuration = new ConfigurationBuilder()
            //                         .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //                         .AddJsonFile("appsettings.json")
            //                         .Build();

            //var services = new ServiceCollection();
            //ConfigureServices(services, configuration);

            //_serviceProvider = services.BuildServiceProvider();


            var config = new LoggingConfiguration();
            var logfile = new FileTarget("logfile") { FileName = "logfile.txt" };
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);
            LogManager.Configuration = config;

            logger.Info("Application started");

            Server server = new Server();
            server.StartServer();

            logger.Info("Application ended");

            LogManager.Shutdown();  
        }
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }
    }
}
