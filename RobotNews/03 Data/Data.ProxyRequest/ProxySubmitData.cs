using Dto.CafeF;
using NCommon.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Data.ProxySubmit
{
    public class ProxySubmitData : DisposableBase
    {
        //#region [Property pulic]
        ///// <summary>
        ///// Rss link
        ///// </summary>
        //public string URLRequest { get; set; }

        ///// <summary>
        ///// Method: Get, Post
        ///// </summary>
        //public string Method { get; set; }

        ///// <summary>
        ///// Data to post URL
        ///// </summary>
        //public string PostData { get; set; }

        //private int RequestNumber { get; set; }

        //public string ProxyIP { get; set; }

        //public int ProxyPort { get; set; }

        //#endregion

        //public ProxySubmitData()
        //{
        //    RequestNumber = 0;
        //}

        //public string GetDataViaProxy()
        //{
        //    var result = WebReq.GetWebRequest(this.URLRequest, this.Method, this.PostData, Encoding.UTF8, null, null, 120000, null, this.ProxyIP, this.ProxyPort);
        //    if (result.StatusCode != "200") {
        //        return "That bai";
        //    }
        //    return RequestNumber++.ToString();
        //}


    }
}
