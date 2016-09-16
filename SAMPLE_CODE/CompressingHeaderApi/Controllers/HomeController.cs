using CompressingHeaderApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace CompressingHeaderApi.Controllers
{
    public class HomeController : Controller
    {
        [DeflateCompression]
        public JsonResult Index() {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Dictionary<object, object> dict = new Dictionary<object, object>();
            List<Employee> li = new List<Employee>();
            li.Add(new Employee { id = "2", Name = "Debendra", Id = "A123", Email = "Debendra256@gmail.com" });
            li.Add(new Employee { id = "3", Name = "Sumit", Id = "A124", Email = "Sumit@gmail.com" });
            li.Add(new Employee { id = "4", Name = "Jayant", Id = "A125", Email = "jsyant@gmail.com" });
            li.Add(new Employee { id = "5", Name = "Kumar", Id = "A126", Email = "KR@gmail.com" });


            sw.Stop();

            dict.Add("Details", li);
            dict.Add("Time", sw.Elapsed);

            return Json(dict, JsonRequestBehavior.AllowGet);

        }
    }
}
