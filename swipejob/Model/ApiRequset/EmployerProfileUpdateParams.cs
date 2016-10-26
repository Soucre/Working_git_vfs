using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwipeJob.Model.ApiRequset
{
    public class EmployerProfileUpdatedCompanyInfoParams
    {
        public Guid UserId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyRegistrationNumber { get; set; }

        public string Address { get; set; }

        public string WebLink { get; set; }

        public string ContactName { get; set; }

        public string PhoneNumber { get; set; }

        public string NatureOfBusiness { get; set; }
    }

    public class EmployerProfileUpdatedOverviewParams
    {
        public Guid UserId { get; set; }

        public string OverView { get; set; }
    }
}