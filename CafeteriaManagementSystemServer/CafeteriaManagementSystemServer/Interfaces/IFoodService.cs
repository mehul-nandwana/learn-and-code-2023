using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Services
{
    public interface IFoodService
    {
        public Response AddFoodItem(Food food);
        public Response UpdateFoodItem(Food food);
        public Response DeleteFoodItem(int id);
    }
}
