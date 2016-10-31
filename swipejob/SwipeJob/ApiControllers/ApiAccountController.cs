using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using SwipeJob.Core;
using SwipeJob.Utility;
using SwipeJob.Model;
using SwipeJob.Model.ApiRequset;

namespace SwipeJob.Web.ApiControllers
{
    [RoutePrefix("api/account")]
    public class ApiAccountController : ApiBaseController
    {

        [Route("login")]
        [HttpPost]
        public async Task<ApiJsonResult> Login(LoginParams loginParams)
        {
            try {
                var user = await new AccountManager().Login(loginParams);
                SetAuthenticatedUser(user.Id, loginParams.IsKeepSignIn);
                return new ApiJsonResult { Data = user, Success = true };
            }
            catch (Exception ex) {
                return ProcessException(ex);
            }
        }

        [Route("pwd/forgot")]
        [HttpPost]
        public async Task<ApiJsonResult> ForgotPassword(LoginParams email)
        {
            try {
                await new AccountManager().ForgotPassword(email.Email);
                return new ApiJsonResult { Success = true };
            }
            catch (Exception ex) {
                return ProcessException(ex);
            }
        }

        [Route("pwd/reset")]
        [HttpPost]
        public async Task<ApiJsonResult> ResetPassword(ResetPasswordParams resetPasswordParams)
        {
            try {
                await new AccountManager().ResetPassword(resetPasswordParams);
                return new ApiJsonResult { Success = true };
            }
            catch (Exception ex) {
                return ProcessException(ex);
            }
        }

        [Route("pwd/change")]
        [HttpPost]
        public async Task<ApiJsonResult> ChangePassword(ChangePasswordParams changePasswordParams)
        {
            try {
                await new AccountManager().ChangePassword(changePasswordParams);
                return new ApiJsonResult { Success = true };
            }
            catch (Exception ex) {
                return ProcessException(ex);
            }
        }

        [Route("jobseeker/register")]
        [HttpPost]
        public async Task<ApiJsonResult> Register(RegisterJobseekerParams registerJobseekerParams)
        {
            try {
                var user = await new AccountManager().RegisterJobseeker(registerJobseekerParams);
                if (registerJobseekerParams.AccountType == SwipeJob.Model.Extra.AccountType.Facebook || registerJobseekerParams.AccountType == SwipeJob.Model.Extra.AccountType.Google) {
                    SetAuthenticatedUser(user.Id, true);
                }

                return new ApiJsonResult { Success = true };
            }
            catch (Exception ex) {
                return ProcessException(ex);
            }
        }

        [Route("employer/register")]
        [HttpPost]
        public async Task<ApiJsonResult> Register(RegisterEmployerParam registerEmployerParam)
        {
            try {
                await new AccountManager().RegisterEmployer(registerEmployerParam);
                return new ApiJsonResult { Success = true };
            }
            catch (Exception ex) {
                return ProcessException(ex);
            }
        }

        [Route("jobseeker/{code}/active")]
        [HttpGet]
        public async Task<ApiJsonResult> Active(string code)
        {
            try {
                var user = await new AccountManager().ActiveJobSeekerAccount(code);
                SetAuthenticatedUser(user.Id, true);
                return new ApiJsonResult { Success = true };
            }
            catch (Exception ex) {
                return ProcessException(ex);
            }
        }

        [Route("get-current-user")]
        [Authorize]
        [HttpGet]
        public async Task<ApiJsonResult> GetCurrentUser()
        {
            try {
                Guid userId = GetCurrentUserId();
                var user = await new AccountManager(userId).GetUser(userId);
                return new ApiJsonResult { Success = true, Data = user };
            }
            catch (Exception ex) {
                return ProcessException(ex);
            }
        }

        [Route("get-company-history")]
        [Authorize]
        [HttpGet]
        public async Task<ApiJsonResult> GetCompanyHistory()
        {
            try {
                Guid userId = GetCurrentUserId();
                var companyHisotries = await new AccountManager(userId).GetCompanytHistory(userId);
                return new ApiJsonResult { Success = true, Data = companyHisotries };
            }
            catch (Exception ex) {
                return ProcessException(ex);
            }
        }

        [Route("job-seeker/profile-update/personal-info")]
        [Authorize]
        [HttpPost]
        public async Task<ApiJsonResult> UpdateJobSeekerProfile(JobSeekerProfileUpdatedPersonalInfoParams param)
        {
            try {
                Guid userId = GetCurrentUserId();
                await new AccountManager(userId).UpdateJobSeekerProfile(param);
                return new ApiJsonResult { Success = true, Data = null };
            }
            catch (Exception ex) {
                return ProcessException(ex);
            }
        }

        [Route("job-seeker/profile-update/experience-level")]
        [Authorize]
        [HttpPost]
        public async Task<ApiJsonResult> UpdateJobSeekerProfile(JobSeekerProfileUpdatedExperienceLevelParams param)
        {
            try {
                Guid userId = GetCurrentUserId();
                await new AccountManager(userId).UpdateJobSeekerProfile(param);
                return new ApiJsonResult { Success = true, Data = null };
            }
            catch (Exception ex) {
                return ProcessException(ex);
            }
        }

        [Route("job-seeker/profile-update/interest-in")]
        [Authorize]
        [HttpPost]
        public async Task<ApiJsonResult> UpdateJobSeekerProfile(JobSeekerProfileUpdatedInterestInParams param)
        {
            try {
                Guid userId = GetCurrentUserId();
                await new AccountManager(userId).UpdateJobSeekerProfile(param);
                return new ApiJsonResult { Success = true, Data = null };
            }
            catch (Exception ex) {
                return ProcessException(ex);
            }
        }

        [Route("job-seeker/profile-update/addition-info")]
        [Authorize]
        [HttpPost]
        public async Task<ApiJsonResult> UpdateJobSeekerProfile(JobSeekerProfileUpdatedAdditionInfoParam param)
        {
            try {
                Guid userId = GetCurrentUserId();
                await new AccountManager(userId).UpdateJobSeekerProfile(param);
                return new ApiJsonResult { Success = true, Data = null };
            }
            catch (Exception ex) {
                return ProcessException(ex);
            }
        }

        [Route("employer/profile-update/company-info")]
        [Authorize]
        [HttpPost]
        public async Task<ApiJsonResult> UpdateJobSeekerProfile(EmployerProfileUpdatedCompanyInfoParams param)
        {
            try {
                Guid userId = GetCurrentUserId();
                await new AccountManager(userId).UpdateEmployerProfile(param);
                return new ApiJsonResult { Success = true, Data = null };
            }
            catch (Exception ex) {
                return ProcessException(ex);
            }
        }

        [Route("employer/profile-update/overview")]
        [Authorize]
        [HttpPost]
        public async Task<ApiJsonResult> UpdateJobSeekerProfile(EmployerProfileUpdatedOverviewParams param)
        {
            try {
                Guid userId = GetCurrentUserId();
                await new AccountManager(userId).UpdateEmployerProfile(param);
                return new ApiJsonResult { Success = true, Data = null };
            }
            catch (Exception ex) {
                return ProcessException(ex);
            }
        }
    }
}
