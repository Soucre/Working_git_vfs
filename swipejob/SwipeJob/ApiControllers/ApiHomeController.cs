using System;
using System.Threading.Tasks;
using System.Web.Http;
using SwipeJob.Model.ApiRequset;
using SwipeJob.Utility;
using SwipeJob.Core;
using SwipeJob.Model.Extra;
using SwipeJob.Utility.Exceptions;

namespace SwipeJob.Web.ApiControllers
{
    [RoutePrefix("api")]
    public class ApiHomeController : ApiBaseController
    {
        [Route("comming-soon/register")]
        [HttpPost]
        public async Task<ApiJsonResult> Register(ArgJobSeekerTemp arg)
        {
            try
            {
                JobSeekerTempManager jobSeekerTempManager = new JobSeekerTempManager();
                bool isExistEmail = await jobSeekerTempManager.CheckEmail(arg);
                if (!isExistEmail)
                {
                    throw new UserException(ErrorCode.EMAIL_EXISTED.ToString());
                }

                await jobSeekerTempManager.Register(arg);
                return new ApiJsonPagingResult { Success = true };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }

        [Route("feedback")]
        [HttpPost]
        public async Task<ApiJsonResult> Feedback(FeedbackParams feedbackParams)
        {
            try
            {
                await new HomeManager().Feedback(feedbackParams);
                return new ApiJsonResult { Success = true };
            }
            catch (Exception ex)
            {
                return ProcessException(ex);
            }
        }
    }
}
