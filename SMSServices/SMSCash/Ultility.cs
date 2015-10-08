using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Core;
using Core.Domain.Model;
using Core.Domain.Repositories;
using System.Configuration;
using Bussiness;
using System.Data.SqlClient;
using System.Data;

namespace SMS
{
    public static class Ultility
    {
        static string FileOutPut = ConfigurationManager.AppSettings["FileOutPut"].ToString();

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
        public static void syncSentCash(SendSMS sendSMS)
        {
                      
            //sendSMS.SendSPAM("0909070481", "test gui tin nhan cash 1");

            IRepository<VFS_CheckSMSSent> repoVFS_CheckSMSSent = new VFS_CheckSMSSentRepository();
            VFS_CheckSMSSent VFS_CheckSMSSent = new VFS_CheckSMSSent();
            

            VFS_TransactionDayRepository repoVFS_VFS_TransactionDay = new VFS_TransactionDayRepository();
            IList<VFS_TransactionDay> listVFS_VFS_TransactionDay =  repoVFS_VFS_TransactionDay.listTransactionDayFromSP();

          
            if (listVFS_VFS_TransactionDay == null) // nếu bút toán ngoại bảng ngày hiện tại không có dữ liệu, tức là đã tạo ngày mới
                                                    // và lúc này xóa đi dữ liệu cho việc gửi tin
            {
                // xóa tin nhan da gui cho ngay moi
                repoVFS_CheckSMSSent.Delete(VFS_CheckSMSSent); // delete all data in VFS_CheckSMSSent
                LogFile("Delete for new day", FileOutPut);
                return;
            }
            foreach (var item in listVFS_VFS_TransactionDay)
            {
                if (item.CorAccount.Substring(0, 3) == "112") // nop rut tien tai khoan 112
                {
                    if (item.Debit == "C")
                    {                        
                        if (checkSendSMS(item.Id))
                        {
                            string numbermobile = getMobileCustomer(item.AccountId.Replace("K", "C"));
                            if (numbermobile.Length <= 11)
                            {
                                //send sms 
                                sendSMS.SendSPAM(numbermobile, "Nop tien: " + string.Format("{0,0:N0}", item.CreditAmount) + " vao tai khoan " + item.AccountId);
                            }
                            LogFile(numbermobile + "----- Begin Sending SMS -----" + "Nop tien: " + string.Format("{0,0:N0}", item.CreditAmount) + " vao tai khoan " + item.AccountId, FileOutPut);
                        }
                    }
                    else
                    {
                        //send sms rut 112
                        if (checkSendSMS(item.Id))
                        {
                            string numbermobile = getMobileCustomer(item.AccountId.Replace("K", "C"));
                            if (numbermobile.Length <= 11)
                            {
                                //send sms 
                                sendSMS.SendSPAM(numbermobile, "Rut tien: " + string.Format("{0,0:N0}", item.DebitAmount) + " tu tai khoan " + item.AccountId);
                       
                            }
                            LogFile(numbermobile + "----- Begin Sending SMS -----" + "Rut tien: " + string.Format("{0,0:N0}", item.DebitAmount) + " tu tai khoan " + item.AccountId, FileOutPut);
                        }
                    }
                }

                // nop rut qua trung gian

                if (item.CorAccount == "3388110005") // rut qua Trading Online
                {
                    //send sms rut 112
                    if (checkSendSMS(item.Id))
                    {
                        string numbermobile = getMobileCustomer(item.AccountId.Replace("K", "C"));
                        if (numbermobile.Length <= 11)
                        {
                            //send sms
                            sendSMS.SendSPAM(numbermobile, "Rut tien qua Trading Online: " + string.Format("{0,0:N0}", item.DebitAmount) + " tu tai khoan " + item.AccountId);
                        }
                        LogFile(numbermobile + "----- Begin Sending SMS -----" + "Rut tien qua Trading Online: " + string.Format("{0,0:N0}", item.DebitAmount) + " tu tai khoan " + item.AccountId, FileOutPut);
                    }
                }

                if (item.CorAccount == "1362110002" || item.CorAccount == "3362110002") // KH nop o HCM <=> HN
                {
                    if (item.Debit == "D")
                    {
                        //send sms nop 112
                        if (checkSendSMS(item.Id))
                        {
                            string numbermobile = getMobileCustomer(item.AccountId.Replace("K", "C"));
                            if (numbermobile.Length <= 11)
                            {
                                //send sms     
                                sendSMS.SendSPAM(numbermobile, "KH rut: " + string.Format("{0,0:N0}", item.DebitAmount) + " tu tai khoan " + item.AccountId);
                            }
                            LogFile(numbermobile + "----- Begin Sending SMS -----" + "KH rut: " + string.Format("{0,0:N0}", item.DebitAmount) + " tu tai khoan " + item.AccountId, FileOutPut);
                        }
                    }
                    else
                    {
                        //send sms nop 112
                        if (checkSendSMS(item.Id))
                        {
                            string numbermobile = getMobileCustomer(item.AccountId.Replace("K", "C"));
                            if (numbermobile.Length <= 11)
                            {
                                //send sms   
                                sendSMS.SendSPAM(numbermobile, "KH nop: " + string.Format("{0,0:N0}", item.CreditAmount) + " vao tai khoan " + item.AccountId);
                            }
                            LogFile(numbermobile + "----- Begin Sending SMS -----" + "KH nop: " + string.Format("{0,0:N0}", item.CreditAmount) + " vao tai khoan " + item.AccountId, FileOutPut);
                        }
                    }
                }
                
                // chuyen khoan qua lai tai khoan Cash <=> margin

                if (item.CorAccount.Substring(0, 3) == "094" && (item.CorAccount.Substring(4, 6) != item.AccountId.Substring(4, 6))) // nop rut tien tai khoan 112
                {
                    if (item.Debit == "D")
                    {
                        //send sms rut 112
                        if (checkSendSMS(item.Id))
                        {
                            string numbermobile = getMobileCustomer(item.AccountId.Replace("K", "C"));
                            if (numbermobile.Length <= 11)
                            {
                                //send sms   
                                sendSMS.SendSPAM(numbermobile, "Da chuyen so tien: " + string.Format("{0,0:N0}", item.DebitAmount) + " cho tai khoan " + item.CorAccount);
                            }
                            LogFile(numbermobile + "----- Begin Sending SMS -----" + "Da chuyen so tien: " + string.Format("{0,0:N0}", item.DebitAmount) + " cho tai khoan " + item.CorAccount, FileOutPut);
                        }
                    }
                    else
                    {
                        //send sms nop 112
                        if (checkSendSMS(item.Id))
                        {
                            string numbermobile = getMobileCustomer(item.AccountId.Replace("K", "C"));
                            if (numbermobile.Length <= 11)
                            {
                              
                                //send sms   
                                sendSMS.SendSPAM(numbermobile, "Nhan tien tu tai khoan: " + item.CorAccount + " so tien: " + string.Format("{0,0:N0}", item.CreditAmount));
                            }
                            LogFile(getMobileCustomer(item.AccountId.Replace("K", "C")) + "----- Begin Sending SMS -----" + "Nhan tien tu tai khoan: " + item.CorAccount + " so tien:" + string.Format("{0,0:N0}", item.CreditAmount), FileOutPut);
                        }
                    }
                }
            }
        }

        static bool checkSendSMS(int id)
        {
            bool resual = false;
            IRepository<VFS_CheckSMSSent> repoVFS_CheckSMSSent = new VFS_CheckSMSSentRepository();
            VFS_CheckSMSSent listcheck = repoVFS_CheckSMSSent.GetById(id);

            if (listcheck == null)
            {
                resual = true;
                VFS_CheckSMSSent item = new VFS_CheckSMSSent();
                item.Id = id;
                repoVFS_CheckSMSSent.Save(item);
            }

            return resual;
        }
        static string getMobileCustomer(string customerCode)
        {
            ICustomerServiceRepository<CustomerService> repoCustomerService = new CustomerServiceRepository();
            CustomerService VFS_CheckSMSSent = repoCustomerService.getCustomerId(customerCode);
            string mobile = "";
            if (VFS_CheckSMSSent == null || VFS_CheckSMSSent.Mobile == "")
            {
                return "Please set your mobile number";
            }
            if (VFS_CheckSMSSent.CustomerId.Substring(4, 6) == customerCode.Substring(4, 6))
            {
                mobile = VFS_CheckSMSSent.Mobile;
            }
            
            return mobile;
        }

        static public string CheckDatabaseConnection
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["CheckDatabaseConnection"].ToString();

            }
        }
        public static bool CheckConnectionSQL()
        {
            try
            {
                using (SqlConnection myConn = new SqlConnection(CheckDatabaseConnection))
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
        
    }
}
