using System;
using System.ComponentModel.DataAnnotations.Schema;
using SwipeJob.Model.Extra;

namespace SwipeJob.Model
{
    public class Applicant
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid JobSeekerId { get; set; }

        public Guid JobId { get; set; }

        public ApplicantStatus ApplicantStatus { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public DateTime UpdatedDateUtc { get; set; }

        [InverseProperty("Applicants")]
        [ForeignKey("JobSeekerId")]
        public JobSeeker JobSeeker { get; set; }

        public Job Job { get; set; }
    }
}