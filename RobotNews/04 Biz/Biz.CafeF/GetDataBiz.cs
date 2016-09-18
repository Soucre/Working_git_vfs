using Dto.CafeF;
using NCommon.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.CafeF
{
    public class GetDataBiz : DisposableBase
    {
        /// <summary>
        /// Methol
        /// </summary>
        public string Methol { get; set; }

        /// <summary>
        /// URL Request
        /// </summary>
        public string URLRequest { get; set; }

        public GetRssXMLDto GetData()
        {
            using (var data = new Data.CafeF.GetRssXMLData()) {
                data.RssLink = this.URLRequest;
                data.Method = this.Methol;

                return data.GetRssData();
            };
        }
    }
}
