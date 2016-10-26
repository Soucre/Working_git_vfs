using System.ComponentModel.DataAnnotations;

namespace SwipeJob.Model.Extra
{
    public enum ExperienceYear
    {
        [Display(Name = "I'm a Student")]
        Student =1,
        [Display(Name = "Fresh Graduate")]
        FreshGraduate=2,
        [Display(Name = "I have work experience")]
        Experience=3
    }
}