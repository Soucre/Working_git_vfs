using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Core;
using Core.Domain.Model;
using Core.Domain.Repositories;
using System.Data.SqlClient;
using System.Data;

namespace SMS
{
    public static class Ultility
    {
        static public string LocalHost
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["LocalHost"].ToString();

            }
        }
        public static bool CheckConnectionSQL()
        {
            try
            {
                using (SqlConnection myConn = new SqlConnection(LocalHost))
                {
                    SqlCommand myCmd = new SqlCommand("SELECT COUNT(*) FROM [master].[dbo].[spt_values]", myConn);
                    if (myConn.State != ConnectionState.Open)
                        myConn.Open();
                    myCmd.ExecuteNonQuery();
                    return (myConn.State == ConnectionState.Open);

                }
            }
            catch
            {
                return false;
            }

        }
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
        public static bool checkHuyNiemYeu(string stockCode,IList<string> listHuyNiemYet)
        {
            bool result = false;
            if (listHuyNiemYet.SingleOrDefault(m => m.Contains(stockCode)) != null)
            {
                result = true;
            }
            return result;
        }
        public static void LaiLo()
        {
            IRepository<VFS_Report_LaiLo_Customer> rp = new VFS_Report_LaiLo_CustomerRepository();
            IVFS_Report_LaiLo_CustomerRepository rpI = new VFS_Report_LaiLo_CustomerRepository();
            CustomerLaiLoListRepository ctrp = new CustomerLaiLoListRepository();
            IList<CustomerLaiLoList> listCustomer = ctrp.getList();
            IList<string> listHuyNiemYet = rpI.getListStockHuyNiemYet();

            //foreach (var customer in listCustomer)
            //{


                //IList<TransactionCreditDebit> transactionCreditDebit = rpI.getTransactionCreditDebit(customer.CustomerId, "2007-01-01", "2014-04-20");
                IList<TransactionCreditDebit> transactionCreditDebit = rpI.getTransactionCreditDebit("094C000015", "2007-01-01", "2014-04-20");
                foreach (var item in transactionCreditDebit)
                {

                    VFS_Report_LaiLo_Customer vFS_Report_LaiLo_Customer = new VFS_Report_LaiLo_Customer();
                    vFS_Report_LaiLo_Customer.TransactionDate = item.TransactionDate.ToString("yyyyMMdd");
                    vFS_Report_LaiLo_Customer.ActiveDate = item.ActiveDate.ToString("yyyyMMdd");
                    vFS_Report_LaiLo_Customer.CreditOrDebit = item.CreditOrDebit;
                    vFS_Report_LaiLo_Customer.CustomerId = item.CustomerId;
                    vFS_Report_LaiLo_Customer.StockCode = item.StockCode;
                    vFS_Report_LaiLo_Customer.Volume = item.Volume;
                    vFS_Report_LaiLo_Customer.TransactionType = item.TransactionType;
                    vFS_Report_LaiLo_Customer.FeeRate = item.FeeRate; // % phí
                    vFS_Report_LaiLo_Customer.TaxRate = item.TaxRate; // % thuế
                    

                    if (item.ActiveDate < DateTime.Now.Date || item.ActiveDate == null) // nhỏ hơn ngày hiện tại thì nó đã chuyển
                    {                                                                       // vào chứng khoán giao dịch
                        vFS_Report_LaiLo_Customer.CKGiaoDich = item.Volume;
                        vFS_Report_LaiLo_Customer.CKChoGiaoDich = 0;
                    }
                    else
                    {
                        vFS_Report_LaiLo_Customer.CKChoGiaoDich = item.Volume;
                        vFS_Report_LaiLo_Customer.CKGiaoDich = 0;
                        vFS_Report_LaiLo_Customer.RunAgain = "R"; // lớn hơn ngày hiện tại thì phải chạy cập nhật lại CK
                    }

                    if (item.CreditOrDebit == "D") // gia ban
                    {
                        vFS_Report_LaiLo_Customer.GiaBan = item.Price;
                    }
                    else
                    {
                        vFS_Report_LaiLo_Customer.GiaBan = 0;
                    }

                    VFS_Report_LaiLo_Customer LastvFS_Report_LaiLo_Customer = rpI.getLastVolumeAvg(item.CustomerId, item.StockCode, "L"); // khối lượng tồn

                    if (item.CreditOrDebit == "D") // giá thực hiện: nếu mua thì giữ nguyên giá, còn bán thì lấy giá trung bình gần nhất
                    {
                        if (LastvFS_Report_LaiLo_Customer == null)
                        {
                            vFS_Report_LaiLo_Customer.GiaThucHien = item.Price;
                        }
                        else
                        {
                            vFS_Report_LaiLo_Customer.GiaThucHien = LastvFS_Report_LaiLo_Customer.GiaTrungBinh;
                        }
                    }
                    else
                    {
                        vFS_Report_LaiLo_Customer.GiaThucHien = item.Price;
                    }

                    if (item.TransactionType == "QS") // nếu cổ tức bằng cổ phiếu thì giá thực hiện = 0
                    {
                        vFS_Report_LaiLo_Customer.GiaThucHien = 0;
                    }
                    if (item.TransactionType == "QM") // nếu cổ tức bằng cổ phiếu thì giá thực hiện 10 ngàn
                    {
                        vFS_Report_LaiLo_Customer.GiaThucHien = -10; // đơn vị là 1000
                    }

                    vFS_Report_LaiLo_Customer.KhoiLuongNhapKho = item.Volume; // khoi luong nhat kho

                    if (item.TransactionType == "QM") // nếu cổ tức bằng tiền thì khối lượng nhập kho = 0
                    {
                        vFS_Report_LaiLo_Customer.KhoiLuongNhapKho = 0;

                    }

                    decimal giatrimuaban = vFS_Report_LaiLo_Customer.KhoiLuongNhapKho * vFS_Report_LaiLo_Customer.GiaThucHien;
                    decimal phimuaban = giatrimuaban * vFS_Report_LaiLo_Customer.FeeRate;
                    decimal thuemuaban = giatrimuaban * vFS_Report_LaiLo_Customer.TaxRate;
                    vFS_Report_LaiLo_Customer.GiaTriNhapKho = giatrimuaban + phimuaban + thuemuaban; // gia tri nhap kho
                    // giao dịch mua = gia tri mua + phí
                    // giao dịch bán = gia tri bán - phí - thuế

                    if (item.TransactionType == "QM") // cổ tức bằng tiền + thuế
                    {
                        decimal giatriQuyenCoTucBangTien = vFS_Report_LaiLo_Customer.Volume * vFS_Report_LaiLo_Customer.GiaThucHien;
                        decimal thueQuyenCoTucBangTien = giatriQuyenCoTucBangTien * vFS_Report_LaiLo_Customer.TaxRate;
                        vFS_Report_LaiLo_Customer.GiaTriNhapKho = giatriQuyenCoTucBangTien - thueQuyenCoTucBangTien;
                    }

                   
                   
                    if (LastvFS_Report_LaiLo_Customer != null)
                    {
                        vFS_Report_LaiLo_Customer.KhoiLuongTonKho = LastvFS_Report_LaiLo_Customer.KhoiLuongTonKho + vFS_Report_LaiLo_Customer.KhoiLuongNhapKho;
                        vFS_Report_LaiLo_Customer.GiaTriTonKho = LastvFS_Report_LaiLo_Customer.GiaTriTonKho + vFS_Report_LaiLo_Customer.GiaTriNhapKho;

                    }
                    else
                    {
                        vFS_Report_LaiLo_Customer.KhoiLuongTonKho = vFS_Report_LaiLo_Customer.KhoiLuongNhapKho;
                        vFS_Report_LaiLo_Customer.GiaTriTonKho = vFS_Report_LaiLo_Customer.GiaTriNhapKho;
                    }

                    if (vFS_Report_LaiLo_Customer.KhoiLuongTonKho == 0 || vFS_Report_LaiLo_Customer.GiaTriTonKho == 0) // bán hết
                    {
                        vFS_Report_LaiLo_Customer.GiaTrungBinh = vFS_Report_LaiLo_Customer.GiaThucHien;
                        vFS_Report_LaiLo_Customer.GiaTriTonKho = 0; //Nếu giá trị tồn kho bằng không, thì cân băng lại giá trị tồn = 0
                    }
                    else
                    {
                        vFS_Report_LaiLo_Customer.GiaTrungBinh = vFS_Report_LaiLo_Customer.GiaTriTonKho / vFS_Report_LaiLo_Customer.KhoiLuongTonKho;
                    }

                    vFS_Report_LaiLo_Customer.LastVolumeBlance = "L";

                    // giá trị mua bán và thuế
                    vFS_Report_LaiLo_Customer.FeeRateValue = item.MatchedValue * vFS_Report_LaiLo_Customer.FeeRate;
                    vFS_Report_LaiLo_Customer.TaxRateValue = item.MatchedValue * vFS_Report_LaiLo_Customer.TaxRate;
                    // end giá trị mua bán và thuế
                    if (checkHuyNiemYeu(item.StockCode, listHuyNiemYet)) // nếu nằm trong danh sách hủy niêm yết thì tất cả = 0
                    {
                        vFS_Report_LaiLo_Customer.KhoiLuongNhapKho = 0;
                        vFS_Report_LaiLo_Customer.GiaTriNhapKho = 0;
                        vFS_Report_LaiLo_Customer.KhoiLuongTonKho = 0;
                        vFS_Report_LaiLo_Customer.GiaTriTonKho = 0;
                        vFS_Report_LaiLo_Customer.GiaTrungBinh = 0;

                        
                    }
                    try
                    {
                        rp.Save(vFS_Report_LaiLo_Customer);
                        if (LastvFS_Report_LaiLo_Customer != null)
                        {
                            LastvFS_Report_LaiLo_Customer.LastVolumeBlance = "";
                            rp.Update(LastvFS_Report_LaiLo_Customer);
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }

                // end for transactionCreditDebit
            //}// end for customer
        }
    }
}
