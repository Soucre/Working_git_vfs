using System.ComponentModel.DataAnnotations;

namespace SwipeJob.Model.Extra
{
    public enum Frequency
    {
        [Display(Name = "Daily")]
        Daily=1,
        [Display(Name = "Weekly")]
        Weekly=2,
        [Display(Name = "Monthly")]
        Monthly=3
    }
}