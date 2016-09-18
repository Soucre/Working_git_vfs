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
            using (var rss = new Data.CafeF.CafeFRssData<RssCafeFDto>("http://cafef.vn/thi-truong-chung-khoan.rss")) {
                rss.Method = "GET";
                var rssBase = rss.GetRssData();

                rss.GetContainDetail(rssBase.Channel.Item);

                return rssBase;
            };
        }
    }
}
