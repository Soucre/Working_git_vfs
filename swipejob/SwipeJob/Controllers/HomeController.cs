using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SwipeJob.Core;
using SwipeJob.Model;

namespace SwipeJob.Web.Controllers
{
    public class HomeController : BaseController
    {
        [Route("")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            await GetCurrentUser();
            ViewData["IsHome"] = true;
            //return RedirectToAction("CommingSoon");
            return View();
        }

        [Route("subscribe")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> CommingSoon()
        {
            return View();
        }

        [Route("subscribe/success")]
        [HttpGet]
        public async Task<ActionResult> SubscribeSuccess()
        {
            return View();
        }

        [Route("about-us")]
        [HttpGet]
        public async Task<ActionResult> AboutUs()
        {
            await GetCurrentUser();
            return View();
        }

        [Route("contact-us")]
        [HttpGet]
        public async Task<ActionResult> ContactUs()
        {
            await GetCurrentUser();
            return View();
        }

        [Route("faq")]
        [HttpGet]
        public async Task<ActionResult> FAQ()
        {
            await GetCurrentUser();
            return View();
        }

        [Route("document/download/{id}")]
        [HttpGet]
        public async Task<FileResult> DownloadExport(Guid id)
        {
            JobSeeker jobSeeker = await new CommonManager().CreateTempFile(id);
            try
            {
                switch (jobSeeker.Extension)
                {
                    case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
                        return File(jobSeeker.MoreDocument, "Content-type:" + jobSeeker.Extension, ReplaceFileName(jobSeeker.FullName + ".docx"));
                    case "application/msword":
                        return File(jobSeeker.MoreDocument, "Content-type:" + jobSeeker.Extension, ReplaceFileName(jobSeeker.FullName + ".doc"));
                    case "application/pdf":
                        return File(jobSeeker.MoreDocument, "Content-type:" + jobSeeker.Extension, ReplaceFileName(jobSeeker.FullName + ".pdf"));
                }
            }
            catch (Exception ex)
            {
                ProcessException(ex);
            }
            return null;
        }
    }
}