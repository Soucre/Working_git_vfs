using Biz.CafeF;
using Biz.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region [CafeF Test]
            //string bizName = "Biz.CafeF.GetDataBiz";
            ////var objected = Activator.CreateInstance(Type.GetType(bizName));
            //Type t = Type.GetType(bizName);
            //MethodInfo method = t.GetMethod("GetData", BindingFlags.Static | BindingFlags.Public);

            //method.Invoke(null, null);
            //var response = instance.GetData();

            //using (var biz = new Biz.CafeF.GetDataBiz()) {
            //    biz.Methol = "GET";
            //    biz.URLRequest = "http://cafef.vn/doanh-nghiep.rss";

            //    var result = biz.GetData();
            //    if (result != null) {
            //        Console.WriteLine(string.Format("Get Sucessed: {0}", "http://cafef.vn/thi-truong-chung-khoan.rss"));
            //    }
            //}

            #endregion

            #region [Proxy Submit Test]
            Test_ProxySubmit();
            #endregion

        }

        #region [CafeF Function]


        #endregion

        #region[Proxy Submit Function]
        private static void Test_ProxySubmit()
        {

            // Proxy list got from http://proxylist.hidemyass.com/
            var listProxy = new Dictionary<string, int>{
                { "123.56.74.13", 8080 },
                { "46.101.22.228", 8118 },
                { "40.113.118.174", 8129 }
            };
            while (true) {
                foreach (var item in listProxy) {
                    //Create Auto Theard with serveral Site News
                    //Thread tid = new Thread(() => SumitViaProxy(item.Key, item.Value));
                    //tid.Name = "Thread: " + item.Key;
                    //tid.Start();
                    //SumitViaProxy(item.Key, item.Value);
                    CallSumitViaProxyAsyn(item.Key, item.Value);
                    Console.WriteLine("Waiting ....." + item.Key);
                }
                Thread.Sleep(1000);
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
                biz.URLRequest = "http://113.61.110.234/";

                var result = biz.GetDataViaProxy();
                return thr.Name + "= " + result.StatusCode;
            }

        }
        #endregion
    }
}
