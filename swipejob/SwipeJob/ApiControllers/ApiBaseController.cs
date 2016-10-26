using System;
using System.Security;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using SwipeJob.Model.Extra;
using SwipeJob.Utility;
using SwipeJob.Utility.Exceptions;

namespace SwipeJob.Web.ApiControllers
{
    public class ApiBaseController : ApiController
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

        protected void SetAuthenticatedUser(Guid userId,bool value)
        {
            FormsAuthentication.SetAuthCookie(HttpUtility.UrlEncode(UtilsCryptography.Encrypt(userId.ToString())), value);
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

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        protected ApiJsonResult ProcessException(Exception ex)
        {
            LoggingHelper.Log(ex);
            if (ex is UserException)
            {
                return new ApiJsonResult { Success = false, Data = ex.Message };
            }

            if (ex is SecurityException)
            {
                return new ApiJsonResult { Success = false, Data = ErrorCode.NO_PERMISSION.ToString() };
            }

            return new ApiJsonResult { Success = false, Data = ErrorCode.SYSTEM_ERROR + "=>" + ex.Message };
        }

        protected string ReplaceFileName(string filename)
        {
            char[] chars = { '/', ' ', ':' };
            string[] slipt = filename.Split(chars);
            return string.Join("-", slipt);
        }
    }
}