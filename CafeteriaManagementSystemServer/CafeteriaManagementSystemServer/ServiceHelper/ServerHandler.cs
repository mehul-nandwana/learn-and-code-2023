using System.Net.Sockets;
using System.Text;
using CafeteriaManagementSystemServer.Controller;
using CafeteriaManagementSystemServer.Models;

namespace CafeteriaManagementSystemServer.ServiceHelper
{
    public class ServerHandler
    {
        JSonSerializer _jSonSerializer = new JSonSerializer();
        ExceptionHandler.ExceptionHandler UnknownExceptionHandler = new ExceptionHandler.ExceptionHandler();
        Socket _clientSocket;
        int clientNumber;

        public ServerHandler(Socket clientSocket, int clientNumber)
        {
            this.clientNumber = clientNumber;
            _clientSocket = clientSocket;
        }

        public void HandleClient()
        {
            Thread Thread = new Thread(Communicate);
            Thread.Start();
        }
        public void Communicate()
        {
            try
            {
                while (true)
                {
                    string request = ReadFromClient();
                    CustomProtocolParameters serializedRequest = _jSonSerializer.DeSerializeObject(request);
                    ExecuteClientRequest(serializedRequest);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                SendCustomProtocolParameters(UnknownExceptionHandler.HandleUnknownException(ex));
                HandleClient();
            }
        }

        private string ReadFromClient()
        {
            byte[] bytes = new byte[4096];
            string data = "";

            while (true)
            {
                int CustomProtocolParametersSize = _clientSocket.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, CustomProtocolParametersSize);
                if (data.IndexOf("<EOF>") == -1)
                    break;
            }
            return data;
        }

        private void SendCustomProtocolParameters(byte[] CustomProtocolParametersData)
        {
            _clientSocket.Send(CustomProtocolParametersData);
        }

        private void ExecuteClientRequest(CustomProtocolParameters serializedRequest)
        {
            FactoryController factoryController = new FactoryController();
            ICommonController controller = factoryController.GetController(serializedRequest);
            SendCustomProtocolParameters(controller.ExecuteRequest(serializedRequest, controller));
        }
    }
}



