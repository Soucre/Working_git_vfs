using PhimHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace PhimHang.Controllers
{

    public class UploadController : Controller
    {
        //
        // GET: /Upload/
        private VfsCustomerServiceEntities db = new VfsCustomerServiceEntities();
        public async Task<ActionResult> Index(string pdfName)
        {
            using (db = new VfsCustomerServiceEntities())
            {
                Report rp = await db.Reports.FirstOrDefaultAsync(r => r.UploadDir == pdfName);
                CustomerLog cl = await db.CustomerLogs.FirstOrDefaultAsync(log => log.CustomerId == User.Identity.Name);
                if (rp!=null && cl!=null)
                {
                    rp.TotalDownload += 1;
                    cl.Total_Download += 1;
                    try // save databse
                    {
                        db.Entry(rp).State = EntityState.Modified;
                        db.Entry(cl).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
            return Redirect("~/upload/" + pdfName + ".pdf");
        }

        //
        // GET: /Upload/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Upload/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Upload/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Upload/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Upload/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Upload/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Upload/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
