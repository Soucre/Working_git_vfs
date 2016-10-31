using Biz.ProxyList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UI.ProxyList.Test
{
    class Program
    {

        static void Main(string[] args)
        {
            using (var biz = new ProxyListBiz()) {
                GetListProxy();
            }

        }

        public static void GetListProxy()
        {
            using (var biz = new ProxyListBiz()) {
                biz.URLRequest = ConfigurationManager.AppSettings["URLProxy"];
                biz.HTMLPartenReg = new Regex(@"<tr><td>\w+.*");
                biz.HTMLPartenSubReg = new Regex(@"<tr><td>|</td><td>|</td></tr>");

                Base.Execute(biz.GetListProxy());
            }
        }
    }
}
