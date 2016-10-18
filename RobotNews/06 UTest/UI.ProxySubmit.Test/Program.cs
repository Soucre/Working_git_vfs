using Biz.CafeF;
using Biz.Proxy;
using Biz.ProxyList;
using Dto.ProxyList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region [Proxy Submit Test]
            Test_ProxySubmit();
            #endregion
        }

        #region[Proxy Submit Function]
        private static void Test_ProxySubmit()
        {
            var listProxy = getListProxy();
            while (true) {

                foreach (var item in listProxy) {                 
                    CallSumitViaProxyAsyn(item.IPAddress, item.IPPort);
                    Console.WriteLine("Waiting ....." + item.IPAddress);
                }
                // Ngủ 1 tí
                Thread.Sleep(int.Parse(ConfigurationManager.AppSettings["TimeRequest"]) * 1000);
            }

        }

        private static async void CallSumitViaProxyAsyn(string proxyIP, int proxyPort)
        {
            var result = await SumitViaProxyAsyn(proxyIP, proxyPort);
            Console.WriteLine(result + ":" + proxyIP);
        }

        private static Task<string> SumitViaProxyAsyn(string proxyIP, int proxyPort)
        {
            return Task.Factory.StartNew(() => SumitViaProxy(proxyIP, proxyPort));
        }

        public static string SumitViaProxy(string proxyIP, int proxyPort)
        {
            Thread thr = Thread.CurrentThread;
            using (var biz = new SubmitDataBiz()) {
                biz.Method = "GET";
                biz.ProxyPort = proxyPort;
                biz.ProxyIP = proxyIP;
                biz.URLRequest = ConfigurationManager.AppSettings["SiteSubmitTest"];

                var result = biz.GetDataViaProxy();
                return thr.Name + "= " + result.StatusCode;
            }

        }

        private static List<ProxyListDto> getListProxy()
        {

            using (var biz = new ProxyListBiz()) {
                biz.URLRequest = ConfigurationManager.AppSettings["URLProxy"];
                biz.HTMLPartenReg = new Regex(@"<tr><td>\w+.*");
                biz.HTMLPartenSubReg = new Regex(@"<tr><td>|</td><td>|</td></tr>");

                return biz.GetListProxy();
            }
        }
        #endregion
    }
}
