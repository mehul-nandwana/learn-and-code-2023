
using CafeteriaManagementSystemServer;
using CafeteriaRecomendationEngineClient.Models;
using CafeteriaRecommendationEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace CafeteriaRecomendationEngineClient.Client
{
    public class Client
    {
         static IPHostEntry _ipHost = Dns.GetHostEntry(Dns.GetHostName());
       //  private static IPAddress _ipAddr = IPAddress.Parse("172.16.1.169");
        private static IPAddress _ipAddr;
        private Socket _sender;
        private static Client _client;

        RequestProcessor _processor = new RequestProcessor();

        public static Client GetInstance()
        {
            if (_client == null)
            {
                _client = new Client();
            }
            return _client;
        }
        public void Start()
        {
            try
            {
                Connect();
                SendRequest( "Hello kaushik, mehul this side");
                //PerformAction();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Unexpected exception : {0}", exception.ToString());
            }
        }
        public void Connect()
        {
            try
            {
                //_ipHost = Dns.GetHostEntry(Dns.GetHostName());
                _ipAddr = IPAddress.Parse("172.16.1.169");
                                                IPEndPoint localEndPoint = new IPEndPoint(_ipAddr, 3001);
                _sender = new Socket(_ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _sender.Connect(localEndPoint);
                byte[] JSONdata = Encoding.ASCII.GetBytes("Hello Kaushik");
                _sender.Send(JSONdata);
            }catch (Exception exception)
            {

            }

        }
        private void SendRequest(string request)
        {
            try
            {
               string jsonData = JsonSerializer.Serialize(request);
                byte[] JSONdata = Encoding.ASCII.GetBytes(jsonData);
                _sender.Send(JSONdata);
            }
            catch (Exception exception)
            {

            }
        }
        private string RecieveRequest()
        {
            string response = ReadFromServer();
            return response;
        }
        private void PerformAction()
        {
            try
            {
                Request request;
                //request = _processor.UserLogin();
                SendRequest("Hello kaushik");
                //while (true)
                //{
                //    string response = RecieveRequest();
                //    request = _processor.ProcessRequest(response);
                //    SendRequest(request);
                //}
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error Occured");
            }
            finally {
                _sender.Close();
                Console.WriteLine("Connection Closed Due to some Error");

            }
        }      
        private string ReadFromServer()
        {
            byte[] bytes = new Byte[4096];
            string data = "";
            while (true)
            {
                int responseSize = _sender.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, responseSize);
                if (data.IndexOf("<EOF>") == -1)
                    break;
            }
            return data;
        }
    }
}
