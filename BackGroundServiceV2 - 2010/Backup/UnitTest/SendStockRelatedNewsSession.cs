using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using UnitTest.SBSWebService;

using VfsCustomerService.Entities;
using VfsCustomerService.Data;
using VfsCustomerService.Business;

using Vfs.WebCrawler.Destination.Entities;
using Vfs.WebCrawler.Destination.Data;
using Vfs.WebCrawler.Destination.Business;

namespace UnitTest
{
    

    [Serializable]
    public class SendStockRelatedNewsSession: SendMessage
    {
        protected PorscheCredentials porscheCredentials;

        public SendStockRelatedNewsSession(int commandBlockSize) : base(commandBlockSize)
        {
            Initialize();           
        }

        protected override void Initialize()
        {
            base.Initialize();
            porscheCredentials = GetCrendentials("SBSSystem", "VSSD", "vssd", "123");
        }

        public SendStockRelatedNewsSession(int commandBlockSize, string userName, string password)
             : base(commandBlockSize, userName, password)
        {                      
            Initialize();
        }

        public UnitTest.SBSWebService.PorscheCredentials GetCrendentials(string userPorcheGateway, string passPorcheGateway, string userSbs, string passSbs)
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
            porscheCredentials.ClientInfor.ClientIp = "Svr-06";
            porscheCredentials.ClientInfor.ClientName = "giangum";
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
            CustomerCollection customerCollection = VfsCustomerService.Business.CustomerService.GetCustomerList(CustomerColumns.CustomerId, "DESC");
            SendRelatedNew(customerCollection);
        }

        public void SendRelatedNew(VfsCustomerService.Entities.Customer customer)
        {
            var service = new SBSGatewaySoapClient();

            if(customer.CustomerId == "094P848586") return;

            service.Endpoint.Address = new System.ServiceModel.EndpointAddress("http://Svr-06/SBSGateway/SBSGateway.asmx");
            PorscheCustomer porscheCustomer = new PorscheCustomer();
            porscheCustomer.CustomerId = customer.CustomerId;
            
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
                        stock_NewCollection stockNewsCollection = stock_NewService.Getstock_NewList(obj.StockCode, customer.CustomerId, stock_NewColumns.NewsID, "DESC", 1, 3, out total);
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
                        VfsCustomerService.Business.ImportService.CreateMessage(customer, content, contentSms, contentTemplate, contentTemplateSms);

                        foreach (Vfs.WebCrawler.Destination.Entities.RelatedMessagelog relatedMessagelog in relatedMessagelogColl)
                        {
                            Vfs.WebCrawler.Destination.Business.RelatedMessagelogService.CreateRelatedMessagelog(relatedMessagelog);
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

            service.Endpoint.Address = new System.ServiceModel.EndpointAddress("http://Svr-06/SBSGateway/SBSGateway.asmx");
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
                    stock_NewCollection stockNewsCollection = new stock_NewCollection();
                    Vfs.WebCrawler.Destination.Entities.RelatedMessagelog relatedMessagelog;
                    Vfs.WebCrawler.Destination.Entities.RelatedMessagelogCollection relatedMessagelogColl = new Vfs.WebCrawler.Destination.Entities.RelatedMessagelogCollection();
                    customerInquiryData = customerData.StockItems;
                    string content = string.Empty;
                    string contentSms = string.Empty;
                    int templateID = Convert.ToInt32(ConfigurationManager.AppSettings["RelatedMessageTemplateID"]);
                    int templateIDSms = Convert.ToInt32(ConfigurationManager.AppSettings["RelatedSmsMessageTemplateID"]);
                    int smsCount = 0;
                    int total;
                    Boolean sentYn = false;
                    foreach (CustomerStockInquiryObject obj in customerInquiryData)
                    {
                        stockNewsCollection = stock_NewService.Getstock_NewList(obj.StockCode, "094C000006", stock_NewColumns.NewsID, "DESC", 1, 3, out total);
                        if (stockNewsCollection.Count > 0) sentYn = true;
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
                            relatedMessagelog = new Vfs.WebCrawler.Destination.Entities.RelatedMessagelog();
                            relatedMessagelog.NewsID = stock_new.NewsID;
                            relatedMessagelogColl.Add(relatedMessagelog);
                        }                       
                    }

                    //create messsages to be sent
                    if (sentYn)
                    {
                        ContentTemplate contentTemplate = ContentTemplateService.GetContentTemplate(templateID);
                        ContentTemplate contentTemplateSms = ContentTemplateService.GetContentTemplate(templateIDSms);
                        VfsCustomerService.Entities.Customer customer = VfsCustomerService.Business.CustomerService.GetCustomer("094C000006");
                        VfsCustomerService.Business.ImportService.CreateMessage(customer, content, contentSms, contentTemplate, contentTemplateSms);

                        foreach (Vfs.WebCrawler.Destination.Entities.RelatedMessagelog relatedMsglog in relatedMessagelogColl)
                        {
                            Vfs.WebCrawler.Destination.Business.RelatedMessagelogService.CreateRelatedMessagelog(relatedMsglog);
                        }
                    }
                }
            }
        }
    }
}
