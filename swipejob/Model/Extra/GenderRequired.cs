using System.ComponentModel.DataAnnotations;

namespace SwipeJob.Model.Extra
{
    public enum GenderRequired
    {
        [Display(Name = "Male")]
        Male = 1,
        [Display(Name = "Female")]
        Female = 2,
        [Display(Name = "Male and Female")]
        Both =3
    }
}