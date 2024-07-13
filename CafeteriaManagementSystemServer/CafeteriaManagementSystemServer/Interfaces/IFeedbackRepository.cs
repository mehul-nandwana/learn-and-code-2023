using CafeteriaManagementSystemServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeteriaManagementSystemServer.Repository
{
    public interface IFeedbackRepository
    {
        public string AddFeedback(Feedback feedback);
        public List<Feedback> GetFeedbacks();
    }
}
