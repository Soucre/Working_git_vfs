using System.ComponentModel.DataAnnotations;

namespace SwipeJob.Model.Extra
{
    public enum ExperienceLevel
    {
        [Display(Name = "0 - 1 year")]
        F0_1 = 1,
        [Display(Name = "1 - 3 years")]
        F1_3=2,
        [Display(Name = "3 - 5 years")]
        F3_5=3
    }
}