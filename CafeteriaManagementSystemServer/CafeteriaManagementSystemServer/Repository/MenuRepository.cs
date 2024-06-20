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
        public List<Menu> GetMenu(int id)
        {
            DateTime currentDate = DateTime.Now.Date.AddDays(1);
            var itemsForCurrentDate = DbContext.Menus
                .Where(item => item.Date == currentDate & item.MealTypeId ==id)
                .ToList();
            return itemsForCurrentDate;
        }
        public void AddChoice(Choice choice)
        {
            DbContext.Choices.Add(choice);
        }
    }
}
