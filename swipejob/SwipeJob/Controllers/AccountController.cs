using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SwipeJob.Model.Extra;

namespace SwipeJob.Web.Controllers
{
    public class AccountController : BaseController
    {
        [Route("job-seeker/login")]
        [HttpGet]
        public async Task<ActionResult> LoginJobSeeker(string returnUrl)
        {
            if (returnUrl == null)
            {
                returnUrl = "";
            }
            ViewData["ReturnURL"] = returnUrl;
            return View();
        }

        [Route("job-seeker/register")]
        [HttpGet]
        public async Task<ActionResult> RegisterJobSeeker()
        {
            return View();
        }

        [Route("job-seeker/{id}/profile")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> JobSeekerProfile(Guid id)
        {
            await GetCurrentUser();
            ViewData["profileId"] = id;
            return View();
        }

        [Route("job-seeker/{id}/dashboad")]
        [HttpGet]
        public async Task<ActionResult> JobSeekerDashboad(Guid id)
        {
            await GetCurrentUser();
            ViewData["profileId"] = id;
            return View();
        }

        [Route("job-seeker/my-job/{applicantStatus}")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> JobSeekerMyJob(ApplicantStatus applicantStatus)
        {
            await GetCurrentUser();
            ViewData["ApplicantStatus"] = applicantStatus;
            return View();
        }

        [Route("job-seeker/{code}/activation")]
        [HttpGet]
        public async Task<ActionResult> ActiveAccountJobSeeker(string code)
        {
            ViewData["code"] = code;
            return View();
        }

        //employer

        [Route("employer/{id}/profile")]
        [HttpGet]
        public async Task<ActionResult> EmployerProfile(Guid id)
        {
            await GetCurrentUser();
            ViewData["profileId"] = id;
            return View();
        }

        [Route("employer/{id}/dashboad")]
        [HttpGet]
        public async Task<ActionResult> EmployerDashboad(Guid id)
        {
            await GetCurrentUser();
            ViewData["profileId"] = id;
            return View();
        }
        [Route("employer/login")]
        [HttpGet]
        public async Task<ActionResult> LoginEmployer(string returnUrl)
        {
            if (returnUrl == null)
            {
                returnUrl = "";
            }
            ViewData["ReturnURL"] = returnUrl;
            return View();
        }

        [Route("employer/register")]
        [HttpGet]
        public async Task<ActionResult> RegisterEmployer()
        {
            return View();
        }

        [Route("logout")]
        [HttpGet]
        public async Task<ActionResult> Signout()
        {
            Logout();
            return RedirectToAction("Index","Home");
        }

        [Route("password/forgot")]
        [HttpGet]
        public async Task<ActionResult> ForgotPassword()
        {
            return View();
        }

        [Route("password/reset/{code}")]
        [HttpGet]
        public async Task<ActionResult> ResetPassword(string code)
        {
            ViewData["code"] = code;
            return View();
        }

        [Route("setting")]
        [HttpGet]
        public async Task<ActionResult> Setting()
        {
            await GetCurrentUser();
            ViewData["UserId"] = GetCurrentUserId();
            return View();
        }
    }

}