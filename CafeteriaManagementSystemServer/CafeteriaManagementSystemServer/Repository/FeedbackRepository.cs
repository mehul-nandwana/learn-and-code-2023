using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;

namespace CafeteriaManagementSystemServer.Repository
{
    public class FeedbackRepository
    {
        public CafeteriaMangagementSystemContext DbContext;
        public void AddFeedback(Feedback feedback)
        {
            DbContext.Feedbacks.Add(feedback);
        }
        public List<Feedback> GetFeedbacks()
        {
            return DbContext.Feedbacks.ToList();
        }
    }
}
