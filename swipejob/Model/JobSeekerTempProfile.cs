using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SwipeJob.Model.Extra;

namespace SwipeJob.Model
{
    public class JobSeekerTempProfile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid IndustryId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public DateTime? DayOfBirthUtc { get; set;}

        public ExperienceLevel? ExperienceLevel { get; set; }

        public Industry Industry { get; set; }

        public DateTime RegisteredDateUtc { get; set; }
    }
}
