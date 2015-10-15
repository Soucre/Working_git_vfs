using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhimHang.Models;
using PagedList;
using System.Data.Entity;
using System.Data.SqlClient;

namespace PhimHang.Controllers
{
    public class ResearchController : Controller
    {
        //
        // GET: /Research/
        [Authorize]
        public async Task<ActionResult> Index()
        {
            ViewBag.DescriptionTile = Resources.VN_Resources.Research_Title;
            return View();
        }
	}
}