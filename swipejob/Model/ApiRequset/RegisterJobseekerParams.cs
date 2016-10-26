
using System;
using SwipeJob.Model.Extra;

namespace SwipeJob.Model.ApiRequset
{
    public class RegisterJobseekerParams
    {
        public string AvatarPath { get; set; }

        public string FullName { get; set; }

        public string NRICNumber { get; set; }

        public NRICType NRICType { get; set; }

        public DateTime DayOfBirthUtc { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public AccountType AccountType { get; set; }

    }
}
