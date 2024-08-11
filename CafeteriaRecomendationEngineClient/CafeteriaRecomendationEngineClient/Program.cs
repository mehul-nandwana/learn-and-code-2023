using CafeteriaRecomendationEngineClient.Client;

namespace CafeteriaManagementClient
{
    class Program
    {
        public static void Main(string[] args)
        {
            ClientHandler client = new ClientHandler();
            client.Start();
        }
    }      
}

