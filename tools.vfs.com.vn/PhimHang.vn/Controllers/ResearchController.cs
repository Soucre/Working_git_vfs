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
    public class ResearchController : Controller
    {
        //
        // GET: /Research/
        private VfsCustomerServiceEntities DbCustomerSV = new VfsCustomerServiceEntities();
        public async Task<ActionResult> Index(string ticker, int[] CategoryIDs)
        {
            using (DbCustomerSV = new VfsCustomerServiceEntities())
            {
                ViewBag.DescriptionTile = Resources.VN_Resources.Research_Title;                
                ViewBag.Ticker = ticker;
                string tickerFilter = string.IsNullOrEmpty(ticker) ? "ALL" : ticker;
                DateTime dateVIPFilter = DateTime.Now;
                if (Request.Cookies["AccountType"] != null)
                {
                    dateVIPFilter = dateVIPFilter.AddDays(2);
                }
                LoadParameter(CategoryIDs);
                var listReport = new List<Report>();
                if (CategoryIDs == null)
                {
                    listReport = await (from r in DbCustomerSV.Reports.Include(r => r.ReportType)
                                        where (r.Ticker == tickerFilter || "ALL" == tickerFilter)
                                        && (r.DateViewCustomer <= dateVIPFilter)
                                        orderby r.CreateDate descending
                                        select r).Take(5).ToListAsync();
                }
                else
                {
                    listReport = await (from r in DbCustomerSV.Reports.Include(r => r.ReportType)
                                        where (r.Ticker == tickerFilter || "ALL" == tickerFilter)
                                        && (CategoryIDs.Contains(r.ReportType.Id))
                                        && (r.DateViewCustomer <= dateVIPFilter)
                                        orderby r.CreateDate descending
                                        select r).Take(5).ToListAsync();
                }

                return View(listReport);
            }

        }



        private void LoadParameter(int[] CategoryIDs)
        {
           
            var listTypeReport = new List<CheckBoxes>
                    { 
                        new CheckBoxes { Id = 1, Description = "Cảm nhận thị trường" },
                        new CheckBoxes { Id = 2, Description = "Phân tích cổ phiếu" },
                        new CheckBoxes{ Id = 3, Description = "Phân tích kỹ thuật" },
                        new CheckBoxes{ Id = 4, Description = "Báo cáo chiến lược" },
                        new CheckBoxes{ Id = 5, Description = "Phím hàng" },
                        new CheckBoxes{ Id = 6, Description = "Snapshot" }                        
                    }.ToList();
            if (CategoryIDs!=null)
            {
                foreach (var item in listTypeReport)
                {
                    if (CategoryIDs.Any(ct => ct == item.Id))
                    {
                        item.Checked = true;
                    }
                }
            }
            
            ViewBag.listTypeReport = listTypeReport; // load loai bao cao trong menu
        }

        public async Task<ActionResult> LoadMoreReport(int skipPostion, string ticker, int[] CategoryIDs)
        {
            //System.Threading.Thread.Sleep(6000);
            string tickerFilter = string.IsNullOrEmpty(ticker) ? "ALL" : ticker;
            DateTime dateVIPFilter = DateTime.Now; 
            if (Request.Cookies["AccountType"] != null)
            {
                dateVIPFilter = dateVIPFilter.AddDays(2); // neu la VIP dc xem tat ca cac report
            }
            using (DbCustomerSV = new VfsCustomerServiceEntities())
            {
                var listReport = new List<Report>();
                if (CategoryIDs == null)
                {
                    listReport = await (from r in DbCustomerSV.Reports.Include(r => r.ReportType)
                                        where (r.Ticker == tickerFilter || "ALL" == tickerFilter)
                                        && (r.DateViewCustomer <= dateVIPFilter)
                                        orderby r.CreateDate descending
                                        select r).Skip(skipPostion).Take(5).ToListAsync();

                }
                else
                {
                    listReport = await (from r in DbCustomerSV.Reports.Include(r => r.ReportType)
                                        where (r.Ticker == tickerFilter || "ALL" == tickerFilter)
                                        && (r.DateViewCustomer <= dateVIPFilter)
                                        && (CategoryIDs.Contains(r.ReportType.Id))
                                        orderby r.CreateDate descending
                                        select r).Skip(skipPostion).Take(5).ToListAsync();


                }
              
                return PartialView("_PartialListReport", listReport);
            }
        }

	}
}