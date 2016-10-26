using System;
using SwipeJob.Model.Extra;

namespace SwipeJob.Model.ApiRequset
{
    public class SearchJobSeekerApplicantParams
    {
        public ApplicantStatus ApplicantStatus { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
