using Biz.CafeF;
using Biz.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create Auto Theard with serveral Site News 
            
            Dictionary<string, int> listProxy = new Dictionary<string, int>{
                { "123.56.74.13", 8080 },
                { "46.101.22.228", 8118 },
                { "40.113.118.174", 8129 }

            };
            foreach (var item in listProxy) {
                //Thread tid = new Thread(() => DisplayAsync(item.Key, item.Value));
                //tid.Name = "Thread: " + item.Key;
                //tid.Start();

                DisplayAsync(item.Key, item.Value);
            }           

        }

        public static void DisplayAsync(string proxyIP, int proxyPort)
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
    }
}
