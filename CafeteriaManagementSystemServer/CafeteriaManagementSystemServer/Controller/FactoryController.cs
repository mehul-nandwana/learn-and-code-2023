using CafeteriaManagementSystemServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Controller
{
    public  class FactoryController
    {
        public ICommonController GetController(string action, Object obj)
        {
            ICommonController controller = null;
            if (action.Equals("addfood") || action.Equals("updatefood") || action.Equals("deletefood"))
            {

                controller = new AdminController();
            }
            else if (action.Equals("login"))
            {
                //UserModel userModel = JsonSerializer.Deserialize<UserModel>(obj.ToString());
                controller = new UserController();
            }
            else if(action.Equals("getrecommendation"))
            {
                controller = new RecommendationController();
            }
            else if(action.Equals("addfeedback"))
            {
                controller = new FeedbackController();

            }
            else if(action.Equals("setmenu") || action.Equals("addmenu") || action.Equals("getmenu"))
            {
                controller = new MenuController();
            }
           else if(action.Equals("getNotification"))
            {
                controller = new NotificationController();
            }
            else
            {
                throw new ("No controller found for " + action);
            }
            return controller;
        }
    }
}
