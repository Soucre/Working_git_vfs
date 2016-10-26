using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SwipeJob.Model.Extra;

namespace SwipeJob.Model.ApiRequset
{
    public class SearchJobParams
    {
        public string JobTitle { get; set; }

        public Location Location { get; set; }

        public Industry Category { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public JobType JobType { get; set; }

        public string PositionLevel { get; set; }

        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}