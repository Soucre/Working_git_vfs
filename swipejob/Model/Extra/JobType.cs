using System.ComponentModel.DataAnnotations;

namespace SwipeJob.Model.Extra
{
    public enum JobType
    {
        [Display(Name = "Full time")]
        FullTime=1,
        [Display(Name = "Part time")]
        PartTime=2
    }
}