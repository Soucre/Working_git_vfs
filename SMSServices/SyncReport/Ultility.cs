using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Core;
using Core.Domain.Model;
using Core.Domain.Repositories;

namespace SMS
{
    public static class Ultility
    {
        public static void LogFile(string sExceptionName, string directory)
        {

            StreamWriter log;

            if (!File.Exists(directory))
            {

                log = new StreamWriter(directory);

            }

            else
            {

                log = File.AppendText(directory);

            }

            // Write to the file:

            log.WriteLine("Data Time:" + DateTime.Now + "-------" + sExceptionName);



            // Close the stream:

            log.Close();

        }
        public static void syncBalanceNoKyQuy()
        {
            #region Lay danh sach khach hang
            IMAccDetailLogRepository<MAccDetailLog> repo = new MAccDetailLogRepository();
            IList<String> listCustomer = repo.getListAllCutomer();

            #endregion

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
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void syncQuyen()
        {
            IRepository<SecuritiesHist> repoSecuritieshist = new SecuritiesHistRepository();
            IRepository<RightExec> repoExec = new RightExecRepository();
            ISecuritiesHistRepository iSecuritiesHistRepository = new SecuritiesHistRepository();
            iSecuritiesHistRepository.truncateTable();
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

        }
    }
}
