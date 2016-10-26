using SwipeJob.Model.Extra;

namespace SwipeJob.Model.ApiRequset
{
    public class CurrentUser
    {
        public User User { get; set; }

        public int AppliedJob { get; set; }

        public int SavedJob { get; set; }

        public int DeletedJob { get; set; }

        public int TotalPostJob { get; set; }
    }
}