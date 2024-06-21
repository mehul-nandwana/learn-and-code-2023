using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Repository
{
    public class MenuRepository
    {
        public CafeteriaMangagementSystemContext DbContext;
        public MenuRepository() {
            DbContext = new CafeteriaMangagementSystemContext();
        }
        public void AddMenu(Menu menu)
        {
            DbContext.Menus.Add(menu);
            DbContext.SaveChanges();
            
        }
        public List<int> GetMenu()
        {
            //& item.MealTypeId ==id
            List<int> listOfFoodId = new List<int>();
            DateTime currentDate = DateTime.Now.Date;
            var itemsForCurrentDate = DbContext.Menus
                .Where(item => item.Date.Date == currentDate )
                .ToList();
            foreach(Menu item in itemsForCurrentDate)
            {
                listOfFoodId.Add(item.Id);
            }
            return listOfFoodId;
        }
        public void AddChoice(Choice choice)
        {
            DbContext.Choices.Add(choice);
            DbContext.SaveChanges ();
        }
    }
}
