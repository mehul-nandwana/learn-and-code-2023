using CafeteriaManagementSystemServer.Models;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace CafeteriaManagementServer
{
    public static class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void Main(string[] args)
        {
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
    }
}
