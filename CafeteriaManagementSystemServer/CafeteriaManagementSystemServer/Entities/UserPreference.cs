using System;
using System.Collections.Generic;

namespace CafeteriaManagementSystemServer.Models
{
    public partial class UserPreference
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IsVegeterian { get; set; } = null!;
        public int SpiceLevel { get; set; }
        public bool Sweet { get; set; }
        public string CuisineType { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
