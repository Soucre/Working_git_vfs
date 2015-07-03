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
	public class SendPorfolioSmsMessageSession : SendMessage
	{
		protected string endPointAddress;
        protected int templateID;
        protected int templateIDSms;
        protected PorscheCredentials porscheCredentials;
		protected int porfolioSmsMessageTemplateID;

        public SendPorfolioSmsMessageSession(int commandBlockSize) : base(commandBlockSize)
        {
            Initialize();                       
        }

        protected override void Initialize()
        {
            base.Initialize();            
            endPointAddress = ConfigurationManager.AppSettings["EndpointAddress"];
           
           
			porfolioSmsMessageTemplateID = Convert.ToInt32(ConfigurationManager.AppSettings["PorfolioSmsMessageTemplateID"]);
            porscheCredentials = Ultility.GetCrendentials("SBSSystem", "VSSD", "vssd", "123");
        }

		public SendPorfolioSmsMessageSession(int commandBlockSize, string userName, string password)
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

		public void SendPorfolioSmsMessage()
        {
            Int32 totalRows = 0;
            //CustomerCollection customerCollection = VfsCustomerService.Business.CustomerService.GetCustomerList(CustomerColumns.CustomerId, "DESC");            
            CustomerCollection customerCollection = VfsCustomerService.Business.CustomerService.GetCustomerListSearch(1, "ALL", "ALL", "ALL", "ALL", "ALL", CustomerColumns.CustomerId, "ASC", 0, 0, out totalRows);
			SendPorfolioSmsMessage(customerCollection);
        }

        public void SendPorfolioSmsMessage(VfsCustomerService.Entities.Customer customer)
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
					ExtensionMessageCollection extensionMessageCollection = new ExtensionMessageCollection();
                    ExtensionMessageLog relatedMessagelog;
					ExtensionMessageLogCollection relatedMessagelogColl = new ExtensionMessageLogCollection();
                    customerInquiryData = customerData.StockItems;
                    StringBuilder content = new StringBuilder();
                    string contentSms = string.Empty;
                   
                    int smsCount = 0;
                    int total;
                    Boolean sentYn = false;
                    smsCount = 0;
                   
                    foreach (CustomerStockInquiryObject obj in customerInquiryData)
                    {
                        Ultility.Info(customer.CustomerId + ": " + obj.StockCode);
						extensionMessageCollection = ExtensionMessageService.GetExtensionMessageListByTitle(obj.StockCode, customer.CustomerId, ExtensionMessageColumns.ExtensionMessageID, "DESC", 1, 5, out total);
						if (extensionMessageCollection.Count > 0) sentYn = true;

						foreach (ExtensionMessage extMessage in extensionMessageCollection)
                        {                           
                            if (smsCount == 0)
                            {
                                if (contentSms == string.Empty)
									contentSms +=  Ultility.RemoveSound(extMessage.Content.Normalize()) ;
                                else
									contentSms += ";" + Ultility.RemoveSound(extMessage.Content.Normalize()) + "";
                            }
                            smsCount++;
                            relatedMessagelog = new ExtensionMessageLog();
							relatedMessagelog.ExtensionMessageID = extMessage.ExtensionMessageID;
                            relatedMessagelog.CustomerId = customer.CustomerId;
							
                            relatedMessagelogColl.Add(relatedMessagelog);
                        }                       
                    }
                  
                    //create messsages to be sent
                    if (sentYn)
                    {
                        //ContentTemplate contentTemplate = ContentTemplateService.GetContentTemplate(templateID);
						ContentTemplate contentTemplateSms = ContentTemplateService.GetContentTemplate(porfolioSmsMessageTemplateID);                        
                        VfsCustomerService.Business.ImportService.CreateMessage(customer,  contentSms, contentTemplateSms);

						foreach (ExtensionMessageLog relatedMsglog in relatedMessagelogColl)
                        {
							ExtensionMessageLogService.CreateExtensionMessageLog(relatedMsglog);
                        }
                    }
                }
            }
        }

		public void SendPorfolioSmsMessage(VfsCustomerService.Entities.CustomerCollection customers)
        {
            foreach (VfsCustomerService.Entities.Customer cust in customers)
            {
				SendPorfolioSmsMessage(cust);
            }
        }    
	}
}
