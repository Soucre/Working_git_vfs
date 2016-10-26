using System.ComponentModel.DataAnnotations;

namespace SwipeJob.Model.Extra
{
    public enum ApplicantStatus
    {
        [Display(Name = "Applied Jobs")]
        Applied =1,
        [Display(Name = "Saved Jobs")]
        Saved=2,
        [Display(Name = "Deleted Jobs")]
        Deleted=3
    }
}