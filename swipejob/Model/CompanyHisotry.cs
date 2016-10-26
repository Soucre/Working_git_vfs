using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace SwipeJob.Model
{
    public class CompanyHisotry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid JobSeekerId { get; set; }

        public string CompanyName { get; set; }

        public string Position { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public DateTime UpdatedDateUtc { get; set; }

        [InverseProperty("CompanyHistories")]
        [ForeignKey("JobSeekerId")]
        public JobSeeker JobSeeker { get; set; }
    }
}