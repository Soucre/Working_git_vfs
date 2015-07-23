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
    [Authorize]
    public class HomeController : Controller
    {
        private StoxDataEntities Stoxdb;      

        
        public async Task<ActionResult> Index(int row = 20)
        {
            using (Stoxdb = new StoxDataEntities())
            {
                if (row > 60)
                {
                    row = 60;
                }
                var yearReportParameter = new SqlParameter("@YearReport", 2014);
                var rowtotal = new SqlParameter("@TotalRow", row);
                var result = await Stoxdb.Database
                            .SqlQuery<DIV>("VFS_DIV @YearReport,@TotalRow", yearReportParameter, rowtotal).ToListAsync();
                            
                //int pageSize = 60;
                //int pageNumber = 1;// (page ?? 1);
                return View(result);
            }
            
        }
        

        private async Task LoadInit()
        {
            //var listStock = (from s in dbstox.stox_tb_Company.ToList()
            //                 orderby s.Ticker
            //                 where s.ExchangeID == 0
            //                 select new
            //                 {
            //                     Ticker = s.Ticker
            //                 }).ToList();

            //ViewBag.listStock = new SelectList(listStock, "Ticker", "Ticker");


            //ViewBag.listUserId = new SelectList(db.UserLogins, "Id", "UserNameCopy");

            //var listTypeRecomendation = new List<dynamic>
            //        { 
            //            new { Id = "MUA", Name = "MUA" },
            //            new { Id = "BAN", Name = "BÁN" } 
            //        }.ToList();

            //ViewBag.listTypeRecomendation = new SelectList(listTypeRecomendation, "Id", "Name");

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}