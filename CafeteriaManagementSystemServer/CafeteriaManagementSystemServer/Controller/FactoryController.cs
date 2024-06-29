using CafeteriaManagementSystemServer.ExceptionHandler;
using CafeteriaManagementSystemServer.Models;

namespace CafeteriaManagementSystemServer.Controller
{
    public class FactoryController
    {
        public ICommonController GetController(CustomProtocolParameters serializedRequest)
        {
            string action = serializedRequest.Method;
            ICommonController controller = action switch
            {
                "addfood" or "updatefood" or "deletefood" => new FoodController(),
                "login" => new UserController(),
                "getrecommendation" => new RecommendationController(),
                "addfeedback" or "getfeedback" => new FeedbackController(),
                "setmenu" or "addchoice" or "getmenu" => new MenuController(),
                "getnotification" => new NotificationController(),
                _ => throw new ControllerNotfound()
            };
            return controller;
        }
    }
}
