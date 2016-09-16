using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NCommon.Web
{
    public class ApiResult
    {
        /// <summary>
        /// HttpWebRequest
        /// </summary>
        public HttpWebRequest WebRequest { get; set; }

        /// <summary>
        /// Header
        /// </summary>
        public WebHeaderCollection headers { get; set; }

        /// <summary>
        /// CookieContainer
        /// </summary>
        public CookieContainer cookieContainer { get; set; }

        /// <summary>
        /// StatusCode
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// HTML
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// 추가정보1
        /// </summary>
        public string prop1 { get; set; }

        /// <summary>
        /// 추가정보2
        /// </summary>
        public string prop2 { get; set; }

        #region 생성자
        /// <summary>
        /// 생성자
        /// </summary>
        public ApiResult()
        {
        }
        #endregion
    }
}
