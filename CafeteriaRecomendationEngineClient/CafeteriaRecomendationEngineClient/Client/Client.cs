
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
        private static IPAddress _ipAddr = _ipHost.AddressList[0];

        private  Socket _sender = new Socket(_ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
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
                PerformAction();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Unexpected exception : {0}", exception.ToString());
            }
        }
        public void Connect()
        {
             
             _ipHost = Dns.GetHostEntry(Dns.GetHostName());
             _ipAddr = _ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(_ipAddr, 8080);
            _sender = new Socket(_ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _sender.Connect(localEndPoint);

        }
        private void SendRequest(Request request)
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(request);
                byte[] JSONdata = Encoding.ASCII.GetBytes(jsonData);
                Connect();
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
                request = _processor.UserLogin();
                SendRequest(request);
                while (true)
                {
                    string response = RecieveRequest();
                    request = _processor.ProcessRequest(response);
                    SendRequest(request);
                }
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
