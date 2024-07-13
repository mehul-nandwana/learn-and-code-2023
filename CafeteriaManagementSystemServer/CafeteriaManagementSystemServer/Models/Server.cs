using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CafeteriaManagementSystemServer.Controller;
using CafeteriaManagementSystemServer.ServiceHelper;

namespace CafeteriaManagementSystemServer.Models
{
    public class Server
    {
        public void StartServer()
        {
            try
            {
                Socket serverSocket = CreateServerSocket();
                Console.WriteLine("Waiting for the connection of client to server ");
                int clientNumber = 0;
                while (true)
                {
                    clientNumber++;
                    ServerHandler thread = new ServerHandler(serverSocket.Accept(), clientNumber);
                    Console.WriteLine(" Client " + clientNumber + " Started ");
                    thread.HandleClient();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private Socket CreateServerSocket()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ip = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ip, 8080);
            Socket serverSocket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(localEndPoint);
            serverSocket.Listen(5);
            return serverSocket;
        }

    }
}

