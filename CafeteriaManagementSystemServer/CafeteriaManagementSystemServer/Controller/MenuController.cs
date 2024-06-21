using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Controller
{
    public class MenuController:ICommonController
    {
        MenuService menuService =new MenuService();

        
        public byte[] ExecuteRequest(CustomProtocolParameters<Object> requestData, string method)
        {
            Response response = CallMethod(method, requestData.obj.ToString());
            string Output = System.Text.Json.JsonSerializer.Serialize(response);
            byte[] responseData = Encoding.ASCII.GetBytes(Output);
            return responseData;
        }

        public Response CallMethod(string methodName, string par)
        {
            if (methodName == "getmenu")
            {
                return menuService.GetMenu();
            }
            else if (methodName == "setmenu")
            {
                int[] food = JsonSerializer.Deserialize<int[]>(par);
                return menuService.AddMenu(food);

            }
            else 
            {
                ChoiceData data = JsonSerializer.Deserialize<ChoiceData>(par);
                return menuService.AddChoice(data);

            }

        }

    }
}
