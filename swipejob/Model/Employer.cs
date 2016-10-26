using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SwipeJob.Model
{
    public class Employer
    {
        public Employer()
        {
            Jobs = new HashSet<Job>();
        }

        [Key, ForeignKey("User")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid UserId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string CompanyRegistrationNumber { get; set; }

        public byte[] Logo { get; set; }

        public string LogoImage => Logo != null ? Convert.ToBase64String(Logo) : "";

        public string Address { get; set; }

        public string WebLink { get; set; }

        [Required]
        public string ContactName { get; set; }

        public string PhoneNumber { get; set; }

        public string NatureOfBusiness { get; set; }

        [Column(TypeName = "ntext")]
        public string OverView { get; set; }

        public DateTime CreatedDateUtc { get; set; }

        public DateTime UpdatedDateUtc { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public ICollection<Job> Jobs { get; set; }
    }
}