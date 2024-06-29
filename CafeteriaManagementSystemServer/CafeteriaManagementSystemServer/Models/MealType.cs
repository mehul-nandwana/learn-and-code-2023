using System;
using System.Collections.Generic;

namespace CafeteriaManagementSystemServer.Models
{
    public partial class MealType
    {
        public MealType()
        {
            Choices = new HashSet<Choice>();
            Menus = new HashSet<Menu>();
        }

        public int Id { get; set; }
        public string MealType1 { get; set; } = null!;

        public virtual ICollection<Choice> Choices { get; set; }
        public virtual ICollection<Menu> Menus { get; set; }
    }
}
