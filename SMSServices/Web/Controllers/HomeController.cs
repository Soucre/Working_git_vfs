using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using Core.Domain.Model;
using Core.Domain.Repositories;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
           
        int pageSize = 8;        

        IList<RightExec> listRightExec = null;
        [HttpGet]
        public ActionResult Index(string stock)
        {
            #region test Blance
            //IRepository<MAccDetailLog> refda = new MAccDetailLogRepository();
            //var vjkdaf = refda.GetAll();

            #endregion
            IRightExecRepository<RightExec> rpo = new RightExecRepository();
            IEnumerable<RightExec> paging = new List<RightExec>();
            ViewBag.Message = "Cập nhật quyền";
            if (stock == null)
                ViewData["stockcode"] = string.Empty;
            else
            {
                Session["Stockcode"] = stock;
                
                if(listRightExec ==null) listRightExec = rpo.getRightExecListFromStock(Session["Stockcode"].ToString());
                ViewData["count"] = listRightExec.Count;

                paging = listRightExec.Skip((1 - 1) * pageSize).Take(pageSize);//paging
                ViewData["pageNo"] = 1;
                ViewData["stockcode"] = stock; // luu lai gia tri sau khi tim
            }
            //IRepository<Post> repo = new PostRepository();
            return View(paging);
        }
        [HttpGet]
        public ActionResult Paging(string page)
        {
            if (Session["Stockcode"] == null)
            {
                return RedirectToAction("Index");
                
            }
            ViewBag.Message = "Cập nhật quyền";
            IRightExecRepository<RightExec> rpo = new RightExecRepository();
            IEnumerable<RightExec> testpaging = new List<RightExec>();

            if (listRightExec == null) listRightExec = rpo.getRightExecListFromStock(Session["Stockcode"].ToString());
                ViewData["count"] = listRightExec.Count;
                ViewData["pageNo"] = page;
                testpaging = listRightExec.Skip((Convert.ToInt32(page) - 1) * pageSize).Take(pageSize);//paging                            
            
            return View("Index",testpaging);
        }

        [HttpGet]
        public ActionResult Update(string id)
        {
            
            //
            IRepository<RightExec> repoRightExec = new RightExecRepository();
            RightExec rightExec = repoRightExec.GetById(Convert.ToInt32(id)); //lay 1 quyen vào Id
            //
            IVFS_RightExecDetailCustomerRepository<VFS_RightExecDetailCustomer> repoVFS_RightExecDetailCustomer = new VFS_RightExecDetailCustomerRepository();
            IList<VFS_RightExecDetailCustomer> listVFS_RightExecDetailCustomer = repoVFS_RightExecDetailCustomer.getListRightExecDetailCustomerFromIdRightExec(Convert.ToInt32(id));
            //
            ISecuritiesHistRepository iSecuritiesHistRepository = new SecuritiesHistRepository(); // lay so du chung khoan
             IRepository<VFS_RightExecDetailCustomer> repoDetailCustomerRightExec = new VFS_RightExecDetailCustomerRepository();
            //

            if (listVFS_RightExecDetailCustomer.Count == 0 )
            {
                IList<SecuritiesHist> sHist = iSecuritiesHistRepository.getSecuritiesHistByStockCodeAndTransactionDate(rightExec.StockCode, rightExec.DateOwnerConfirm);
                foreach (var item in sHist)
                {
                    VFS_RightExecDetailCustomer itemInsert = new VFS_RightExecDetailCustomer();

                    itemInsert.IdRightExec = rightExec.Id;
                    itemInsert.StockCode = rightExec.StockCode;
                    itemInsert.StockType = rightExec.StockType;
                    itemInsert.BoardType = rightExec.BoardType;
                    itemInsert.DateNoRight = rightExec.DateNoRight;
                    itemInsert.DateOwnerConfirm = rightExec.DateOwnerConfirm;
                    itemInsert.DatePay = rightExec.DatePay;
                    itemInsert.BeginRegisterDate = rightExec.BeginRegisterDate;
                    itemInsert.EndRegisterDate = rightExec.EndRegisterDate;
                    itemInsert.EndTransferDate = rightExec.EndTransferDate;
                    itemInsert.Description = rightExec.Description;
                    itemInsert.RateA = rightExec.RateA;
                    itemInsert.RateB = rightExec.RateB;
                    itemInsert.RightType = rightExec.RightType;
                    itemInsert.Difference = rightExec.Difference;
                    itemInsert.Posted = rightExec.Posted;
                    itemInsert.RightExecPrice = rightExec.RightExecPrice;
                    itemInsert.RoundType = rightExec.RoundType;
                    itemInsert.RoundPrice = rightExec.RoundPrice;

                    itemInsert.BranchCode = item.BranchCode;
                    itemInsert.BankGl = item.BankGl;
                    itemInsert.SectionGl = item.SectionGl;
                    itemInsert.AccountId = item.AccountId;
                    itemInsert.AccountName = item.AccountName;
                    itemInsert.StockCodeCurrent = item.StockCode;
                    itemInsert.QuantityCurrent = item.Quantity;
                    itemInsert.PendingDebitQuantity = item.PendingDebitQuantity;
                    itemInsert.TransactionDate = item.TransactionDate;

                    repoDetailCustomerRightExec.Save(itemInsert);
                }

            }
            else
            {
                foreach (var item in listVFS_RightExecDetailCustomer)
                {               

                    item.DatePay = rightExec.DatePay;                    

                    repoDetailCustomerRightExec.Update(item);
                }
            }


            ViewBag.Message = "Cập nhật quyền";
          
            return RedirectToAction("Index");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }
        [HttpPost]
        public ActionResult Search(FormCollection formcollection)
        {
            
            ViewBag.Message = "Search";
            string data = formcollection.Get("typeStockCode");
            //ViewData[""] = 

            return View();
        }
       
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Sync()
        {
            ViewBag.Message = "Your contact page.";
            IRepository<SecuritiesHist> repoSecuritieshist = new SecuritiesHistRepository();
            IRepository<RightExec> repoExec = new RightExecRepository();
            ISecuritiesHistRepository iSecuritiesHistRepository = new SecuritiesHistRepository();
            
            //IList<SecuritiesHist> listSecuritiesHist = repoSecuritieshist.GetAll();

            IList<RightExec> listRightExec = repoExec.GetAll();
            IRepository<VFS_RightExecDetailCustomer> repoDetailCustomerRightExec = new VFS_RightExecDetailCustomerRepository();
            foreach (var item in listRightExec)
            {
                IList<SecuritiesHist> sHist = iSecuritiesHistRepository.getSecuritiesHistByStockCodeAndTransactionDate(item.StockCode, item.DateOwnerConfirm);
                if (sHist.Count > 0)
                {
                    foreach (var itemHist in sHist)
                    {
                        VFS_RightExecDetailCustomer itemInsert = new VFS_RightExecDetailCustomer();
                        itemInsert.IdRightExec = item.Id;
                        itemInsert.StockCode = item.StockCode;
                        itemInsert.StockType = item.StockType;
                        itemInsert.BoardType = item.BoardType;
                        itemInsert.DateNoRight = item.DateNoRight;
                        itemInsert.DateOwnerConfirm = item.DateOwnerConfirm;
                        itemInsert.DatePay = item.DatePay;
                        itemInsert.BeginRegisterDate = item.BeginRegisterDate;
                        itemInsert.EndRegisterDate = item.EndRegisterDate;
                        itemInsert.EndTransferDate = item.EndTransferDate;
                        itemInsert.Description = item.Description;
                        itemInsert.RateA = item.RateA;
                        itemInsert.RateB = item.RateB;
                        itemInsert.RightType = item.RightType;
                        itemInsert.Difference = item.Difference;
                        itemInsert.Posted = item.Posted;
                        itemInsert.RightExecPrice = item.RightExecPrice;
                        itemInsert.RoundType = item.RoundType;
                        itemInsert.RoundPrice = item.RoundPrice;

                        itemInsert.BranchCode = itemHist.BranchCode;
                        itemInsert.BankGl = itemHist.BankGl;
                        itemInsert.SectionGl = itemHist.SectionGl;
                        itemInsert.AccountId = itemHist.AccountId;
                        itemInsert.AccountName = itemHist.AccountName;
                        itemInsert.StockCodeCurrent = itemHist.StockCode;
                        itemInsert.QuantityCurrent = itemHist.Quantity;
                        itemInsert.PendingDebitQuantity = itemHist.PendingDebitQuantity;
                        itemInsert.TransactionDate = itemHist.TransactionDate;

                        repoDetailCustomerRightExec.Save(itemInsert);
                    }
                    
                }
            }         

            return View();
        }



    }
}
