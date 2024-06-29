using CafeteriaManagementSystemServer.Models;
using CafeteriaRecomendationEngineClient.DTO;
using CafeteriaRecomendationEngineClient.Models;
using System.Text.Json;

namespace CafeteriaRecomendationEngineClient.Client
{
    public class RequestProcessor
    {
        private Request _request;
        private readonly JSonSerializer _jsonSerializer = new JSonSerializer();
        private readonly UserInput _userInput = new UserInput();
        private readonly ConsoleOutput _consoleOutput = new ConsoleOutput();

        public Request ProcessUserLogin()
        {
            UserModel user = _userInput.GetUserCredentials();
            _request = new Request(Constant.LOGIN, user);
            return _request;
        }

        public CustomProtocolParameters ProcessRequest(string message)
        {
            CustomProtocolParameters serializedRequest = _jsonSerializer.DeserializeObject(message);
            string method = serializedRequest.Method.ToLower();

            return method switch
            {
                Constant.ADMIN_LOGIN => ProcessAdminLogin(),
                Constant.CHEF_LOGIN => ProcessChefLogin(),
                Constant.EMPLOYEE_LOGIN => ProcessEmployeeLogin((JsonElement)serializedRequest.Obj),
                Constant.SHOW_MENU => ProcessShowMenu(serializedRequest),
                Constant.SEE_NOTIFICATION => ProcessSeeNotification(serializedRequest),
                Constant.SHOW_RECOMMENDATION => ProcessShowRecommendation(serializedRequest),
                Constant.USER_LOGIN => ProcessUserLogin(),
                Constant.GET_FEEDBACK => ProcessGetFeedback(serializedRequest),
                _ => HandleInvalidInput()
            };
        }

        private CustomProtocolParameters ProcessShowMenu(CustomProtocolParameters serializedRequest)
        {
            _consoleOutput.ShowMenu(serializedRequest);
            return ProcessUserLogin();
        }

        private CustomProtocolParameters ProcessSeeNotification(CustomProtocolParameters serializedRequest)
        {
            _consoleOutput.ShowNotification(serializedRequest);
            return ProcessUserLogin();
        }

        private CustomProtocolParameters ProcessShowRecommendation(CustomProtocolParameters serializedRequest)
        {
            _consoleOutput.ShowRecommendation(serializedRequest);
            return ProcessChefLogin();
        }

        private CustomProtocolParameters ProcessGetFeedback(CustomProtocolParameters serializedRequest)
        {
            _consoleOutput.ShowFeedback(serializedRequest);
            return ProcessChefLogin();
        }

        private CustomProtocolParameters HandleInvalidInput()
        {
            _consoleOutput.DisplayWrongInputMessage();
            return ProcessUserLogin();
        }

        private CustomProtocolParameters ProcessEmployeeLogin(JsonElement serializedRequestObj)
        {
            int id = serializedRequestObj.GetInt32();
            return ProcessEmployeeLogin(id);
        }

        public CustomProtocolParameters ProcessEmployeeLogin(int id)
        {
            int choice = _userInput.GetEmployeeChoice();

            return choice switch
            {
                1 => CreateAddFeedbackRequest(id),
                2 => CreateAddChoiceRequest(id),
                3 => CreateGetNotificationRequest(),
                4 => CreateGetMenuRequest(),
                _ => ProcessEmployeeLogin(id)
            };
        }

        private CustomProtocolParameters CreateAddFeedbackRequest(int id)
        {
            FeedBackData feedbackData = _userInput.GetFeedbackData(id);
            _request = new Request(Constant.ADD_FEEDBACK, feedbackData);
            return _request;
        }

        private CustomProtocolParameters CreateAddChoiceRequest(int id)
        {
            ChoiceData choiceData = _userInput.GetChoiceData(id);
            _request = new Request(Constant.ADD_CHOICE, choiceData);
            return _request;
        }

        private CustomProtocolParameters CreateGetNotificationRequest()
        {
            _request = new Request(Constant.GET_NOTIFICATION, new FeedBackData());
            return _request;
        }

        private CustomProtocolParameters CreateGetMenuRequest()
        {
            _request = new Request(Constant.GET_MENU, string.Empty);
            return _request;
        }

        public CustomProtocolParameters ProcessChefLogin()
        {
            int choice = _userInput.GetChefChoice();

            return choice switch
            {
                1 => CreateSetMenuRequest(),
                2 => CreateGetRecommendationRequest(),
                3 => CreateGetFeedbackRequest(),
                _ => HandleInvalidChefInput()
            };
        }

        private CustomProtocolParameters CreateSetMenuRequest()
        {
            int[] rollOutItemIds = _userInput.GetRolloutIds();
            _request = new Request(Constant.SET_MENU, rollOutItemIds);
            return _request;
        }

        private CustomProtocolParameters CreateGetRecommendationRequest()
        {
            RecommendatioData recommendationData = _userInput.GetRecommendationData();
            _request = new Request(Constant.GET_RECOMMENDATION, recommendationData);
            return _request;
        }

        private CustomProtocolParameters CreateGetFeedbackRequest()
        {
            _request = new Request(Constant.GET_FEEDBACK, string.Empty);
            return _request;
        }

        private CustomProtocolParameters HandleInvalidChefInput()
        {
            _consoleOutput.DisplayWrongInputMessage();
            return ProcessChefLogin();
        }

        private CustomProtocolParameters ProcessAdminLogin()
        {
            int choice = _userInput.GetAdminChoice();

            return choice switch
            {
                1 => CreateAddFoodRequest(),
                2 => CreateUpdateFoodRequest(),
                3 => CreateDeleteFoodRequest(),
                _ => HandleInvalidAdminInput()
            };
        }

        private CustomProtocolParameters CreateAddFoodRequest()
        {
            Food food = _userInput.GetFoodEntries();
            _request = new Request(Constant.ADD_FOOD, food);
            return _request;
        }

        private CustomProtocolParameters CreateUpdateFoodRequest()
        {
            Food foodItem = _userInput.GetFoodEntries();
            _request = new Request(Constant.UPDATE_FOOD, foodItem);
            return _request;
        }

        private CustomProtocolParameters CreateDeleteFoodRequest()
        {
            int id = _userInput.GetId();
            _request = new Request(Constant.DELETE_FOOD, id);
            return _request;
        }

        private CustomProtocolParameters HandleInvalidAdminInput()
        {
            _consoleOutput.DisplayWrongInputMessage();
            return ProcessAdminLogin();
        }
    }
}
