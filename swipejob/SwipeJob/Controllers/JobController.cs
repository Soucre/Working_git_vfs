using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SwipeJob.Web.Controllers
{
    [RoutePrefix("job")]
    public class JobController : BaseController
    {
        [Route("search")]
        [HttpGet]
        public async  Task<ActionResult> Search()
        {
             GetCurrentUser();
            return View();
        }

        [Route("{id}/detail")]
        [HttpGet]
        public async Task<ActionResult> JobDetail(Guid id)
        {
             GetCurrentUser();
            ViewData["id"] = id;
            return View();
        }

        [Route("employer/update")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> CreateEditJob(Guid? jobId)
        {
             GetCurrentUser();
            if (jobId.HasValue)
            {
                ViewData["JobId"] = jobId;
            }
            else
            {
                ViewData["JobId"] = "";
            }
            return View();
        }

        [Route("employer/all")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> EmployerJobs()
        {
             GetCurrentUser();
            return View();
        }
    }
}