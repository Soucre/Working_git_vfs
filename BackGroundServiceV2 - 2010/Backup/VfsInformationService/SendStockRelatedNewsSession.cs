using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using VfsInformationService.SBSWebService;
using System.Net;

using VfsCustomerService.Entities;
using VfsCustomerService.Data;
using VfsCustomerService.Business;

using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;
using Vfs.WebCrawler.Destination.Business;

namespace VfsCustomerInformationServices
{
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

    [Serializable]
    public class SendStockRelatedNewsSession: SendMessage
    {
        protected string endPointAddress;
        protected int templateID;
        protected int templateIDSms;
        protected PorscheCredentials porscheCredentials;

        public SendStockRelatedNewsSession(int commandBlockSize) : base(commandBlockSize)
        {
            Initialize();                       
        }

        protected override void Initialize()
        {
            base.Initialize();            
            endPointAddress = ConfigurationManager.AppSettings["EndpointAddress"];
            templateID = Convert.ToInt32(ConfigurationManager.AppSettings["RelatedMessageTemplateID"]);
            templateIDSms  = Convert.ToInt32(ConfigurationManager.AppSettings["RelatedSmsMessageTemplateID"]);
            porscheCredentials = Ultility.GetCrendentials("SBSSystem", "VSSD", "vssd", "123");
        }

        public SendStockRelatedNewsSession(int commandBlockSize, string userName, string password)
             : base(commandBlockSize, userName, password)
        {                      
            Initialize();
        }      

        public VfsInformationService.SBSWebService.PorscheCredentials GetCrendentials(string userPorcheGateway, string passPorcheGateway, string userSbs, string passSbs)
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
            porscheCredentials.ClientInfor.ClientIp = "127.0.0.1";
            porscheCredentials.ClientInfor.ClientName = Dns.GetHostName();
            porscheCredentials.RequestType = "BackOffice";
            return porscheCredentials;
        }
              
        public string SetNewsGroupName(Int64 NewsId)
        {
            string returnValue = string.Empty;
            stock_NewsGroupCollection stock_NewsGroupColl = stock_NewsGroupService.Getstock_NewsGroupList(NewsId, stock_NewsGroupColumns.NewsGroup, "ASC");
            if (stock_NewsGroupColl != null)
            {
                if (stock_NewsGroupColl.Count > 0)
                {
                    if (stock_NewsGroupColl[0].NewsGroup == (Int16)NewsGroup.Market) returnValue = NewsGroup.Market.ToString();
                    if (stock_NewsGroupColl[0].NewsGroup == (Int16)NewsGroup.MarketUpdate) returnValue = NewsGroup.MarketUpdate.ToString();
                    if (stock_NewsGroupColl[0].NewsGroup == (Int16)NewsGroup.Public) returnValue = NewsGroup.Public.ToString();
                    if (stock_NewsGroupColl[0].NewsGroup == (Int16)NewsGroup.Stock) returnValue = NewsGroup.Stock.ToString();
                }
            }
            return returnValue;
        }

        public void SendRelatedNew()
        {
            Int32 totalRows = 0;
            //CustomerCollection customerCollection = VfsCustomerService.Business.CustomerService.GetCustomerList(CustomerColumns.CustomerId, "DESC");            
            CustomerCollection customerCollection = VfsCustomerService.Business.CustomerService.GetCustomerListSearch(1, "ALL", "ALL", "ALL", "ALL", "ALL", CustomerColumns.CustomerId, "ASC", 0, 0, out totalRows);
            SendRelatedNew(customerCollection);
        }

        public void SendRelatedNew(VfsCustomerService.Entities.Customer customer)
        {
            var service = new SBSGatewaySoapClient();

            if(customer.CustomerId == "094P848586") return;

            service.Endpoint.Address = new System.ServiceModel.EndpointAddress(endPointAddress);
            PorscheCustomer porscheCustomer = new PorscheCustomer();
            porscheCustomer.CustomerId = customer.CustomerId;
            ClientParam clientParam = new ClientParam();            

            CustomerInquiryData customerData = service.CustomerInquiry
                                                           (porscheCredentials,
                                                           porscheCustomer,                                                            
                                                           GetCustomerType.None.ToString(),
                                                           CustomerStockInquiryType.Normal.ToString(),
                                                           CustomerAmountInquiryType.Full.ToString());
            if (customerData.ErrorCode != SbsErrorCode.None)
            {
                //throw new Exception(customerData.Message);
                Ultility.Info(customer.CustomerId + ": " + customerData.Message);
            }
            else
            {
                Ultility.Info(customer.CustomerId + ": " + customer.CustomerId);
                //dataGridView1.DataSource = customerData.StockItems;
                CustomerStockInquiryObject[] customerInquiryData;
                if (customerData != null)
                {
                    stock_NewCollection stockNewsCollection = new stock_NewCollection();
                    Vfs.WebCrawler.Destination.Entities.RelatedMessagelog relatedMessagelog;
                    Vfs.WebCrawler.Destination.Entities.RelatedMessagelogCollection relatedMessagelogColl = new Vfs.WebCrawler.Destination.Entities.RelatedMessagelogCollection();
                    customerInquiryData = customerData.StockItems;
                    StringBuilder content = new StringBuilder();
                    string contentSms = string.Empty;
                   
                    int smsCount = 0;
                    int total;
                    Boolean sentYn = false;
                    smsCount = 0;
                    content.Append("<table>");
                    foreach (CustomerStockInquiryObject obj in customerInquiryData)
                    {
                        Ultility.Info(customer.CustomerId + ": " + obj.StockCode);
                        stockNewsCollection = stock_NewService.Getstock_NewList(obj.StockCode, customer.CustomerId , stock_NewColumns.NewsID, "DESC", 1, 3, out total);
                        if (stockNewsCollection.Count > 0) sentYn = true;
                      
                        foreach (stock_New stock_new in stockNewsCollection)
                        {
                            content.Append("<tr style=\"height:28px\">");
                            content.Append("<td style=\"background-color:#FFCC00;font-weight:bold;width:40px; heigh:25px; text-align:left; font:Tahoma;\">" + obj.StockCode + "</td>");
                            content.Append("<td style=\"background-color:#F5F5F5;text-align:left;font:Tahoma;\"><a style=\"cursor:pointer; text-decoration:none;font:Tahoma;color:#000000;\" href=\"http://www.vfs.com.vn/News.aspx?newsid=" + stock_new.NewsID.ToString() + "&group=" + SetNewsGroupName(stock_new.NewsID) + "\"" + ">" + (stock_new.NewsTitle.Normalize().IndexOf(':') == 3 ? stock_new.NewsTitle.Normalize().Substring(4) : stock_new.NewsTitle.Normalize().Substring(3)) + "</a></td>");
                            content.Append("</tr>");
                            if (smsCount == 0)
                            {
                                if (contentSms == string.Empty)
                                    contentSms += "(" + Ultility.RemoveSound(stock_new.NewsTitle.Normalize()) + ")";
                                else
                                    contentSms += ";(" + Ultility.RemoveSound(stock_new.NewsTitle.Normalize()) + ")";
                            }
                            smsCount++;
                            relatedMessagelog = new Vfs.WebCrawler.Destination.Entities.RelatedMessagelog();
                            relatedMessagelog.NewsID = stock_new.NewsID;
                            relatedMessagelog.CustomerID = customer.CustomerId;
                            relatedMessagelogColl.Add(relatedMessagelog);
                        }                       
                    }
                    content.Append("</table>");
                    //create messsages to be sent
                    if (sentYn)
                    {
                        ContentTemplate contentTemplate = ContentTemplateService.GetContentTemplate(templateID);
                        ContentTemplate contentTemplateSms = ContentTemplateService.GetContentTemplate(templateIDSms);                        
                        VfsCustomerService.Business.ImportService.CreateMessage(customer, content.ToString(), contentSms, contentTemplate, contentTemplateSms);

                        foreach (Vfs.WebCrawler.Destination.Entities.RelatedMessagelog relatedMsglog in relatedMessagelogColl)
                        {
                            Vfs.WebCrawler.Destination.Business.RelatedMessagelogService.CreateRelatedMessagelog(relatedMsglog);
                        }
                    }
                }            
            }
        }

        public void SendRelatedNew(VfsCustomerService.Entities.CustomerCollection customers)
        {
            foreach (VfsCustomerService.Entities.Customer cust in customers)
            {
                SendRelatedNew(cust);
            }
        }

        public void SendRelatedNewTest()
        {
            var service = new SBSGatewaySoapClient();

            service.Endpoint.Address = new System.ServiceModel.EndpointAddress(endPointAddress);
            CustomerInquiryData customerData = service.CustomerInquiry
                                                           (porscheCredentials,
                                                           new PorscheCustomer { CustomerId = "094C000006" },
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
                    int total;
                    foreach (CustomerStockInquiryObject obj in customerInquiryData)
                    {
                        stock_NewCollection stockNewsCollection = stock_NewService.Getstock_NewList(obj.StockCode, "094C000006", stock_NewColumns.NewsID, "DESC", 1, 3, out total);
                        Vfs.WebCrawler.Destination.Entities.RelatedMessagelogCollection relatedMessagelogColl = new Vfs.WebCrawler.Destination.Entities.RelatedMessagelogCollection();
                        smsCount = 0;
                        foreach (stock_New stock_new in stockNewsCollection)
                        {
                            content += "<a href=\"www.vfs.com.vn/News.aspx?newsid=\"" + stock_new.NewsID.ToString() + "&group=" + SetNewsGroupName(stock_new.NewsID) + "\" >" + stock_new.NewsTitle.Normalize() + "</a> <br/>";

                            if (smsCount == 0)
                            {
                                if (contentSms == string.Empty)
                                    contentSms += "(" + Ultility.RemoveSound(stock_new.NewsTitle.Normalize()) + "-www.vfs.com.vn/News.aspx?newsid=" + stock_new.NewsID.ToString() + "&group=" + SetNewsGroupName(stock_new.NewsID) + ")";
                                else
                                    contentSms += ";(" + Ultility.RemoveSound(stock_new.NewsTitle.Normalize()) + "-www.vfs.com.vn/News.aspx?newsid=" + stock_new.NewsID.ToString() + "&group=" + SetNewsGroupName(stock_new.NewsID) + ")";
                            }
                            smsCount++;
                            Vfs.WebCrawler.Destination.Entities.RelatedMessagelog relatedMessagelog = new Vfs.WebCrawler.Destination.Entities.RelatedMessagelog();
                            relatedMessagelog.NewsID = stock_new.NewsID;
                            relatedMessagelogColl.Add(relatedMessagelog);
                        }
                        //create messsages to be sent
                        ContentTemplate contentTemplate = ContentTemplateService.GetContentTemplate(templateID);
                        ContentTemplate contentTemplateSms = ContentTemplateService.GetContentTemplate(templateIDSms);
                        VfsCustomerService.Entities.Customer customer = VfsCustomerService.Business.CustomerService.GetCustomer("094C000006");
                        VfsCustomerService.Business.ImportService.CreateMessage(customer, content, contentSms, contentTemplate, contentTemplateSms);

                        foreach (Vfs.WebCrawler.Destination.Entities.RelatedMessagelog relatedMessagelog in relatedMessagelogColl)
                        {
                            Vfs.WebCrawler.Destination.Business.RelatedMessagelogService.CreateRelatedMessagelog(relatedMessagelog);
                        }
                    }
                }
            }
        }
    }
}
