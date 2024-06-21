using CafeteriaRecomendationEngineClient.Client;
using CafeteriaRecomendationEngineClient.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementClient
{
    class Program
    {
        public static void Main(string[] args)
        {
            Client client = new Client();
            client.Start();
        }
    }      
}

