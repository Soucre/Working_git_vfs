using System;
using SwipeJob.Model.Extra;

namespace SwipeJob.Model.ApiRequset
{
    public class FeedbackParams
    {
        public string FullName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Message { get; set; }
    }
}
