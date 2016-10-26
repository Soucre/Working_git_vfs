using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SwipeJob.Model.Extra;
using SwipeJob.Utility;

namespace SwipeJob.Model.ApiRequset
{
    public class SearchJobSeekerApplicantRepone
    {
        public Guid Id { get; set; }

        public Guid JobId { get; set; }

        public Guid LocationId { get; set; }

        public Guid UserId { get; set; }

        public Location Location { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public JobType JobType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ApplicantStatus ApplicantStatus { get; set; }

        public string ApplicantStatusText => ApplicantStatus != 0 ? ApplicantStatus.GetDisplayName() : "";

        public string JobTypeText => JobType != 0 ? JobType.GetDisplayName() : "";

        public string JobName { get; set; }

        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string CompanyName { get; set; }

        public byte[] Logo { get; set; }

        public string LogoImage => Logo != null ? Convert.ToBase64String(Logo) : "";
    }
}