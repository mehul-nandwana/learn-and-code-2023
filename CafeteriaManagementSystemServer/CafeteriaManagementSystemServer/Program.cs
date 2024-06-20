using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CafeteriaManagementSystemServer.Controller;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Repository;
using CafeteriaManagementSystemServer.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CafeteriaManagementServer
{
    class Program
    {
        public static void Main(string[] args)
        {
            Server server = new Server();
            server.StartServer(); 
        }
    }
}