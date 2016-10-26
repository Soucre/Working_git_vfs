using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SwipeJob.Core;
using SwipeJob.Model;
using SwipeJob.Model.ApiRequset;
using SwipeJob.Utility;
using System.Web.Http;
using SwipeJob.Model.Extra;

namespace SwipeJob.Web.ApiControllers
{
    [RoutePrefix("api/jobs")]
    public class ApiJobController : ApiBaseController
    {
        [Route("create")]
        [HttpPost]
        public async Task<ApiJsonResult> CreateNewJob(CreateEditJobParams createEditJobParams)
        {
            try
            {
                await new JobManager(GetCurrentUserId()).CreateNewJob(createEditJobParams);
                return new ApiJsonResult { Success = true };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("update")]
        [HttpPost]
        public async Task<ApiJsonResult> UpdateJob(CreateEditJobParams createEditJobParams)
        {
            try
            {
                await new JobManager(GetCurrentUserId()).UpdateJob(createEditJobParams);
                return new ApiJsonResult { Success = true };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("all")]
        [HttpPost]
        public async Task<ApiJsonResult> GetAllJobs(GetAllJobsParams getAllJobsParams)
        {
            try
            {
                Tuple<List<Job>, int, int> result = await new JobManager(GetCurrentUserId()).GetAllJobs(getAllJobsParams);
                return new ApiJsonPagingResult { Success = true, Data = result.Item1, TotalPages = result.Item2, TotalItems = result.Item3 };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("search")]
        [HttpPost]
        public async Task<ApiJsonResult> Search(SearchJobParams jobParams)
        {
            try
            {
                Tuple<List<Job>, int, int> result = await new JobManager().Search(jobParams);
                return new ApiJsonPagingResult { Success = true, Data = result.Item1, TotalPages = result.Item2, TotalItems = result.Item3 };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("get/{id}")]
        [HttpGet]
        public async Task<ApiJsonResult> GetJob(Guid id)
        {
            try
            {
                Job job = await new JobManager().GetJob(id);

                return new ApiJsonResult { Success = true, Data = job };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("education/{id}")]
        [HttpGet]
        public async Task<ApiJsonResult> GetEducation(Guid id)
        {
            try
            {
                JobManager jobManager = new JobManager();
                List<Education> educations = await jobManager.GetEducation(id);

                return new ApiJsonResult { Success = true, Data = educations };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("job-seeker/applicant/search")]
        [HttpPost]
        [Authorize]
        public async Task<ApiJsonResult> SearchApplicant(SearchJobSeekerApplicantParams param)
        {
            try
            {
                Guid userId = GetCurrentUserId();
                Tuple<List<SearchJobSeekerApplicantRepone>, int, int> result = await new JobManager(userId).SearchApplicant(param);

                return new ApiJsonPagingResult { Success = true, Data = result.Item1, TotalPages = result.Item2, TotalItems = result.Item3 };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("job-seeker/applicant/update")]
        [HttpPost]
        [Authorize]
        public async Task<ApiJsonResult> UpdateApplicant(UpdateApplicantParam param)
        {
            try
            {
                Guid userId = GetCurrentUserId();
                await new JobManager(userId).UpdateApplicant(param);
                return new ApiJsonPagingResult { Success = true };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("job-seeker/apply")]
        [HttpPost]
        [Authorize]
        public async Task<ApiJsonResult> ApplyJob(ApplySaveJobParams applySaveJobParams)
        {
            try
            {
                Guid userId = GetCurrentUserId();
                await new JobManager(userId).ApplyJob(applySaveJobParams.JobId);
                return new ApiJsonResult { Success = true };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("job-seeker/check-job-applied/{jobId}")]
        [HttpGet]
        [Authorize]
        public async Task<ApiJsonResult> CheckJObApplied(Guid jobId)
        {
            try
            {
                Guid userId = GetCurrentUserId();
                ApplicantStatus applicantStatus = await new JobManager(userId).CheckJObApplied(jobId);
                return new ApiJsonResult { Success = true, Data = applicantStatus };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("job-seeker/save")]
        [HttpPost]
        [Authorize]
        public async Task<ApiJsonResult> SaveJob(ApplySaveJobParams applySaveJobParams)
        {
            try
            {
                Guid userId = GetCurrentUserId();
                await new JobManager(userId).SaveJob(applySaveJobParams.JobId);
                return new ApiJsonResult { Success = true };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }
    }
}
