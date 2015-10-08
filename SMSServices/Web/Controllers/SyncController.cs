using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Domain.Model;
using Core;
using Core.Domain.Repositories;

namespace Web.Controllers
{
    public class SyncController : Controller
    {
        //
        // GET: /Sync/

        public ActionResult Index()
        {
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
                        //if (item.RightType == "S")
                        //{
                        //    itemInsert.RightType = "K";
                        //}
                        //else
                        //{//K = stock
                            itemInsert.RightType = item.RightType;
                        //}
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

            return RedirectToAction("Index");
        }




    }
}
