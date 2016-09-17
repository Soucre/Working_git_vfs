using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Data.CafeF
{
    public class RssData<TEntity>
    {
        #region [Property pulic]

        public string RssLink { get; set; }

        #endregion

        #region [Contructor]
        public RssData(string rssLink)
        {
            this.RssLink = rssLink;
        }
        #endregion

        #region [Extent]
        public TEntity GetRssData()
        {
            string method = "GET";
            string requestUrl = this.RssLink;
            string postData = "";
            
            var apiData = new NCommon.Web.WebReq().GetWebRequest(requestUrl , method, postData, Encoding.UTF8, null, null);
            byte[] byteArray = Encoding.UTF8.GetBytes(apiData.Html);
            MemoryStream stream = new MemoryStream(byteArray);
            StreamReader reader = new StreamReader(stream);

            XmlSerializer e = new XmlSerializer(typeof(TEntity));
            var entity = (TEntity)e.Deserialize(reader);


            return entity;
        }
        #endregion

    }
}
