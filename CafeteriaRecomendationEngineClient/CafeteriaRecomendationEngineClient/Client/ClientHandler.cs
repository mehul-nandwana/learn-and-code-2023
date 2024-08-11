using CafeteriaRecomendationEngineClient.DTO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace CafeteriaRecomendationEngineClient.Client
{
    public class ClientHandler
    {
        private const int Port = 8080;
        private Socket _sender;
        private IPAddress _ipAddress;
        RequestProcessor _processor = new RequestProcessor();
        JSonSerializer _jSonSerializer = new JSonSerializer();
        public void Start()
        {
            try
            {
                ConnectToServer();
                HandleCommunication();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected exception: {0}", ex);
            }
        }

        private void ConnectToServer()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            _ipAddress = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(_ipAddress, Port);
            _sender = new Socket(_ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _sender.Connect(endPoint);
            Console.WriteLine("Connected to server.");
        }

        private void HandleCommunication()
        {
            try
            {
                CustomProtocolParameters request = _processor.ProcessUserLogin();
                SendRequest(request);
                while (true)
                {
                    string response = ReadFromServer();
                    request = _processor.ProcessRequest(response);
                    SendRequest(request);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during communication: {0}", ex.Message);
                Start();
            }
        }

        public void SendRequest(CustomProtocolParameters request)
        {         
            
            string jsonData = _jSonSerializer.SerializeObject(request);
            byte[] dataBytes = Encoding.ASCII.GetBytes(jsonData);
            if (_sender == null)
                ConnectToServer();
            _sender.Send(dataBytes);
            Console.WriteLine("Sent: {0}", jsonData);
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

            Console.WriteLine("Received: {0}", data);
            return data;
        }
    }
}
