using Biz.CafeF;
using Biz.Proxy;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            int i = 1;
            while (true) {
                string serviceName = ConfigurationManager.AppSettings["Service" + i];
                if (serviceName == null) {
                    i = 1; // Reset services then 30 minutes
                    Thread.Sleep(2 * 1000);
                    continue;
                }

                ServiceStart(serviceName.Split('|'));
                ++i;
            };


            #region [Temp]




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
            #endregion

            #region [Proxy Submit Test]
            //Test_ProxySubmit();
            #endregion

        }

        private static void ServiceStart(string[] serviceDetail)
        {
            string bizName = string.Format("{0}.GetDataBiz,{1}", serviceDetail[0], serviceDetail[0]);
            Type type = Type.GetType(bizName);
            var objectBiz = Activator.CreateInstance(type);

            type.GetProperty("Methol").SetValue(objectBiz, "GET");
            type.GetProperty("URLRequest").SetValue(objectBiz, serviceDetail[1]);

        }

        #region [CafeF Function]


        #endregion

        #region[Proxy Submit Function]
        private static void Test_ProxySubmit()
        {
            // Proxy list got from http://proxylist.hidemyass.com/
            Dictionary<string, int> listProxy = new Dictionary<string, int>{
                { "123.56.74.13", 8080 },
                { "46.101.22.228", 8118 },
                { "40.113.118.174", 8129 }
            };
            foreach (var item in listProxy) {
                // Create Auto Theard with serveral Site News
                //Thread tid = new Thread(() => DisplayAsync(item.Key, item.Value));
                //tid.Name = "Thread: " + item.Key;
                //tid.Start();
                SumitViaProxy(item.Key, item.Value);
            }
        }

        public static void SumitViaProxy(string proxyIP, int proxyPort)
        {
            Thread thr = Thread.CurrentThread;
            using (var biz = new SubmitDataBiz()) {
                biz.Methol = "GET";
                biz.ProxyPort = proxyPort;
                biz.ProxyIP = proxyIP;
                biz.URLRequest = "http://113.61.110.234/";

                var result = biz.GetDataViaProxy();
                Console.WriteLine(thr.Name + "= " + result);
            }

        }
        #endregion
    }
}
