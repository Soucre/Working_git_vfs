using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using SwipeJob.Model.Extra;

namespace SwipeJob.Web.Controllers
{
    public class AccountController : BaseController
    {
                
        [Route("logout")]
        [HttpGet]
        public ActionResult Signout()
        {
            Logout();
            return RedirectToAction("Index", "Home");
        }

        [Route("password/forgot")]
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [Route("password/reset/{code}")]
        [HttpGet]
        public ActionResult ResetPassword(string code)
        {
            ViewData["code"] = code;
            return View();
        }

        [Route("setting")]
        [HttpGet]
        public async Task<ActionResult> Setting()
        {
             GetCurrentUser();
            ViewData["UserId"] = GetCurrentUserId();
            return View();
        }
    }

}