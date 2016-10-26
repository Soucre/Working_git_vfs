using System;
using SwipeJob.Model.Extra;

namespace SwipeJob.Model.ApiRequset
{
    public class CreateEditJobParams
    {
        public Guid JobID { get; set; }

        public string JobName { get; set; }

        public JobType JobType { get; set; }

        public string JobDescription { get; set; }
        
        public Location Location { get; set; }

        public EducationLevel EducationLevel { get; set; }

        public Industry FieldOfStudy { get; set; }

        public Language Language { get; set; }

        public string Certification { get; set; }

        public string MinimumGrade { get; set; }

        public bool IsStartWorkImmediately { get; set; }

        public ExperienceLevel ExperienceLevel { get; set; }

        public string StartWorkingAt { get; set; }

        public string EndWorkingAt { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime JobStartDate { get; set; }

        public int HoursPerDay { get; set; }

        public int DayPerWeek { get; set; }

        public int DayPerMonth { get; set; }

        public GenderRequired GenderRequired { get; set; }

        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }

        public string ExtraSalary { get; set; }

        public bool IsSalaryIncludeMealAndBreakTime { get; set; }

    }
}