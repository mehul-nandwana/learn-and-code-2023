using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using CafeteriaManagementSystemServer.Services;
using System.Text.Json;

namespace CafeteriaManagementSystemServer.Controller
{
    public class MenuController : ICommonController
    {
        private readonly MenuService _menuService;

        public MenuController()
        {
            _menuService = new MenuService();
        }

        public override CustomProtocolParameters CallMethod(CustomProtocolParameters requestData)
        {
            string requestedMethod = requestData.Method;
            string parameter = requestData.Obj.ToString();

            try
            {
                return requestedMethod switch
                {
                    Constant.GET_MENU => GetMenu(parameter),
                    Constant.SET_MENU => SetMenu(parameter),
                    Constant.ADD_CHOICE => AddChoice(parameter),
                    _ => throw new ControllerNotfound(),
                };
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON Deserialization error: {ex.Message}");
                throw;
            }
            catch (ControllerNotfound ex)
            {
                Console.WriteLine($"Controller not found: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        private CustomProtocolParameters GetMenu(string parameter)
        {
            int id = JsonSerializer.Deserialize<int>(parameter);
            return _menuService.GetMenu(id);
        }

        private CustomProtocolParameters SetMenu(string parameter)
        {
            int[] food = JsonSerializer.Deserialize<int[]>(parameter);
            return _menuService.AddMenu(food);
        }

        private CustomProtocolParameters AddChoice(string parameter)
        {
            ChoiceData data = JsonSerializer.Deserialize<ChoiceData>(parameter);
            return _menuService.AddChoiceForMenu(data);
        }
    }
}
