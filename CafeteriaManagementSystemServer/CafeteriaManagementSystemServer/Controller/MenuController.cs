using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using System.Text.Json;

namespace CafeteriaManagementSystemServer.Controller
{
    public class MenuController:ICommonController
    {
        MenuService menuService =new MenuService();

        public override CustomProtocolParameters CallMethod(CustomProtocolParameters requestData)
        {
            string method = requestData.Method;
            string parameter = requestData.Obj.ToString();
            if (method == "getmenu")
            {
                int id = JsonSerializer.Deserialize<int>(parameter);
                return menuService.GetMenu(id);
            }
            else if (method == "setmenu")
            {
                int[] food = JsonSerializer.Deserialize<int[]>(parameter);
                return menuService.AddMenu(food);
            }
            else 
            {
                ChoiceData data = JsonSerializer.Deserialize<ChoiceData>(parameter);
                return menuService.AddChoiceForMenu(data);
            }
        }
    }
}
