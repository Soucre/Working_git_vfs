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
    public class Job
    {
        public Job()
        {
            Applicants = new HashSet<Applicant>();
            Educations = new HashSet<Education>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid EmployerId { get; set; }

        public Guid LanguageId { get; set; }

        public Guid LocationId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public JobType JobType { get; set; }

        public string JobTypeText => JobType != 0 ? JobType.GetDisplayName() : "";

        [Required]
        public string JobName { get; set; }

        [Required]
        public decimal MinSalary { get; set; }

        [Required]
        public decimal MaxSalary { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public DateTime JobStartDate { get; set; }

        public string JobDescription { get; set; }

        public bool IsStartWorkImmediately { get; set; }

        public string StartWorkingAt { get; set; }

        public string EndWorkingAt { get; set; }

        public DateTime AvailableDate { get; set; }

        public int HoursPerDay { get; set; }

        public int DayPerWeek { get; set; }

        public int DayPerMonth { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public GenderRequired GenderRequired { get; set; }

        public string GenderRequiredText => GenderRequired != 0 ? GenderRequired.GetDisplayName() : "";

        public string ExtraSalary { get; set; }

        public bool IsSalaryIncludeMealAndBreakTime { get; set; }

        public long ViewCount { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public DateTime UpdatedDateUtc { get; set; }

        public Location Location { get; set; }

        public Language Language { get; set; }

        [InverseProperty("Jobs")]
        [ForeignKey("EmployerId")]
        public Employer Employer { get; set; }

        public ICollection<Applicant> Applicants { get; set; }

        [JsonIgnore]
        public ICollection<Education> Educations { get; set; }
    }
}