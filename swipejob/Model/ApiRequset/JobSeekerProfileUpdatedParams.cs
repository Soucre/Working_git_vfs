using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SwipeJob.Model.Extra;

namespace SwipeJob.Model.ApiRequset
{
    public class JobSeekerProfileUpdatedPersonalInfoParams
    {
        public Guid UserId { get; set; }

        public string FullName { get; set; }

        public Gender Gender { get; set; }

        public NationnalServiceStatus NationalServiceStatus { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string NRICNumber { get; set; }

        
        public NRICType NRICType { get; set; }

        public string Race { get; set; }

        public string Religions { get; set; }

        public string PhoneNumber { get; set; }

        public string MobileNumber { get; set; }

        public string PostalCode { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
    }

    public class JobSeekerProfileUpdatedExperienceLevelParams
    {
        public Guid UserId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ExperienceYear ExperienceYear { get; set; }

        public string HighestEducation { get; set; }

        public Language Language { get; set; }

        public ICollection<CompanyHisotry> CompanyHistories { get; set; }

        public string ExpectedPosition { get; set; }

        public string ExpectedLocation { get; set; }

        public string ExpectedJobCategory { get; set; }

        public decimal ExpectedSalary { get; set; }

        public bool CanNegotiation { get; set; }
    }

    public class JobSeekerProfileUpdatedInterestInParams
    {
        public Guid UserId { get; set; }

        public string InterestIn { get; set; }
    }

    public class JobSeekerProfileUpdatedAdditionInfoParam
    {
        public Guid UserId { get; set; }

        public string AdditionalInfo { get; set; }
    }
}