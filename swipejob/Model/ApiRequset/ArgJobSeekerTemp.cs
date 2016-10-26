using System;
using SwipeJob.Model.Extra;

namespace SwipeJob.Model.ApiRequset
{
    public class ArgJobSeekerTemp
    {

        public string FullName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime? DayOfBirthUtc { get; set; }

        public ExperienceLevel ExperienceLevel { get; set; }

        public Industry Industry { get; set; }

    }
}