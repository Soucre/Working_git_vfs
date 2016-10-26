using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using SwipeJob.Core;
using SwipeJob.Model;
using SwipeJob.Model.ApiRequset;
using SwipeJob.Utility;

namespace SwipeJob.Web.ApiControllers
{
    [RoutePrefix("api/employer")]
    public class ApiEmployerController : ApiBaseController
    {
        [Route("top-employer")]
        [HttpGet]
        public async Task<ApiJsonResult> GetTopEmployer()
        {
            try
            {
                List<User> employers = await new EmployerManager().GetTopEmployer();
                return new ApiJsonResult {Success = true, Data = employers};
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("search")]
        [HttpPost]
        public async Task<ApiJsonResult> Search(SearchEmployerParams employerParams)
        {
            try
            {
                Tuple<List<User>, int, int> result = await new EmployerManager().Search(employerParams);
                return new ApiJsonPagingResult { Success = true, Data = result.Item1, TotalPages = result.Item2, TotalItems = result.Item3 };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("{id}/profile")]
        [HttpGet]
        public async Task<ApiJsonResult> GetEmployerById(Guid id)
        {
            try
            {
                User employer = await new EmployerManager().GetEmployerById(id);
                return new ApiJsonResult { Success = true, Data = employer };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("{id}/latest-job")]
        [HttpGet]
        public async Task<ApiJsonResult> GetEmployerLatestJob(Guid id)
        {
            try
            {
                List<Job> jobs = await new JobManager().GetEmployerLatestJob(id);
                return new ApiJsonResult { Success = true, Data = jobs };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }
    }
}
