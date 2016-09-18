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
        public string Methol { get; set; }

        /// <summary>
        /// URL Request
        /// </summary>
        public string URLRequest { get; set; }

        public string GetDataViaProxy()
        {
            using (var data = new Data.ProxySubmit.ProxySubmitData()) {
                data.ProxyIP = this.ProxyIP;
                data.ProxyPort = this.ProxyPort;
                data.Method = this.Methol;
                data.URLRequest = this.URLRequest;

                return data.GetDataViaProxy();
            }
        }


    }

}
