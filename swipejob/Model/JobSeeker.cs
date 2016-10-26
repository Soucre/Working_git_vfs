using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SwipeJob.Model.Extra;
using SwipeJob.Utility;

namespace SwipeJob.Model
{
    public class JobSeeker
    {
        public JobSeeker()
        {
            CompanyHistories = new HashSet<CompanyHisotry>();
            JobSeekerMessages = new HashSet<Inbox>();
            Applicants = new HashSet<Applicant>();
        }

        [Key, ForeignKey("User")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserId { get; set; }

        //personal info
        public byte[] Avartar { get; set; }

        public string AvartarImage => Avartar != null ? Convert.ToBase64String(Avartar) : "";

        [Required]
        public string FullName { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }

        public string GenderText => Gender != 0 ? Gender.GetDisplayName() : "";

        [JsonConverter(typeof(StringEnumConverter))]
        public NationnalServiceStatus NationalServiceStatus { get; set; }

        public string NationalServiceStatusText => NationalServiceStatus != 0 ? NationalServiceStatus.GetDisplayName() : "";

        public DateTime? DateOfBirth { get; set; }

        public string NRICNumber { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public NRICType NRICType { get; set; }

        public string NRICTypeText => NRICType != 0 ? NRICType.GetDisplayName() : "";

        public string Race { get; set; }

        public string Religions { get; set; }

        public string PhoneNumber { get; set; }

        public string MobileNumber { get; set; }

        public string PostalCode { get; set; }

        public string Address { get; set; }

        public byte[] MoreDocument { get; set; }

        public string Extension { get; set; }

        //experience level

        [JsonConverter(typeof(StringEnumConverter))]
        public ExperienceYear ExperienceYear { get; set; }

        public string ExperienceYearText => ExperienceYear != 0 ? ExperienceYear.GetDisplayName() : "";

        public string HighestEducation { get; set; }

        public Guid LanguageId { get; set; }

        public string ExpectedPosition { get; set; }

        public string ExpectedLocation { get; set; }

        public string ExpectedJobCategory { get; set; }

        public decimal ExpectedSalary { get; set; }

        public bool CanNegotiation { get; set; }

        // Interest

        [Column(TypeName = "ntext")]
        public string InterestIn { get; set; }

        [Column(TypeName = "ntext")]
        public string AdditionalInfo { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public DateTime UpdatedDateUtc { get; set; }

        public ICollection<Inbox> JobSeekerMessages { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public Language Language { get; set; }

        public ICollection<CompanyHisotry> CompanyHistories { get; set; }

        [JsonIgnore]
        public ICollection<Applicant> Applicants { get; set; }
    }
}