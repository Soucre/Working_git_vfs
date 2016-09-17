using Dto.CafeF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.CafeF
{
    public class RssBiz
    {
        public static RssCafeFDto GetData()
        {
            var rss = new Data.CafeF.RssData<RssCafeFDto>("http://cafef.vn/thi-truong-chung-khoan.rss");
            return rss.GetRssData();
        }
    }
}
