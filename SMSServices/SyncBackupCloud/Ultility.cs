using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Core;
using Core.Domain.Model;
using Core.Domain.Repositories;
using Excel = Microsoft.Office.Interop.Excel;
using SyncReport.App_Code;
using System.Diagnostics;

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
    

        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                //MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        static void KillProcess(String nameProcess)
        {
            foreach (Process proc in Process.GetProcessesByName(nameProcess))
            {
                proc.Kill();
            }
        }

        static void checkExistFile(string directory)
        {
            if (File.Exists(directory))
            {
                File.Delete(directory);
            }
            
        }

        public static void syncBalanceHistCluod() // so du tien
        {
            KillProcess("EXCEL");


            IBalanceHistRepository<BalanceHist> iBalanceHist = new BalanceHistRepository();
            IRepository<BalanceHist> testne = new BalanceHistRepository();
            //IList<SecuritiesHist> listSecuritiesHist = repoSecuritieshist.GetAll();

            string datecurrent = DateTime.Now.ToString("yyyy-MM-dd"); ;


            IList<BalanceHist> sHist = iBalanceHist.listBlanceWithTransactionDate(datecurrent);

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            string movefile = ApplicationHelper.movefile;
            xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //xlWorkSheet.get_Range("I6", Type.Missing).Value2 = StockCodeTxt.Text.ToUpper();

            // title
            xlWorkSheet.Rows[1].Cells[1] = "AccountId";
            xlWorkSheet.Rows[1].Cells[2] = "BankGl";
            xlWorkSheet.Rows[1].Cells[3] = "SectionGl";
            xlWorkSheet.Rows[1].Cells[4] = "CurrentBalance";
            xlWorkSheet.Rows[1].Cells[5] = "TransactionDate";
            //end title
            int numberrow = 2;
            foreach (var item in sHist)
            {
                xlWorkSheet.Rows[numberrow].Cells[1] = item.AccountId;
                xlWorkSheet.Rows[numberrow].Cells[2] = item.BankGl;
                xlWorkSheet.Rows[numberrow].Cells[3] = item.SectionGl;
                xlWorkSheet.Rows[numberrow].Cells[4] = item.CurrentBalance;
                xlWorkSheet.Rows[numberrow].Cells[5] = item.TransactionDate;

                numberrow += 1;
            }
            //MessageBox.Show(xlWorkSheet.get_Range("I6").Value2.ToString());
            string save = ApplicationHelper.movefile + DateTime.Now.ToString("yyyyMMdd");
            string fullDirectory = save + "_SoDuTien" + ".xls"; 
            checkExistFile(fullDirectory); // check exist directory
            xlWorkBook.SaveAs(fullDirectory, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(false, Type.Missing, Type.Missing);
            xlApp.Quit();


            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            // kill all process of MS Excel
        

        }
        public static void syncBalanceStockCluod() // so du Chung khoan
        {
            // kill all process of MS Excel
            KillProcess("EXCEL");

            ISecuritiesHistRepository iSecuritiesHistRepository = new SecuritiesHistRepository();

            //IList<SecuritiesHist> listSecuritiesHist = repoSecuritieshist.GetAll();

            DateTime datecurrent = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);


            IList<SecuritiesHist> sHist = iSecuritiesHistRepository.getSecuritiesHistByStockCodeAndTransactionDate();

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            string movefile = ApplicationHelper.movefile;
            xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //xlWorkSheet.get_Range("I6", Type.Missing).Value2 = StockCodeTxt.Text.ToUpper();
            // title
            xlWorkSheet.Rows[1].Cells[1] = "AccountId";
            xlWorkSheet.Rows[1].Cells[3] = "SectionGl";
            xlWorkSheet.Rows[1].Cells[4] = "BankGl";
            xlWorkSheet.Rows[1].Cells[5] = "AccountName";
            xlWorkSheet.Rows[1].Cells[6] = "StockCode";
            xlWorkSheet.Rows[1].Cells[7] = "Quantity";
            xlWorkSheet.Rows[1].Cells[8] = "TransactionDate";
            //end title

            int numberrow = 2;
            foreach (var item in sHist)
            {
                xlWorkSheet.Rows[numberrow].Cells[1] = item.AccountId;
                xlWorkSheet.Rows[numberrow].Cells[3] = item.SectionGl;
                xlWorkSheet.Rows[numberrow].Cells[4] = item.BankGl;                
                xlWorkSheet.Rows[numberrow].Cells[5] = item.AccountName;
                xlWorkSheet.Rows[numberrow].Cells[6] = item.StockCode;
                xlWorkSheet.Rows[numberrow].Cells[7] = item.Quantity;                
                xlWorkSheet.Rows[numberrow].Cells[8] = item.TransactionDate;

                numberrow += 1;
            }

          

            //MessageBox.Show(xlWorkSheet.get_Range("I6").Value2.ToString());
            string save = ApplicationHelper.movefile + DateTime.Now.ToString("yyyyMMdd");
            string fullDirectory = save + "_SoDuChungKhoan" + ".xls";
            checkExistFile(fullDirectory); // check exist directory

            xlWorkBook.SaveAs(fullDirectory, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(false, Type.Missing, Type.Missing);
            xlApp.Quit();


            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);


        }

        public static void syncBalanceMarginCluod() // so du no
        {
            // kill all process of MS Excel
            KillProcess("EXCEL");

            BalanceMarginRepository iSecuritiesHistRepository = new BalanceMarginRepository();

            //IList<SecuritiesHist> listSecuritiesHist = repoSecuritieshist.GetAll();

            String datecurrent = DateTime.Now.ToString("yyyyMMdd");


            IList<BalanceMargin> sHist = iSecuritiesHistRepository.getBalancelist();

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            string movefile = ApplicationHelper.movefile;
            xlWorkBook = xlApp.Workbooks.Add(Type.Missing);

            #region no ky quy
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //xlWorkSheet.get_Range("I6", Type.Missing).Value2 = StockCodeTxt.Text.ToUpper();
            // title of excel
            xlWorkSheet.Rows[1].Cells[1] = "AccountId";
            xlWorkSheet.Rows[1].Cells[2] = "ContractId";
            xlWorkSheet.Rows[1].Cells[3] = "EffectiveOnDate";
            xlWorkSheet.Rows[1].Cells[4] = "Amount";
            xlWorkSheet.Rows[1].Cells[5] = "AmountPaid";
            xlWorkSheet.Rows[1].Cells[6] = "Balance";

            // end title
            int numberrow = 2;
            foreach (var item in sHist)
            {
                if (item.Status != "D" && (item.Amount - item.AmountPaid) != 0)
                {
                    xlWorkSheet.Rows[numberrow].Cells[1] = item.AccountId;
                    xlWorkSheet.Rows[numberrow].Cells[2] = item.ContractId;
                    xlWorkSheet.Rows[numberrow].Cells[3] = item.EffectiveOnDate;
                    xlWorkSheet.Rows[numberrow].Cells[4] = item.Amount;
                    xlWorkSheet.Rows[numberrow].Cells[5] = item.AmountPaid;
                    xlWorkSheet.Rows[numberrow].Cells[6] = item.Amount - item.AmountPaid;
                    numberrow += 1;
                }
            }
            #endregion

            #region mua quyen sheet 2
            Excel.Worksheet xlWorkSheet2;
            xlWorkSheet2 = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);

            BuyCashContractRepository BuyCashContractRepository = new BuyCashContractRepository();
            IList<BuyCashContract> ListBuyCashContract = new List<BuyCashContract>();
            ListBuyCashContract = BuyCashContractRepository.getBuyCashContractlist();

            // header
            xlWorkSheet2.Rows[1].Cells[1] = "AccountId";
            xlWorkSheet2.Rows[1].Cells[2] = "ContractId";
            xlWorkSheet2.Rows[1].Cells[3] = "DateContract";
            xlWorkSheet2.Rows[1].Cells[4] = "PaymentDate";
            xlWorkSheet2.Rows[1].Cells[5] = "AdvanceAmount";
            xlWorkSheet2.Rows[1].Cells[6] = "AdvanceFee";
            xlWorkSheet2.Rows[1].Cells[7] = "Status";    
            numberrow = 2;
            foreach (var item in ListBuyCashContract)
            {
                if (item.Status == "T")
                {
                    xlWorkSheet2.Rows[numberrow].Cells[1] = item.AccountId;
                    xlWorkSheet2.Rows[numberrow].Cells[2] = item.ContractId;
                    xlWorkSheet2.Rows[numberrow].Cells[3] = item.DateContract;
                    xlWorkSheet2.Rows[numberrow].Cells[4] = item.PaymentDate;
                    xlWorkSheet2.Rows[numberrow].Cells[5] = item.AdvanceAmount;
                    xlWorkSheet2.Rows[numberrow].Cells[6] = item.AdvanceFee;
                    xlWorkSheet2.Rows[numberrow].Cells[7] = item.Status;
                    numberrow += 1;
                }
            }
            #endregion ung truoc Sheet 3
          
            #region ung truoc sheet3
            Excel.Worksheet xlWorkSheet3;
            xlWorkSheet3 = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(3);

            AdvanceContractAllRepository AdvanceContractAllRepository = new AdvanceContractAllRepository();
            IList<AdvanceContractAll> AdvanceContractAllList = new List<AdvanceContractAll>();
            AdvanceContractAllList = AdvanceContractAllRepository.getAdvanceContractlist();

            xlWorkSheet3.Rows[1].Cells[1] = "AccountId";
            xlWorkSheet3.Rows[1].Cells[2] = "ContractId";
            xlWorkSheet3.Rows[1].Cells[3] = "DateContract";
            xlWorkSheet3.Rows[1].Cells[4] = "PaymentDate";
            xlWorkSheet3.Rows[1].Cells[5] = "AdvanceAmount";
            xlWorkSheet3.Rows[1].Cells[6] = "Status";
            numberrow = 2;
            foreach (var item in AdvanceContractAllList)
            {
                if (item.Status == "T")
                {
                    xlWorkSheet3.Rows[numberrow].Cells[1] = item.AccountId;
                    xlWorkSheet3.Rows[numberrow].Cells[2] = item.ContractId;
                    xlWorkSheet3.Rows[numberrow].Cells[3] = item.DateContract;
                    xlWorkSheet3.Rows[numberrow].Cells[4] = item.PaymentDate;
                    xlWorkSheet3.Rows[numberrow].Cells[5] = item.AdvanceAmount;
                    xlWorkSheet3.Rows[numberrow].Cells[6] = item.Status;
                    numberrow += 1;
                }
            }

            #endregion


            //MessageBox.Show(xlWorkSheet.get_Range("I6").Value2.ToString());

            #region luu vao file excel
            string save = ApplicationHelper.movefile + DateTime.Now.ToString("yyyyMMdd");
            string fullDirectory = save + "_SoDuNo" + ".xls";
            checkExistFile(fullDirectory); // check exist directory

            xlWorkBook.SaveAs(fullDirectory, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(false, Type.Missing, Type.Missing);
            xlApp.Quit();


            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
            #endregion


        }

        public static void syncTradingResultHistCluod() // Giao dich 3 ngay gan nhat
        {
            // kill all process of MS Excel
            KillProcess("EXCEL");

            TradingResultHistRepository iSecuritiesHistRepository = new TradingResultHistRepository();

            //IList<SecuritiesHist> listSecuritiesHist = repoSecuritieshist.GetAll();

            String datecurrent = DateTime.Now.ToString("yyyyMMdd");


            IList<TradingResultHist> sHist = iSecuritiesHistRepository.getTradingResultHistlist();

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            string movefile = ApplicationHelper.movefile;
            xlWorkBook = xlApp.Workbooks.Add(Type.Missing);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //xlWorkSheet.get_Range("I6", Type.Missing).Value2 = StockCodeTxt.Text.ToUpper();

            // title 
            xlWorkSheet.Rows[1].Cells[1] = "AccountId";
            xlWorkSheet.Rows[1].Cells[2] = "BoardType";
            xlWorkSheet.Rows[1].Cells[3] = "BranchCode";
            xlWorkSheet.Rows[1].Cells[4] = "FeeRate";
            xlWorkSheet.Rows[1].Cells[5] = "MatchedPrice";
            xlWorkSheet.Rows[1].Cells[6] = "MatchedValue";
            xlWorkSheet.Rows[1].Cells[7] = "MatchedVolume";
            xlWorkSheet.Rows[1].Cells[8] = "OrderSide";
            xlWorkSheet.Rows[1].Cells[9] = "StockCode";
            xlWorkSheet.Rows[1].Cells[10] = "TransactionDate";
            //end title 
            int numberrow = 2;
            foreach (var item in sHist)
            {
                xlWorkSheet.Rows[numberrow].Cells[1] = item.AccountId;
                xlWorkSheet.Rows[numberrow].Cells[2] = item.BoardType;
                xlWorkSheet.Rows[numberrow].Cells[3] = item.BranchCode;
                xlWorkSheet.Rows[numberrow].Cells[4] = item.FeeRate;
                xlWorkSheet.Rows[numberrow].Cells[5] = item.MatchedPrice;
                xlWorkSheet.Rows[numberrow].Cells[6] = item.MatchedValue;
                xlWorkSheet.Rows[numberrow].Cells[7] = item.MatchedVolume;
                xlWorkSheet.Rows[numberrow].Cells[8] = item.OrderSide;
                xlWorkSheet.Rows[numberrow].Cells[9] = item.StockCode;
                xlWorkSheet.Rows[numberrow].Cells[10] = item.TransactionDate;
                numberrow += 1;
            }
            //MessageBox.Show(xlWorkSheet.get_Range("I6").Value2.ToString());
            string save = ApplicationHelper.movefile + DateTime.Now.ToString("yyyyMMdd");
            string fullDirectory = save + "_GIADICH3Ngay" + ".xls";
            checkExistFile(fullDirectory); // check exist directory

            xlWorkBook.SaveAs(fullDirectory, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(false, Type.Missing, Type.Missing);
            xlApp.Quit();


            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);


        }

    }

        
    
}
