using System;
using System.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SwipeJob.Core;
using SwipeJob.Model;
using SwipeJob.Model.ApiRequset;
using SwipeJob.Utility;
using SwipeJob.Utility.Exceptions;
using SwipeJob.Model.Extra;

namespace SwipeJob.Web.Controllers
{
    public class BaseController : Controller
    {
        protected Guid GetCurrentUserId()
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    return Guid.Parse(UtilsCryptography.Decrypt(HttpUtility.UrlDecode(User.Identity.Name)));
                }
                catch
                {
                    return Guid.Empty;
                }
            }

            return Guid.Empty;
        }

        protected CurrentUser GetCurrentUser()
        {
            try
            {
                if (!User.Identity.IsAuthenticated) {
                    return null;
                }
                Guid userId = GetCurrentUserId();
                CurrentUser currentUser = new AccountManager(userId).GetCurrentUser(userId);
                ViewData["CurrentUser"] = currentUser;
                return currentUser;
            }
            catch (Exception ex)
            {
                ViewData["CurrentUser"] = null;
                ProcessException(ex);
            }

            return null;
        }

        protected void ProcessException(Exception ex)
        {
            LoggingHelper.Log(ex);
            if (ex is UserException)
            {
                ViewData["Error"] = ex.Message;
            }
            else if (ex is SecurityException)
            {
                ViewData["Error"] = ErrorCode.NO_PERMISSION.ToString();
            }
            else
            {
                ViewData["Error"] = ErrorCode.SYSTEM_ERROR.ToString();
            }
        }

        protected void Logout()
        {
            ClearCookie(FormsAuthentication.FormsCookieName);
        }

        protected void ClearCookie(string key)
        {
            HttpCookie cookie = new HttpCookie(key)
            {
                Expires = DateTime.Now.AddDays(-1) // or any other time in the past
            };

            Response.Cookies.Add(cookie);
        }

        protected string ReplaceFileName(string filename)
        {
            char[] chars = { '/', ' ', ':' };
            string[] slipt = filename.Split(chars);
            return string.Join("-", slipt);
        }
    }
}