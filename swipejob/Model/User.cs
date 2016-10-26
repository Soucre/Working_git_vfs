using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SwipeJob.Model.Extra;

namespace SwipeJob.Model
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(225)]
        public string Email { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AccountType AccountType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public UserType UserType { get; set; }
        
        public DateTime RegisteredDateUtc { get; set; }

        public bool IsActivated { get; set; }

        public bool IsLocked { get; set; }

        public DateTime? LockedDateUtc { get; set; }

        public string ConfirmationCode { get; set; }

        public JobSeeker JobSeeker { get; set; }

        public Employer Employer { get; set; }
    }
}