using Dto.CafeF;
using HtmlAgilityPack;
using NCommon.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Data.CafeF
{
    public class GetRssXMLData : DisposableBase
    {
        #region [Property pulic]
        /// <summary>
        /// Rss link
        /// </summary>
        public string RssLink { get; set; }

        /// <summary>
        /// Method: Get, Post
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Data to post URL
        /// </summary>
        public string PostData { get; set; }

        /// <summary>
        /// URL Request
        /// </summary>
        public string URLRequest { get; set; }

        #endregion

        #region [Varible Member]



        #endregion

        #region [Contructor]

        public GetRssXMLData()
        {

        }

        #endregion

        #region [Extent]

        public GetRssXMLDto GetRssData()
        {
            ApiResult apiData = WebReq.GetWebRequest(this.RssLink, this.Method, this.PostData, Encoding.UTF8, null, null);
            if (apiData.StatusCode != "200")
                return null;

            byte[] byteArray = Encoding.UTF8.GetBytes(apiData.Html);
            MemoryStream stream = new MemoryStream(byteArray);
            StreamReader reader = new StreamReader(stream);

            XmlSerializer e = new XmlSerializer(typeof(GetRssXMLDto));
            var entity = (GetRssXMLDto)e.Deserialize(reader);

            if (entity != null) {
                //Update contain detail from detail link
                GetContainDetail(entity.Channel.Item);
            }

            return entity;
        }


        public void GetContainDetail(IEnumerable<Item> items)
        {
            HtmlWeb web = new HtmlWeb();
            //Update Contain detail (include HTML)
            foreach (var item in items) {
                item.ContainDetail = this.GetDetailItemHtml(web, item.Link.Trim());
            }
        }

        private string GetDetailItemHtml(HtmlWeb web, string linkURL)
        {
            HtmlDocument document = web.Load(linkURL);
            HtmlNode node = document.DocumentNode.SelectSingleNode("//div[@class='newsbody']");
            return node.InnerHtml;
        }

        #endregion

    }
}
