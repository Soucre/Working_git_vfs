using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SwipeJob.Model.Extra;

namespace SwipeJob.Model.ApiRequset
{
    public class UpdateApplicantParam
    {
        public Guid Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ApplicantStatus ApplicantStatus { get; set; }
    }
}