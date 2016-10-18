using Dto.ProxyList;
using HtmlAgilityPack;
using NCommon.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Biz.ProxyList
{
    public class ProxyListBiz : DisposableBase
    {
        #region [Properties]
        /// <summary>
        /// Parten to filter html
        /// </summary>
        public Regex HTMLPartenReg { get; set; }

        /// <summary>
        /// URL Request
        /// </summary>
        public string URLRequest { get; set; }

        /// <summary>
        /// Parten to filter sub html
        /// </summary>
        public Regex HTMLPartenSubReg { get; set; }

        #endregion

        #region [Variable Private]
        ///// <summary>
        ///// Regex for main HTML
        ///// </summary>
        //public Regex _htmlRegex;

        ///// <summary>
        ///// Regex for sub table
        ///// </summary>
        //public Regex _htmlRegexSub;

        #endregion

        #region [Contructor]

        public ProxyListBiz() {
            //_htmlRegex = new Regex(this.HTMLParten, RegexOptions.Compiled);
            //_htmlRegexSub = new Regex(this.HTMLPartenSub, RegexOptions.Compiled);
        }

        #endregion

        #region [User Define]

        public List<ProxyListDto> GetListProxy()
        {
            var HTMLGet = RequestData(this.URLRequest);
            var result = new List<ProxyListDto>();

            if (HTMLGet.StatusCode == "200") {
                HtmlDocument document2 = new HtmlDocument();
                document2.LoadHtml(HTMLGet.Html);

                var nodes = document2.DocumentNode.SelectSingleNode("//tbody");

                result = this.MapProxy(nodes.InnerHtml);
            }

            return result;
        }

        protected ApiResult RequestData(string urlRequest)
        {
            return WebReq.GetWebRequest(urlRequest);
        }

        protected List<ProxyListDto> MapProxy(string htmlSring)
        {
            var result = new List<ProxyListDto>();            
            var isMatchs = this.HTMLPartenReg.Matches(htmlSring);

            if (isMatchs.Count > 0) {
                foreach (var item in isMatchs) {
                    var listPart = this.HTMLPartenSubReg.Split(item.ToString());
                    result.Add(new ProxyListDto {
                        IPAddress = listPart[1],
                        IPPort = Convert.ToInt32(listPart[2]),
                        Type = listPart[6],
                        UpdatedTime = listPart[7],
                        Anonymity = listPart[5],
                        Code = listPart[3],
                        Country = listPart[4]                       

                    });

                }
            }

            return result;
        }

        #endregion
    }
}
