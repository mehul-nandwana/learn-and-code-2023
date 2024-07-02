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
                "addfood" or "updatefood" or "deletefood" or "getdiscardItem" or "getdiscardItemforwhichfeedbackcanbeadded" => new FoodController(),
                "login" or "updateuserprofile" => new UserController(),
                "getrecommendation" => new RecommendationController(),
                "addfeedback" or "getfeedback" or "adddetailfeedback" => new FeedbackController(),
                "setmenu" or "addchoice" or "getmenu" => new MenuController(),
                "getnotification" or "addgetdetailfeedbacknotification" => new NotificationController(),
                _ => throw new ControllerNotfound()
            };
            return controller;
        }
    }
}
