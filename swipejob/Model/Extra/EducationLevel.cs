using System.ComponentModel.DataAnnotations;

namespace SwipeJob.Model.Extra
{
    public enum EducationLevel
    {
        [Display(Name = "Doctoral or professional degree")]
        Docoral =1,
        [Display(Name = "Master's degree")]
        Master = 2,
        [Display(Name = "Bachelor's degree")]
        Bachelor = 3,
        [Display(Name = "Associate's degree")]
        Associate = 4,
        [Display(Name = "Postsecondary nondegree award")]
        Postsecondary = 5,
        [Display(Name = "Some college, no degree")]
        CollegeNodegree=6,
        [Display(Name = "High school diploma or equivalent")]
        HighSchool = 7,
        [Display(Name = "No formal educational credential")]
        NoFormalEducational=8
    }
}