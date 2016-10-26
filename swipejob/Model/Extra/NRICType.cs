using System.ComponentModel.DataAnnotations;

namespace SwipeJob.Model.Extra
{
    public enum NRICType
    {
        [Display(Name = "Singapore Citizen")]
        Citizen=1,
        [Display(Name = "Singapore Permanent Resident")]
        PermanentResident=2
    }
}