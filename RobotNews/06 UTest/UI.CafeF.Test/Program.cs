using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UI.CafeF.Test
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
            }




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
        }
        private static void ServiceStart(string[] serviceDetail)
        {
            string bizName = string.Format("{0}.GetDataBiz,{1}", serviceDetail[0], serviceDetail[0]);
            Type type = Type.GetType(bizName);
            var objectBiz = Activator.CreateInstance(type);

            type.GetProperty("Methol").SetValue(objectBiz, "GET");
            type.GetProperty("URLRequest").SetValue(objectBiz, serviceDetail[1]);

        }
    }


}
