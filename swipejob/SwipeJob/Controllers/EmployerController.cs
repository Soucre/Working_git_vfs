using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SwipeJob.Web.Controllers
{
    /// <summary>
    /// Employer Controller
    /// </summary>
    public class EmployerController : BaseController
    {
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
        public ActionResult LoginEmployer(string returnUrl)
        {
            if (returnUrl == null) {
                returnUrl = "";
            }
            ViewData["ReturnURL"] = returnUrl;
            return View();
        }

        [Route("employer/register")]
        [HttpGet]
        public ActionResult RegisterEmployer()
        {
            return View();
        }

    }
}