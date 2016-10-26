using System.ComponentModel.DataAnnotations;

namespace SwipeJob.Model.Extra
{
    public enum Gender
    {
        [Display(Name = "Male")]
        Male=1,
        [Display(Name = "Female")]
        Female = 2
    }
}