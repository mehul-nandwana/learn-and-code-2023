using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;
using NLog;
using NLog.Config;
namespace CafeteriaManagementSystemServer.Controller
{
    public class FactoryController
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public ICommonController GetController(CustomProtocolParameters serializedRequest)
        {
            string action = serializedRequest.Method;
            ICommonController controller = action switch
            {
                Constant.ADD_FOOD or Constant.UPDATE_FOOD or Constant.GET_FOOD or Constant.DELETE_FOOD 
                or Constant.GET_DISCARD_ITEM_LIST or Constant.GET_DISCARD_ITEM_LIST_FOR_WHICH_FEEDBACK_CAN_BE_ADDED
                => new FoodController(),
                Constant.UPDATE_USER_PROFILE => new UserController(),
                Constant.LOGIN or Constant.LOGOUT => new AuthenticationController(),
                Constant.GET_RECOMMENDATION => new RecommendationController(),
                Constant.ADD_FEEDBACK or Constant.GET_FEEDBACK or Constant.ADD_DETAIL_FEEDBACK => new FeedbackController(),
                Constant.SET_MENU or Constant.ADD_CHOICE or Constant.GET_MENU or Constant.GET_MENU_FOR_FEEDBACK => new MenuController(),
                Constant.GET_NOTIFICATION or Constant.ADD_GET_DETAIL_FEEDBACK_NOTIFICATION => new NotificationController(),
                _ => throw new ControllerNotfound()
            };
            return controller;
        }
    }
}
