using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SwipeJob.Model;
using SwipeJob.Model.ApiRequset;
using SwipeJob.Model.EF;
using SwipeJob.Utility;

namespace SwipeJob.Core
{
    public class HomeManager : BaseManager
    {
        public async Task Feedback (FeedbackParams feedbackParams)
        {
            Utils.CheckNullOrEmpty(new List<string> { "FullName", "Address", "Email", "PhoneNumber", "Message" }, feedbackParams.FullName, feedbackParams.Address, feedbackParams.Email, feedbackParams.PhoneNumber, feedbackParams.Message);

            using (AppDbContext context = new AppDbContext())
            {
                Feedback feedback = new Feedback
                {
                    Id = Guid.NewGuid(),
                    FullName = feedbackParams.FullName,
                    Address = feedbackParams.Address,
                    Email = feedbackParams.Email,
                    PhoneNumber = feedbackParams.PhoneNumber,
                    Message = feedbackParams.Message,
                    CreatedDateUtc = DateTime.UtcNow
                };

                context.Feedbacks.Add(feedback);
                await context.SaveChangesAsync();
            }
        }
    }
}
