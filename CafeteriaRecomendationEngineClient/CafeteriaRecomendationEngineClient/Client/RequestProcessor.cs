using CafeteriaManagementSystemServer.DTOs;
using CafeteriaRecomendationEngineClient.DTO;
using System.Text.Json;

namespace CafeteriaRecomendationEngineClient.Client
{
    public class RequestProcessor
    {
        Request _request;
        JSonSerializer _jsonSerializer = new JSonSerializer();
        UserInput _userInput = new UserInput();
        ConsoleOutput _consoleOutput = new ConsoleOutput();

        public Request ProcessUserLogin()
        {
            UserModel user = _userInput.GetUserCredentials();
            _request = new Request(Constant.LOGIN, user);
            return _request;
        }

        public Request ProcessUserLogout(string role,int id = 0)
        {
            _request = new Request(role, id);
            return _request;
        }

        public CustomProtocolParameters ProcessRequest(string message)
        {
            CustomProtocolParameters serializedRequest =  new CustomProtocolParameters();
            string method = "";
            serializedRequest = _jsonSerializer.DeserializeObject(message);
            method = serializedRequest.Method.ToLower();
            
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
                Constant.SHOW_DISCARD_ITEM => ProcessDiscardItems(serializedRequest),
                Constant.ADD_GET_DETAIL_FEEDBACK_NOTIFICATION => ProcessGetDetailFeedback(),
                Constant.SHOW_DISCARD_ITEM_FOR_FEEDBACK => ProcessFeedbackForDiscardItem(serializedRequest),
                Constant.SHOW_fEEDBACK => ProcessShowFeedback(serializedRequest),
                Constant.ADD_FEEDBACK => CreateAddFeedbackRequest(serializedRequest),
                _ => HandleInvalidInput()
            };
        }

        private CustomProtocolParameters ProcessShowMenu(CustomProtocolParameters serializedRequest)
        {
            return _consoleOutput.ShowMenu(serializedRequest);
        }

        private CustomProtocolParameters ProcessSeeNotification(CustomProtocolParameters serializedRequest)
        {
            int userId =_consoleOutput.ShowNotification(serializedRequest);
            return ProcessEmployeeLogin(userId);
        }

        private CustomProtocolParameters ProcessShowRecommendation(CustomProtocolParameters serializedRequest)
        {
            _consoleOutput.ShowRecommendation(serializedRequest);
            return ProcessChefLogin();
        }

        private CustomProtocolParameters ProcessGetFeedback(CustomProtocolParameters serializedRequest)
        {
            _consoleOutput.ShowDiscardItems(serializedRequest);
            return ProcessChefLogin();
        }

        private CustomProtocolParameters ProcessDiscardItems(CustomProtocolParameters serializedRequest)
        {
           return _consoleOutput.ShowDiscardItems(serializedRequest);
        }

        private CustomProtocolParameters ProcessShowFeedback(CustomProtocolParameters serializedRequest)
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

        private CustomProtocolParameters ProcessFeedbackForDiscardItem(CustomProtocolParameters serializedRequest)
        {
            DiscardItemData discardItemData = _jsonSerializer.DeserializeObject<DiscardItemData>(serializedRequest.Obj);
            _consoleOutput.ShowDiscardItemsForFeedback(discardItemData);
            return CreateAddDetailFeedbackRequest(discardItemData);
        }

        public CustomProtocolParameters ProcessEmployeeLogin(int id)
        {
            int choice = _userInput.GetEmployeeChoice();

            return choice switch
            {
                1 => CreateGetMenuItemForFeedbackRequest(id),
                2 => CreateGetNotificationRequest(id),
                3 => CreateGetMenuRequest(id),
                4 => CreateUpdateUserProfileRequest(id),
                5 => CreateGetItemAddDetailFeedbackRequest(id),
                6 => ProcessUserLogout(Constant.EMPLOYEE_LOGOUT,id),
                _ => ProcessEmployeeLogin(id)
            }; 
        }

        private CustomProtocolParameters CreateGetMenuItemForFeedbackRequest(int id)
        {
            _request = new Request(Constant.GET_MENU_FOR_FEEDBACK, id);
            return _request;
        }

        private CustomProtocolParameters CreateAddFeedbackRequest(CustomProtocolParameters serializedRequest)
        {
            // JsonElement id = (JsonElement)serializedRequest.Obj;
            //FeedBackData feedbackData = _userInput.GetFeedbackData(id.GetInt32());

             _consoleOutput.ShowFoodItem(serializedRequest);
            DataItem DataItem = _jsonSerializer.DeserializeObject<DataItem>(serializedRequest.Obj);
            List<string> notifications = DataItem.Message;
            int userId = DataItem.userId;
            if (notifications.Count != 0)
            {
                FeedBackData feedbackData = _userInput.GetFeedbackData(DataItem);
                _request = new Request(Constant.ADD_FEEDBACK, feedbackData);
                return _request;
            }
            else
                return ProcessEmployeeLogin(userId);
        }

        public CustomProtocolParameters CreateAddChoiceRequest(int id,List<int> menuIds)
        {
            ChoiceData choiceData = _userInput.GetChoiceData(id,menuIds);
            _request = new Request(Constant.ADD_CHOICE, choiceData);
            return _request;
        }

        private CustomProtocolParameters CreateGetNotificationRequest(int id)
        {
            _request = new Request(Constant.GET_NOTIFICATION, id);
            return _request;
        }

        private CustomProtocolParameters CreateGetMenuRequest(int id)
        {
            _request = new Request(Constant.GET_MENU,id);
            return _request;
        }

        private CustomProtocolParameters CreateGetItemAddDetailFeedbackRequest(int id)
        {
            _request = new Request(Constant.GET_DISCARD_ITEM_LIST_FOR_WHICH_FEEDBACK_CAN_BE_ADDED, id);
            return _request;
        }

        private CustomProtocolParameters CreateAddDetailFeedbackRequest(DiscardItemData discardItemData)
        {
            DetailFeedbackData userFeedback = _userInput.GetDetailFeedback(discardItemData);
            _request = new Request(Constant.ADD_DETAIL_FEEDBACK, userFeedback);
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
                4 => CreateGetDiscardItems(),
                5 => ProcessUserLogout(Constant.CHEF_LOGOUT),
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

        public CustomProtocolParameters ProcessGetDetailFeedback()
        {
            int choice = _userInput.GetValidIntInput("Enter the menu Id");
            _request = new Request(Constant.ADD_GET_DETAIL_FEEDBACK_NOTIFICATION, choice);
            return _request;
        }

        private CustomProtocolParameters CreateGetDiscardItems()
        {
            _request = new Request(Constant.GET_DISCARD_ITEM_LIST, string.Empty);
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
                4 => ProcessUserLogout(Constant.ADMIN_LOGOUT),
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

        public CustomProtocolParameters CreateDeleteFoodRequest()
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
        private CustomProtocolParameters CreateUpdateUserProfileRequest(int id)
        {
            UserProfile userprofile = _userInput.UpdateUserProfile(id);
            _request = new Request(Constant.UPDATE_USER_PROFILE, userprofile);
            return _request;
        }
    }
}
