using Dto.CafeF;
using NCommon.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.Proxy
{
    public class SubmitDataBiz : DisposableBase
    {
        /// <summary>
        /// Proxy IP
        /// </summary>
        public string ProxyIP { get; set; }

        /// <summary>
        /// Proxy Port
        /// </summary>
        public int ProxyPort { get; set; }

        /// <summary>
        /// Methol
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// URL Request
        /// </summary>
        public string URLRequest { get; set; }

        public string PostData { get; set; }
         
        public ApiResult GetDataViaProxy()
        {
            return RequestData();
        }

        public ApiResult RequestData()
        {
            return WebReq.GetWebRequest(this.URLRequest, this.Method, this.PostData, Encoding.UTF8, null, null, 120000, null, this.ProxyIP, this.ProxyPort);

        }


    }

}
