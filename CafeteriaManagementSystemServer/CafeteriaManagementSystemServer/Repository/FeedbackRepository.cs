using CafeteriaManagementSystemServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeteriaManagementSystemServer.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly CafeteriaMangagementSystemContext _dbContext;
        private readonly string[] _specificSentimentStrings = { "Tasteless", "Not worth having", "extremely bad experience" };

        public FeedbackRepository()
        {
            _dbContext = new CafeteriaMangagementSystemContext();
        }

        public string AddFeedback(Feedback feedback)
        {
            var existingFeedback = _dbContext.Feedbacks
                .Where(f => f.UserId == feedback.UserId && f.Feedbackdate.Date == DateTime.Now.Date && f.MenuId == feedback.MenuId && f.MealId == feedback.MealId)
                .OrderByDescending(f => f.Id)
                .FirstOrDefault();

            if (existingFeedback == null)
            {
                _dbContext.Feedbacks.Add(feedback);
                _dbContext.SaveChanges();
                return Constant.FEEDBACK_ADDED_SUCCESSFULLY;
            }
            else
            {
                UpdateFeedback(existingFeedback.Id, feedback);
                return Constant.FEEDBACK_UPDATED_SUCCESSFULLY;
            }
        }

        public void UpdateFeedback(int feedbackId, Feedback feedback)
        {
            var existingFeedback = _dbContext.Feedbacks.Find(feedbackId);
            if (existingFeedback != null)
            {
                existingFeedback.Rating = feedback.Rating;
                existingFeedback.Comment = feedback.Comment;
                existingFeedback.Sentimentscore = feedback.Sentimentscore;
                _dbContext.Feedbacks.Update(existingFeedback);
                _dbContext.SaveChanges();
            }
        }

        public void AddDetailFeedback(DetailFeedback detailFeedback)
        {
            _dbContext.DetailFeedbacks.Add(detailFeedback);
            _dbContext.SaveChanges();
        }

        public List<Feedback> GetFeedbacks()
        {
            return _dbContext.Feedbacks.ToList();
        }

        public List<int> GetDiscardItem()
        {
            var menuIdsWithLowRatings = _dbContext.Feedbacks
        .GroupBy(f => f.MenuId)
        .Select(g => new
        {
            MenuId = g.Key,
            AverageRating = g.Average(f => f.Rating)
        })
        .Where(g => g.AverageRating < 2)
        .Select(g => g.MenuId)
        .ToList();

            var feedbacksWithLowRatings = _dbContext.Feedbacks
        .Where(f => menuIdsWithLowRatings.Contains(f.MenuId))
        .ToList();

            var menuIdsWithSpecificSentiments = feedbacksWithLowRatings
                .Where(f => _specificSentimentStrings.Any(s => f.Comment.Contains(s, StringComparison.OrdinalIgnoreCase)))
                .Select(f => f.MenuId)
                .Distinct()
                .ToList();

            return menuIdsWithSpecificSentiments;
        }

        public double AverageFeedback(int foodId)
        {
            var ratings = _dbContext.Feedbacks
                .Where(f => f.MenuId == foodId)
                .Select(f => (double)f.Rating)
                .ToList();

            return ratings.DefaultIfEmpty(0.0).Average();
        }
    }
}
