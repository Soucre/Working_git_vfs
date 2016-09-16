using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

using System.Threading.Tasks;

using Newtonsoft.Json;
using System.Web.Helpers;
using System.Web.Caching;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [OutputCache(Duration = 0)]
    public class HomeController : Controller
    {

        public IVehicleRepository Repository { get; set; }

        public HomeController()
            : this(new VehicleRepository())
        {

        }

        public HomeController(IVehicleRepository iVehicleRepository) {
            this.Repository = iVehicleRepository;
        }

        public JsonResult Index() {
            var result = Repository.GetVehicles();

            return Json(result, JsonRequestBehavior.AllowGet);
             
        }



    }

   
}
