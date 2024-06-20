using System;
using System.Collections.Generic;

namespace CafeteriaManagementSystemServer.Models
{
    public partial class Choice
    {
        public int Id { get; set; }
        public int MealTypeId { get; set; }
        public int MenuId { get; set; }
        public DateTime Time { get; set; }
        public int UserId { get; set; }
        public bool IsChoiceAdded { get; set; }

        public virtual MealType MealType { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
