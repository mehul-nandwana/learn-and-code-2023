using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Controller
{
    public interface IFoodController
    {
        public Response AddFood(Food food);
        public Response UpdateFood(Food food);
        public Response DeleteFood(int id);

    }
}
