using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text.Json;
using System.Diagnostics;
using CafeteriaManagementSystemServer.Controller;
using CafeteriaManagementSystemServer.Models;

namespace CafeteriaManagementSystemServer
{
    public class ServerHandler
    {
        private string SUCCESS_MESSAGE = "OK";
        private string SUCCESS_STATUS = "200";
        JSonSerializer jSonSerializer = new JSonSerializer();
        Socket clientSocket;
        int clientNumber;

        public ServerHandler(Socket clientSocket, int clientNumber)
        {
            this.clientNumber = clientNumber;
            this.clientSocket = clientSocket;
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

                    Console.WriteLine("Request recieved");
                    string request = ReadFromClient();
                    CustomProtocolParameters<object> serializedRequest = jSonSerializer.DeSerializeObject(request);

                    if (VerifyRequestedData(serializedRequest))
                    {
                        ExecuteClientRequest(serializedRequest);
                    }
                    else
                    {
                        SendErrorResponse();
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went Wrong");
            }
        }

     

        private string ReadFromClient()
        {


            byte[] bytes = new Byte[4096];
            string data = "";

            while (true)
            {

                int responseSize = clientSocket.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, responseSize);
                if (data.IndexOf("<EOF>") == -1)
                    break;
            }
            return data;  

        }

        private bool VerifyRequestedData(CustomProtocolParameters<Object> serializedRequest)
        {
             if (serializedRequest == null )
            {
                Console.WriteLine("Client disconnected - " + clientNumber);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SendErrorResponse()
        {
            byte[] responseData = Encoding.ASCII.GetBytes("requestFailed");
            SendResponse(responseData);
        }

        private void SendResponse(byte[] responseData)
        {
                     
            clientSocket.Send(responseData);
            
        }
        private void ExecuteClientRequest(CustomProtocolParameters<Object> serializedRequest)
        {
            FactoryController factoryController = new FactoryController();
            ICommonController controller = factoryController.GetController(serializedRequest.Headers["method"],serializedRequest.obj);
            string jsonString = JsonSerializer.Serialize(serializedRequest.obj);
            var reqdata = Encoding.UTF8.GetBytes(jsonString);
            string requestData = Encoding.ASCII.GetString(reqdata);
            SendResponse(controller.ExecuteRequest(serializedRequest, serializedRequest.Headers["method"]));
        }
    }
}



