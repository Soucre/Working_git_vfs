using SwipeJob.Model.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SwipeJob.Web.Controllers
{
    /// <summary>
    /// job-seeker Controller
    /// </summary>
    public class JobSeekerController : BaseController
    {
        [Route("job-seeker/login")]
        [HttpGet]
        public ActionResult LoginJobSeeker(string returnUrl)
        {
            if (returnUrl == null) {
                returnUrl = "";
            }
            ViewData["ReturnURL"] = returnUrl;
            return View();
        }

        [Route("job-seeker/register")]
        [HttpGet]
        public ActionResult RegisterJobSeeker()
        {
            return View();
        }

        /// <summary>
        /// Profile job-seeker
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("job-seeker/{id}/profile")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> JobSeekerProfile(Guid id)
        {
             GetCurrentUser();
            ViewData["profileId"] = id;
            return View();
        }

        [Route("job-seeker/{id}/dashboad")]
        [HttpGet]
        public async Task<ActionResult> JobSeekerDashboad(Guid id)
        {
             GetCurrentUser();
            ViewData["profileId"] = id;
            return View();
        }

        [Route("job-seeker/my-job/{applicantStatus}")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> JobSeekerMyJob(ApplicantStatus applicantStatus)
        {
             GetCurrentUser();
            ViewData["ApplicantStatus"] = applicantStatus;
            return View();
        }

        [Route("job-seeker/{code}/activation")]
        [HttpGet]
        public ActionResult ActiveAccountJobSeeker(string code)
        {
            ViewData["code"] = code;
            return View();
        }

    }
}