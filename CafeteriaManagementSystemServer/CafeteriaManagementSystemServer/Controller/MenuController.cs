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
        private readonly JSonSerializer _jsonSerializer;
        public MenuController()
        {
            _menuService = new MenuService();
            _jsonSerializer = new JSonSerializer();
        }

        public override CustomProtocolParameters CallMethod(CustomProtocolParameters requestData)
        {
            string requestedMethod = requestData.Method;
            string requestedData = requestData.Obj.ToString();

            try
            {
                return requestedMethod switch
                {
                    Constant.GET_MENU => GetMenu(requestedData),
                    Constant.SET_MENU => SetMenu(requestedData),
                    Constant.ADD_CHOICE => AddChoiceForMenu(requestedData),
                    Constant.GET_MENU_FOR_FEEDBACK => GetMenuItemForFeedback(requestedData),
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

        private CustomProtocolParameters GetMenu(string userId)
        {
            int id = _jsonSerializer.DeserializeObject<int>(userId);
            return _menuService.GetMenu(id);
        }

        private CustomProtocolParameters SetMenu(string foodIds)
        {
            int[] foodId = _jsonSerializer.DeserializeObject<int[]>(foodIds);
            return _menuService.AddMenu(foodId);
        }

        private CustomProtocolParameters AddChoiceForMenu(string userChoice)
        {
            ChoiceData data = _jsonSerializer.DeserializeObject<ChoiceData>(userChoice);
            return _menuService.AddChoiceForMenu(data);
        }

        private CustomProtocolParameters GetMenuItemForFeedback(string userId)
        {
            int id = _jsonSerializer.DeserializeObject<int>(userId);
            return _menuService.GetItemsForFeedback(id);
        }
    }
}
