using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Globalization;
using log4net;
using UnitTest.SBSWebService;
using System.Configuration;


using VfsCustomerService.Entities;
using VfsCustomerService.Data;
using VfsCustomerService.Business;

using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;
using Vfs.WebCrawler.Destination.Business;

namespace UnitTest
{    
    public enum CustomerReportType
        {
            TradingConfirm = 0,
            BalanceTransaction = 1,
            ContigenTransaction = 2,
            OrderHistory = 3,
            OrderHistoryWithStatus = 4,
            AccountSummary = 5,
            CustomerLostAndProfit = 6,
            CustomerLostAndProfitAllStock = 7,
            CustomerAccountHistoryReceive = 8,
            CustomerAccountPendingReceive = 9,
            OrderListHistory = 10,


        }

    public enum GetCustomerType
    {
        None = 0,
        Lite = 1,
        LiteWithBankInfor = 2,
        BaseInforWithSign = 3,
        FullInformation = 4,
        ProxyOnly = 5,
    }
    
    public enum CustomerStockInquiryType
    {
        None = 0,
        Normal = 1,
    }
    
    public enum CustomerAmountInquiryType
    {
        None = 0,
        Common = 1,
        Full = 2,
    }

    public enum NewsGroup
    {
        Market = 0,
        MarketUpdate = 1,
        Public = 2,
        Stock = 3,
    }    
     
    class Program
    {
        public static readonly string[] VietNamChar = new string[]
	    {
	        "aAeEoOuUiIdDyY",
	        "áàạảãâấầậẩẫăắằặẳẵ",
	        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
	        "éèẹẻẽêếềệểễ",
	        "ÉÈẸẺẼÊẾỀỆỂỄ",
	        "óòọỏõôốồộổỗơớờợởỡ",
	        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
	        "úùụủũưứừựửữ",
	        "ÚÙỤỦŨƯỨỪỰỬỮ",
	       "íìịỉĩ",
	        "ÍÌỊỈĨ",
	        "đ",
	        "Đ",
	        "ýỳỵỷỹ",
	        "ÝỲỴỶỸ"
	    };

        static void Main(string[] args)
        {
            Ultility.Info("test log4net");       

            Console.WriteLine("pause");
            //VfsCustomerService.Entities.CustomerCollection customers = VfsCustomerService.Business.CustomerService.GetCustomerListReceiveMessage(CustomerColumns.CustomerId, "ASC");
            //VfsCustomerService.Entities.Customer c = new VfsCustomerService.Entities.Customer();
            //SendRelatedNew(c);
            //SendRelatedNew(customers);
            //SendStockRelatedNewsSession sendStockRelatedNewsSession = new SendStockRelatedNewsSession(10);
            //sendStockRelatedNewsSession.SendRelatedNewTest();          
            TradingResultReportBase[] tradingResultReportBase = GetSecurities4Settlement("094C000606");
            string test;
            test = "g";
        }

        public static TradingResultReportBase[] GetSecurities4Settlement(string customerID)
        {

            string BranchCode = "100";
            string TradeCode = "094";
            string OnlineTradingUser = "094C000027";

            SBSWebService.SBSGatewaySoapClient sbsGateway = new SBSWebService.SBSGatewaySoapClient();

            DateTime currentdate = sbsGateway.CurrentTransactionDate(GetCrendentials("SBSSystem", "VSSD", "vssd", "123"));

           // SfsGateway.sfsGateway sfsgateway = new sfsGateway();//lấy về đối tượng gate way       

            ////SfsGateway.UserInfo userInfo = sfsgateway.Login(sfsGatewayUserName, sfsGatewayPassword);

            //Created  ServerCertificate
            //ServerCertificate cert = new ServerCertificate();
            //cert.ServerRoleCirtificate = "anystring";

            List<TradingResultReportBase> lstStockOrderResultData = new List<TradingResultReportBase>();

            TradingResultReportBase[] dataArray = lstStockOrderResultData.ToArray();
            try
            {
                //dataArray = new StockOrderDAL().GetSecurities4Settlement(customerID);


                //CustomerInquiryData balanceData = sbsGateway.CustomerInquiry(SBSCredentials.Credential, new PorscheCustomer { CustomerId=customerID }, GetCustomerType.FullInformation.ToString(), CustomerStockInquiryType.Normal.ToString(), CustomerAmountInquiryType.Full.ToString());
                DateTime serverDate = currentdate;
                DateTime fromDate = serverDate;

                int dayspan = 0;
                for (int i = 1; i < 1000; i++)
                {
                    //
                    if (sbsGateway.IsWorkingTransactionDate(GetCrendentials("SBSSystem", "VSSD", "vssd", "123"), serverDate.AddDays(-i)))
                    {
                        dayspan++;
                        if (dayspan == 3)
                        {
                            dayspan = i;
                            fromDate = serverDate.AddDays(-i);
                            break;
                        }
                    }
                }



                for (int day = 0; day <= dayspan; day++)
                {
                    SBSWebService.CustomerReportData customerReportData = sbsGateway.GetCustomerReportData(GetCrendentials("SBSSystem", "VSSD", "vssd", "123"), new PorscheCustomer { CustomerId = customerID }, serverDate.AddDays(-day), serverDate, CustomerReportType.CustomerAccountPendingReceive.ToString());

                    //if (customerReportData.ErrorCode.ToString() != EnumErrorCode.success.ToString())
                    //{
                    //    throw new Exception(customerReportData.Message);
                    //}
                    if (customerReportData != null && customerReportData.CustomerAccountReceive.TradingResultItems.Length > 0)
                    {
                        for (int i = 0; i < customerReportData.CustomerAccountReceive.TradingResultItems.Length; i++)
                        {
                            if (customerReportData.CustomerAccountReceive.TradingResultItems[i].OrderSide.ToUpper() == "B")
                            {
                                TradingResultReportBase stockOrderResultData = new TradingResultReportBase();

                                stockOrderResultData.BoardType = customerReportData.CustomerAccountReceive.TradingResultItems[i].BoardType;
                                stockOrderResultData.BranchCode = customerReportData.CustomerAccountReceive.TradingResultItems[i].BranchCode;
                                stockOrderResultData.ConfirmNo = customerReportData.CustomerAccountReceive.TradingResultItems[i].ConfirmNo;
                                stockOrderResultData.ConfirmTime = customerReportData.CustomerAccountReceive.TradingResultItems[i].ConfirmTime;
                                stockOrderResultData.CustomerId = customerReportData.CustomerAccountReceive.TradingResultItems[i].CustomerId.ToUpper();
                                stockOrderResultData.TradeCode = customerReportData.CustomerAccountReceive.TradingResultItems[i].CustomerTradeCode;
                                stockOrderResultData.FeeRate = customerReportData.CustomerAccountReceive.TradingResultItems[i].FeeRate;
                                stockOrderResultData.FeeValue = stockOrderResultData.FeeRate * customerReportData.CustomerAccountReceive.TradingResultItems[i].MatchedValue;
                                stockOrderResultData.MatchedPrice = customerReportData.CustomerAccountReceive.TradingResultItems[i].MatchedPrice;
                                stockOrderResultData.MatchedValue = customerReportData.CustomerAccountReceive.TradingResultItems[i].MatchedValue;
                                stockOrderResultData.MatchedVolume = customerReportData.CustomerAccountReceive.TradingResultItems[i].MatchedVolume;
                                stockOrderResultData.NoPost = customerReportData.CustomerAccountReceive.TradingResultItems[i].NoPost;
                                stockOrderResultData.OrderDate = customerReportData.CustomerAccountReceive.TradingResultItems[i].TransactionDate.ToString("dd/MM/yyyy");
                                stockOrderResultData.OrderNo = customerReportData.CustomerAccountReceive.TradingResultItems[i].OrderNo;
                                stockOrderResultData.OrderSeq = customerReportData.CustomerAccountReceive.TradingResultItems[i].OrderSeq;
                                stockOrderResultData.OrderSide = customerReportData.CustomerAccountReceive.TradingResultItems[i].OrderSide;
                                stockOrderResultData.PaymentDate = customerReportData.CustomerAccountReceive.TradingResultItems[i].PaymentDate;
                                stockOrderResultData.StockCode = customerReportData.CustomerAccountReceive.TradingResultItems[i].StockCode;
                                stockOrderResultData.StockType = customerReportData.CustomerAccountReceive.TradingResultItems[i].StockType;
                                stockOrderResultData.TradeCode = customerReportData.CustomerAccountReceive.TradingResultItems[i].TradeCode;

                                lstStockOrderResultData.Add(stockOrderResultData);


                                //stockOrderResultData. = customerReportData.BalanceTransactionItem.Items[i].AccountDebitOrCredit;
                                //stockOrderResultData.BranchCode = customerReportData.CustomerAccountReceive.TradingResultItems[i].BankCode;
                                //stockOrderResultData.BoardType = customerReportData.CustomerAccountReceive.TradingResultItems[i].BoardTypePriority;
                                //stockOrderResultData.CurrentPrice = customerReportData.CustomerAccountReceive.TradingResultItems[i].CurrentPrice;
                                //stockOrderResultData.CustomerBranchCode = customerReportData.CustomerAccountReceive.TradingResultItems[i].CustomerBranchCode;
                                //stockOrderResultData.CustomerNameViet = customerReportData.CustomerAccountReceive.TradingResultItems[i].CustomerNameViet;
                                //stockOrderResultData.DateAveragePrice = customerReportData.CustomerAccountReceive.TradingResultItems[i].DateAveragePrice;
                                //stockOrderResultData.DateVolume = customerReportData.CustomerAccountReceive.TradingResultItems[i].DateVolume;
                                //stockOrderResultData.Deferred = customerReportData.CustomerAccountReceive.TradingResultItems[i].Deferred;

                                //stockOrderResultData.FeeRemain = customerReportData.CustomerAccountReceive.TradingResultItems[i].FeeRemain;
                                //stockOrderResultData.FeeTransfer = customerReportData.CustomerAccountReceive.TradingResultItems[i].FeeTransfer;

                                //stockOrderResultData.FeeWithExchange = customerReportData.CustomerAccountReceive.TradingResultItems[i].FeeWithExchange;
                                //stockOrderResultData.IsCross = customerReportData.CustomerAccountReceive.TradingResultItems[i].IsCross;
                                //stockOrderResultData.IsPrivateAccount = customerReportData.CustomerAccountReceive.TradingResultItems[i].IsPrivateAccount;
                                //stockOrderResultData.IsPrivateAccountString = customerReportData.CustomerAccountReceive.TradingResultItems[i].IsPrivateAccountString;
                                //stockOrderResultData.OrderPrice = customerReportData.CustomerAccountReceive.TradingResultItems[i].OrderPrice;
                                //stockOrderResultData.Status = customerReportData.CustomerAccountReceive.TradingResultItems[i].OrderStatus;
                                //stockOrderResultData.OrderTime = customerReportData.CustomerAccountReceive.TradingResultItems[i].OrderTime;
                                //stockOrderResultData.OrderType = customerReportData.CustomerAccountReceive.TradingResultItems[i].OrderType;
                                //stockOrderResultData.OrderVolume = customerReportData.CustomerAccountReceive.TradingResultItems[i].OrderVolume;
                                //stockOrderResultData.ReceivedBy = customerReportData.CustomerAccountReceive.TradingResultItems[i].ReceivedBy;
                                //stockOrderResultData.T = customerReportData.CustomerAccountReceive.TradingResultItems[i].T;
                                //stockOrderResultData.T3 = customerReportData.CustomerAccountReceive.TradingResultItems[i].T3;
                                //stockOrderResultData.TransactionDate = customerReportData.CustomerAccountReceive.TradingResultItems[i].TransactionDate;


                            }
                        }
                    }

                }
                dataArray = lstStockOrderResultData.ToArray();


            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
            return dataArray;
        }

        public static void LoginWs()
        {
            PorscheCredentials objCredentials = new PorscheCredentials();
            objCredentials.GatewayPassword = "vssd";
            objCredentials.GatewayUsername = "123";
            SBSGatewaySoapClient objClient = new SBSGatewaySoapClient();
            objClient.Login("SBSSystem", "123456");
        }

        public static PorscheCredentials GetCrendentials(string userPorcheGateway, string passPorcheGateway, string userSbs, string passSbs)
        {
            PorscheCredentials porscheCredentials = new PorscheCredentials();

            string UserPorcheGateway = userPorcheGateway;
            string PassPorcheGateway = passPorcheGateway;
            string UserSbs = userSbs;
            string PassSbs = passSbs;
            porscheCredentials.GatewayPassword = PassPorcheGateway;
            porscheCredentials.GatewayUsername = UserPorcheGateway;
            porscheCredentials.SBSUser = new PorscheCredentialsUser();
            porscheCredentials.SBSUser.UserName = UserSbs;
            porscheCredentials.SBSUser.Password = PassSbs;
            porscheCredentials.ClientInfor = new ClientInformation();
            porscheCredentials.ClientInfor.ClientIp = "192.168.1.2";
            porscheCredentials.ClientInfor.ClientName = "giangum";
            porscheCredentials.RequestType = "BackOffice";
            return porscheCredentials;
        }

        public static void SendRelatedNew(VfsCustomerService.Entities.Customer customer)
        {
            PorscheCredentials porscheCredentials = GetCrendentials("SBSSystem", "VSSD", "vssd", "123");
            var service = new SBSGatewaySoapClient();
            service.Endpoint.Address = new System.ServiceModel.EndpointAddress("http://Svr_06/SBSGateway/SBSGateway.asmx");
            ClientParam  clientParam = new ClientParam();
            clientParam.FromDate = new DateTime(2000, 01, 01);
            clientParam.ToDate = DateTime.Now;
            clientParam.TransactionDate = DateTime.Now;
            clientParam.LanguageCode = "vi_VN";            
            CustomerData customerData01 = service.DoCustomerWithAction(porscheCredentials, clientParam, null, "List");
                //(GetCustomerBTP(porscheCredentials, clientParam);
 UnitTest.SBSWebService.
            CustomerInquiryData customerData = service.CustomerInquiry
                                                           (porscheCredentials,
                                                           new PorscheCustomer { CustomerId = customer.CustomerId },
                                                           GetCustomerType.None.ToString(),
                                                           CustomerStockInquiryType.Normal.ToString(),
                                                           CustomerAmountInquiryType.Full.ToString());
            if (customerData.ErrorCode != SbsErrorCode.None)
            {
                throw new Exception(customerData.Message);
            }
            else
            {
                //dataGridView1.DataSource = customerData.StockItems;
                CustomerStockInquiryObject[] customerInquiryData;
                if (customerData != null)
                {
                    customerInquiryData = customerData.StockItems;
                    string content = string.Empty;
                    string contentSms = string.Empty;
                    int templateID = Convert.ToInt32(ConfigurationManager.AppSettings["RelatedMessageTemplateID"]);
                    int templateIDSms = Convert.ToInt32(ConfigurationManager.AppSettings["RelatedSmsMessageTemplateID"]);
                    int smsCount = 0;
                    //foreach (CustomerStockInquiryObject obj in customerInquiryData)
                    //{
                    //    stock_NewCollection stockNewsCollection = stock_NewService.Getstock_NewList(obj.StockCode, stock_NewColumns.NewsID, "DESC");
                    //    smsCount = 0;
                    //    foreach (stock_New stock_new in stockNewsCollection)
                    //    {
                    //        content += "<a href=\"www.vfs.com.vn/News.aspx?newsid=\"" + stock_new.NewsID.ToString() + "&group=" + SetNewsGroupName(stock_new.NewsID) + "\" >" + stock_new.NewsTitle.Normalize() + "</a> <br/>";

                    //        if (smsCount == 0)
                    //        {
                    //            if (contentSms == string.Empty)
                    //                contentSms += "(" + RemoveSound(stock_new.NewsTitle.Normalize()) + "-www.vfs.com.vn/News.aspx?newsid=" + stock_new.NewsID.ToString() + "&group=" + SetNewsGroupName(stock_new.NewsID) + ")";
                    //            else
                    //                contentSms += ";(" + RemoveSound(stock_new.NewsTitle.Normalize()) + "-www.vfs.com.vn/News.aspx?newsid=" + stock_new.NewsID.ToString() + "&group=" + SetNewsGroupName(stock_new.NewsID) + ")";
                    //        }
                    //        smsCount++;
                    //        RelatedMessagelog relatedMessagelog = new RelatedMessagelog();
                    //        relatedMessagelog.NewsID = stock_new.NewsID;
                    //        VfsCustomerService.Business.RelatedMessagelogService.CreateRelatedMessagelog(relatedMessagelog);
                    //    }
                    //    //create messsages to be sent
                    //    ContentTemplate contentTemplate = ContentTemplateService.GetContentTemplate(templateID);
                    //    ContentTemplate contentTemplateSms = ContentTemplateService.GetContentTemplate(templateIDSms);
                    //    VfsCustomerService.Business.ImportService.CreateMessage(customer, content, contentSms, contentTemplate, contentTemplateSms);
                    //}
                }
            }
        }

        //public static string SetNewsGroupName(Int64 NewsId)
        //{
        //    string returnValue = string.Empty;
        //    stock_NewsGroupCollection stock_NewsGroupColl = stock_NewsGroupService.Getstock_NewsGroupList(NewsId, stock_NewsGroupColumns.NewsGroup, "ASC");
        //    if(stock_NewsGroupColl != null)
        //    {
        //        if(stock_NewsGroupColl.Count > 0)
        //        {
        //            if (stock_NewsGroupColl[0].NewsGroup == (Int16)NewsGroup.Market) returnValue = NewsGroup.Market.ToString();
        //            if (stock_NewsGroupColl[0].NewsGroup == (Int16)NewsGroup.MarketUpdate) returnValue = NewsGroup.MarketUpdate.ToString();
        //            if (stock_NewsGroupColl[0].NewsGroup == (Int16)NewsGroup.Public) returnValue = NewsGroup.Public.ToString();
        //            if (stock_NewsGroupColl[0].NewsGroup == (Int16)NewsGroup.Stock) returnValue = NewsGroup.Stock.ToString();
        //        }
        //    }
        //    return returnValue;
        //}

        //public static void SendRelatedNew(VfsCustomerService.Entities.CustomerCollection customers)
        //{
        //    foreach (VfsCustomerService.Entities.Customer cust in customers)
        //    {
        //        SendRelatedNew(cust);
        //    }
        //}
       
        public static string RemoveSound(string str)
        {
            //Thay thế và lọc dấu từng char     
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }
            return str;
        }
    }

    public static class Ultility
    {
        public static readonly string[] VietNamChar = new string[]
	    {
	        "aAeEoOuUiIdDyY",
	        "áàạảãâấầậẩẫăắằặẳẵ",
	        "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
	        "éèẹẻẽêếềệểễ",
	        "ÉÈẸẺẼÊẾỀỆỂỄ",
	        "óòọỏõôốồộổỗơớờợởỡ",
	        "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
	        "úùụủũưứừựửữ",
	        "ÚÙỤỦŨƯỨỪỰỬỮ",
	       "íìịỉĩ",
	        "ÍÌỊỈĨ",
	        "đ",
	        "Đ",
	        "ýỳỵỷỹ",
	        "ÝỲỴỶỸ"
	    };

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static DateTime ConvertStringToDate(string dateString)
        {
            DateTimeFormatInfo datefomatProvider = new DateTimeFormatInfo();
            datefomatProvider.DateSeparator = "/";
            datefomatProvider.FullDateTimePattern = "dd/MM/yyyy";
            datefomatProvider.LongDatePattern = "dd/MM/yyyy";
            return new DateTime(int.Parse(dateString.Substring(6, 4)), int.Parse(dateString.Substring(3, 2)), int.Parse(dateString.Substring(0, 2)));

            //return DateTime.Parse(dateString);
            //return Convert.ToDateTime(dateString, datefomatProvider);
        }

        public static void Info(object message)
        {            
            if (log.IsDebugEnabled)
            {
                log.Info(message);
            }
        }

        public static void Info(object message, Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Info(message, ex);
            }
        }

        public static void Error(object message)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(message);
            }
        }

        public static void Error(object message, Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error(message, ex);
            }
        }

        public static string CutAddressHead(string source)
        {
            string s = source;
            if (s.Length <= 160) return s;
            string temp = s.Substring(0, 160);
            int pos = temp.LastIndexOf(" ");
            s = temp.Substring(0, pos);
            return s;
        }

        public static string CutAddressEnd(string source)
        {
            string s = source;
            if (s.Length <= 160) return string.Empty;
            string temp = s.Substring(0, 160);
            int pos = temp.LastIndexOf(" ");
            s = s.Substring(pos + 1, ((s.Length - 1) - pos));
            return s;
        }

        public static string RemoveSound(string str)
        {
            //Thay thế và lọc dấu từng char     
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }
            return str;
        }
    }

    public class WaitingPayCashStockDTO 
    { // đối tượng mô tả thông tin tiền và chứng khoán chờ về
	    private String transactionDate; //ngay giao dich
	    private String datePay; // ngay ve
	    private String stockCode;
	    private String orderSide;
	    private String orderSeq; 
	    private double orderVolume;
	    private double orderPrice;
	    private double matchedVolume; //khoi luong
	    private double matchedPrice; //gia
	    private double orderFee; // phi 
	    private double orderTax; // thuế
	    private double valueSubFee; //Gia tri ban tru phi moi gioi
	    private double valueAddFee;	//Gia tri mua cong phi
	    private double cT1;
	    private double cT2;
	    private double cT3;
	    private double sT1;
	    private double sT2;
	    private double sT3;
	    private double CKGD;	 // CK GD
	    private double CKHC;   // CKHC
	    private bool T;
    }

}
