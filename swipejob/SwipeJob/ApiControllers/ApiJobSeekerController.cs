using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using SwipeJob.Core;
using SwipeJob.Model;
using SwipeJob.Utility;

namespace SwipeJob.Web.ApiControllers
{
    [RoutePrefix("api/job-seeker")]
    public class ApiJobSeekerController : ApiBaseController
    {
        [Route("{id}/profile")]
        [HttpGet]
        public async Task<ApiJsonResult> GetJobSeekerById(Guid id)
        {
            try
            {
                User jobSeeker = await new JobSeekerManager().GetJobSeekerById(id);
                return new ApiJsonResult { Success = true, Data = jobSeeker };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("{id}/recent-company")]
        [HttpGet]
        public async Task<ApiJsonResult> GetCompanyHistoryForJobSeeker(Guid id)
        {
            try
            {
                List<CompanyHisotry> companyHisotries = await new JobSeekerManager().GetCompanyHistoryForJobSeeker(id);
                return new ApiJsonResult { Success = true, Data = companyHisotries };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("company-history/delete")]
        [HttpPost]
        [Authorize]
        public async Task<ApiJsonResult> DeleteCompanyHistory(CompanyHisotry companyHisotry)
        {
            try
            {
                await new JobSeekerManager(GetCurrentUserId()).DeleteCompanyHistory(companyHisotry);
                return new ApiJsonResult { Success = true };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }
    }
}
