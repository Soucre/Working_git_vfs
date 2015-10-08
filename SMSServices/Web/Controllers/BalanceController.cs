using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Domain.Model;
using Core;
using Core.Domain.Repositories;
using System.IO;

namespace Web.Controllers
{
    public class BalanceController : Controller
    {
        //
        // GET: /Balance/

        public ActionResult Index()
        {
            @ViewBag.Message = "cập nhật số dư nợ";
            

            return View();
        }

        public ActionResult First()
        {
            @ViewBag.Message = "Blance";

            IMAccDetailLogRepository<MAccDetailLog> repo = new MAccDetailLogRepository();


            IList<String> listCustomer = repo.getListAllCutomer();
            //
            IRepository<VFS_MAccDetailLogBlance> VFS_MAccDetailLogBlance = new VFS_MAccDetailLogBlanceRepository();

            //

            repo.truncateTable(); // xoa tat cac cac hop dong
           
            try
            {
                foreach (var itemCustomer in listCustomer)
                {
                    decimal blance = 0;
                    IList<MAccDetailLog> listMAccDetailLog = repo.getListFromCustomer(itemCustomer);
                    foreach (var item in listMAccDetailLog)
                    {
                        if (item.Status == "B")
                        {
                            blance = blance + item.AmountCalInterest;
                        }
                        else
                        {
                            blance = blance - item.AmountCalInterest;
                        }
                        VFS_MAccDetailLogBlance itemInsert = new VFS_MAccDetailLogBlance();
                        itemInsert.LogId = item.LogId;
                        itemInsert.Balance = blance;

                        VFS_MAccDetailLogBlance.Save(itemInsert);
                    }
                }
                @ViewBag.Status = "Sucessfull";
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }



        //public DateTime readfiletext()
        //{
        //    TextReader tw = new StreamReader(Server.MapPath(@"." + "/Upload/" +  "date.txt"));
        //    DateTime returnString = Convert.ToDateTime(tw.ReadLine());

        //    tw.Close();
        //    return returnString;
        //}
        //public string writefiletext(DateTime date)
        //{
        //    TextWriter tw = new StreamWriter(Server.MapPath(@"." + "/Upload/" + "date.txt"));
        //    tw.WriteLine(date.ToShortDateString());

        //    tw.Close();
        //    return null;
        //}


    }
}
