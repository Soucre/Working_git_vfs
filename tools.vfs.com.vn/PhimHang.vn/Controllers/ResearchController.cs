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
    //[Authorize]
    public class ResearchController : Controller
    {
        //
        // GET: /Research/
        private VfsCustomerServiceEntities DbCustomerSV = new VfsCustomerServiceEntities();
        public async Task<ActionResult> Index(string ticker)
        {
            using (DbCustomerSV = new VfsCustomerServiceEntities())
            {
                ViewBag.DescriptionTile = Resources.VN_Resources.Research_Title;
                ViewBag.Ticker = ticker;
                string tickerFilter = string.IsNullOrEmpty(ticker) ? "ALL" : ticker;
                LoadParameter();

                var listReport = await (from r in DbCustomerSV.Reports.Include(r => r.ReportType)
                                        where (r.Ticker == tickerFilter || "ALL" == tickerFilter)
                                        orderby r.CreateDate descending
                                        select r).Take(5).ToListAsync();
                return View(listReport);
            }
            
        }

        public async Task<ActionResult> LoadMoreReport(int skipPostion, string urlCurrent)
        {
            string splitURL = urlCurrent.Substring(urlCurrent.IndexOf('?')).Split('#')[0];
            
                //.QueryString["Ticker"];
            using (DbCustomerSV = new VfsCustomerServiceEntities())
            {
                var listReport = await (from r in DbCustomerSV.Reports.Include(r => r.ReportType)
                                        orderby r.CreateDate descending
                                        select r).Skip(skipPostion).Take(5).ToListAsync();
                return PartialView("_PartialListReport", listReport);
            }
        }

        private void LoadParameter()
        {
            var listTypeReport = new List<ListDropbox>
                    { 
                        new ListDropbox { Id = 1, Description = "Báo cáo công ty" },
                        new ListDropbox { Id = 2, Description = "Nhận định thị trường" },
                        new ListDropbox{ Id = 3, Description = "Báo cáo chiến lược" },
                        new ListDropbox{ Id = 4, Description = "BC phân tích doanh nghiệp" },
                        new ListDropbox{ Id = 5, Description = "Chủ đề nóng" }
                        
                    }.ToList();

            ViewBag.listTypeReport = listTypeReport; // load loai bao cao trong menu
        }



	}
}