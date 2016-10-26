using System;

namespace SwipeJob.Model.ApiRequset
{
    public class ChangePasswordParams
    {
        public Guid UserId { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
