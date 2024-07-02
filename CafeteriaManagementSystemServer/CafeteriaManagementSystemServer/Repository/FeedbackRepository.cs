using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CafeteriaManagementSystemServer.DTOs;
using CafeteriaManagementSystemServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeteriaManagementSystemServer.Repository
{
    public class FeedbackRepository:IFeedbackRepository
    {
        List<Feedback> feedbacks = new List<Feedback>
        {
            new Feedback { Id = 1, MenuId = 1, Rating = 1, Comment = "Tasteless" },
            new Feedback { Id = 2, MenuId = 1, Rating = 2, Comment = "Not worth having" },
            new Feedback { Id = 3, MenuId = 2, Rating = 3, Comment = "Excellent" },
            new Feedback { Id = 4, MenuId = 2, Rating = 1, Comment = "extremely bad experience" },
            new Feedback { Id = 5, MenuId = 3, Rating = 1, Comment = "Good" },
            new Feedback { Id = 6, MenuId = 3, Rating = 1, Comment = "Very Bad" }
        };

        public CafeteriaMangagementSystemContext DbContext;
        string[] specificSentimentStrings = { "Tasteless", "Not worth having", "extremely bad experience" };

        public FeedbackRepository()
        {
            DbContext = new CafeteriaMangagementSystemContext();
        }

        public string AddFeedback(Feedback feedback)
        {
            Feedback _feedback = DbContext.Feedbacks
    .Where(x => x.UserId == feedback.UserId && x.Feedbackdate.Date == DateTime.Now.Date && x.MenuId == feedback.MenuId && x.MealId == feedback.MealId)
    .OrderBy(x => x.Id) // Replace 'Id' with the appropriate property you want to order by
    .LastOrDefault();
            //Feedback _feedback = DbContext.Feedbacks.Where(x => x.UserId == feedback.UserId && x.Feedbackdate == DateTime.Now.Date && x.MenuId == feedback.MenuId && x.MealId == feedback.MealId).LastOrDefault();
            if (_feedback == null)
            {
                DbContext.Feedbacks.Add(feedback);
                DbContext.SaveChanges();
                return "Feedback Updated Successfully";
            }
            else
            {
                UpdateFeedback(_feedback.Id, feedback);
                return "Added Feedback Successfully";
            }
        }

        public void UpdateFeedback(int _feedbackId, Feedback feedback)
        {
            Feedback _feedback = DbContext.Feedbacks.Where(x => x.Id == _feedbackId).FirstOrDefault();
            _feedback.Rating = feedback.Rating;
            _feedback.Comment = feedback.Comment;
            _feedback.Sentimentscore = feedback.Sentimentscore;
            DbContext.Feedbacks.Update(_feedback);
            DbContext.SaveChanges();
        }


        public void AddDetailFeedback(DetailFeedback detailFeedback)
        {
            DbContext.DetailFeedbacks.Add(detailFeedback);
            DbContext.SaveChanges();
        }


        public List<Feedback> GetFeedbacks()
        {
            return DbContext.Feedbacks.ToList();
        }

        public List<int> GetDiscardItem()
        {
            var menuIdRatingLessThanTwo = feedbacks
            .GroupBy(f => f.MenuId)
            .Select(g => new
            {
                MenuId = g.Key,
                AverageRating = g.Average(f => f.Rating)
            })
            .Where(g => g.AverageRating < 2)
            .Select(g => g.MenuId).ToList();


            foreach (var menuId in menuIdRatingLessThanTwo)
            {
                Console.WriteLine($"MenuId with average rating < 2: {menuId}");
            }

            var menuIdsWithSpecificSentiments = feedbacks
            .Where(f => menuIdRatingLessThanTwo.Contains(f.MenuId) && specificSentimentStrings.Any(s => f.Comment.Contains(s, StringComparison.OrdinalIgnoreCase)))
            .Select(f => f.MenuId)
            .Distinct()
            .ToList();

            return menuIdsWithSpecificSentiments;

            //var SpecificSentiment = DbContext.Feedbacks
            //.Where(f => f.MenuId  && f.Sentiment.Contains(specificSentimentString))
            //.Select(f => new { f.MenuId, f.FeedbackId, f.Sentiment })
            //.ToList();

        }

    }
}
