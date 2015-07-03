using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Configuration;


namespace VfsCustomerInformationServices
{
    public class Information: MainThread
    {
        public override void Execute()
        {
            int intervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"].ToString());
            int commandBlockSize = Convert.ToInt32(ConfigurationManager.AppSettings["numberOfItem"]);
            string userName = ConfigurationManager.AppSettings["SmsUserName"];
            string password = ConfigurationManager.AppSettings["SmsPassword"];
            log4net.Config.XmlConfigurator.Configure();

            FeedHoseInfo feedHoseInfo = new FeedHoseInfo();
            FeedHnxInfo feedHnxInfo = new FeedHnxInfo();            
            FeedVsdInfo feedVsdInfo = new FeedVsdInfo();
            FeedCafeFInfo feedCafeFInfo = new FeedCafeFInfo();
            FeedStoxInfo feedStoxInfo = new FeedStoxInfo();

            while (true)
            {
                try
                {
                    SendEmailSession sendEmailSession = new SendEmailSession(commandBlockSize);
                    SendSMSSession smsSession = new SendSMSSession(commandBlockSize, userName, password);

                    if (DateTime.Now.Hour > 7 && DateTime.Now.Hour <= 21)
                    {
                        Ultility.Info("-----Begin sending related stock information-----");
                        SendStockRelatedNewsSession sendStockRelatedNewsSession = new SendStockRelatedNewsSession(commandBlockSize);
                        sendStockRelatedNewsSession.SendRelatedNew(); // gửi tin nhắn và Email danh mục đầu tư
                        Ultility.Info("-----End of sending related stock information-----");

                        Ultility.Info("-----Begin sending porfolio stock information-----");
                        SendPorfolioSmsMessageSession sendPorfolioSmsMessageSession = new SendPorfolioSmsMessageSession(commandBlockSize);
                        sendPorfolioSmsMessageSession.SendPorfolioSmsMessage(); // gửi tin nhắn khuyến nghị mua của Thương
                        Ultility.Info("-----End of sending porfolio stock information-----"); 
                        
                    }

                    /*   Ultility.Info("----- Begin Feed Hose information -----"); // lấy dữ liệu từ HOSE de vao website
                       Ultility.Info(DateTime.Now.ToLongTimeString());
                       feedHoseInfo.PerformFeed();
                       Ultility.Info(DateTime.Now.ToLongTimeString());
                       Ultility.Info("----- End of feeding Hose information -----");*/


                    /*
                                        Ultility.Info("----- Begin Feed Hnx information -----");// lấy dữ liệu từ HNX
                                        Ultility.Info(DateTime.Now.ToLongTimeString());
                                        feedHnxInfo.PerformFeed();
                                        Ultility.Info(DateTime.Now.ToLongTimeString());
                                        Ultility.Info("----- End of feeding Hnx information -----");					                  
                    */
                    /*
                    Ultility.Info("----- Begin Feed stox information -----");
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    feedStoxInfo.PerformFeed();
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    Ultility.Info("----- End of feeding stox information -----");

					Ultility.Info("----- Begin Feed Cafef information -----");
					Ultility.Info(DateTime.Now.ToLongTimeString());
					feedCafeFInfo.PerformFeed();
					Ultility.Info(DateTime.Now.ToLongTimeString());
					Ultility.Info("----- End of feeding Cafef information -----");
                    */
                    /*Ultility.Info("----- Begin Feed VnEconomy information -----");
                    /Ultility.Info(DateTime.Now.ToLongTimeString());

                    informationFeedSession.FeedVnEconomyNews();

                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    Ultility.Info("----- End of feeding VnEconomy information -----");*/
                   
                    //Ultility.Info("----- Begin Feed VSD information -----");
                    //Ultility.Info(DateTime.Now.ToLongTimeString());
                    //feedVsdInfo.PerformFeed();
                    //Ultility.Info(DateTime.Now.ToLongTimeString());
                    //Ultility.Info("----- End of feeding VSD information -----");
                    Thread.Sleep(intervalMinutes * 1000 * 60);
                }
                catch (Exception ex)
                {
                    VfsCustomerInformationServices.Ultility.Error(ex);
                }
            }
        }


        private void LogReport(Information information)
        {
            //StringBuilder result = new StringBuilder();
            //string dateTimeFormatString = "yyyy/MM/dd HH:mm:ss";

            //if (sendEmailSession.FinishSendEmailTime == DateTime.MaxValue)
            //{
            //    result.AppendFormat("SENDING EMAIL FAIL");
            //    for (int i = 0; i < sendEmailSession.ListError.Count; i++)
            //    {
            //        result.Append("\r\n");
            //        result.Append("Error: " + sendEmailSession.ListError[i].Message);
            //    }
            //    Logger.Error(result);
            //}
            //else
            //{
            //    if (sendEmailSession.ListError.Count == 0)
            //    {
            //        result.AppendFormat("SENDING EMAIL SUCCESS");
            //    }
            //    else
            //    {
            //        result.AppendFormat("SENDING EMAIL SUCCESS WITH {0} ERROR(S)", sendEmailSession.ListError.Count);

            //        SendEmailException sendEmailException = new SendEmailException();
            //        string errorMessage = "";
            //        for (int i = 0; i < sendEmailSession.ListError.Count; i++)
            //        {
            //            sendEmailException = (SendEmailException)sendEmailSession.ListError[i];
            //            errorMessage = string.Format("Error when sending email from: {0}; to: {1}; title: {2}", sendEmailException.EmailException.SenderEmail, sendEmailException.EmailException.ReceiverEmail, sendEmailException.EmailException.Subject);
            //            result.Append("\r\n");
            //            result.Append(errorMessage);
            //        }
            //    }

            //    result.Append("\r\n")
            //        .AppendFormat("    Start at: {0}", sendEmailSession.StartSendEmailTime.ToString(dateTimeFormatString))
            //        .AppendFormat("    Finish at: {0}", sendEmailSession.FinishSendEmailTime.ToString(dateTimeFormatString))
            //        .AppendFormat("    Number of emails: {0}", sendEmailSession.NumberOfEmails);
            //    Logger.Info(result);
            //}
        }
    }

    public class FeedHoseInfo : FeedUnit
    {
        public FeedHoseInfo() { }

        public void PerformFeed()
        {
            InformationFeedSession = new InformationFeedHoseSession(10);
            InformationFeedSession.FeedNewsList();
        }
    }

    public class FeedHnxInfo : FeedUnit
    {
        public FeedHnxInfo() { }

        public void PerformFeed()
        {
            InformationFeedSession = new InformationFeedHnxSession(10);
            InformationFeedSession.FeedNewsList();
        }
    }    

    public class FeedVsdInfo : FeedUnit
    {
        public FeedVsdInfo() { }

        public void PerformFeed()
        {
            InformationFeedSession = new InformationFeedVsdSession(10);
            InformationFeedSession.FeedNewsList();
        }
    }

    public class FeedCafeFInfo : FeedUnit
    {
        public FeedCafeFInfo() { }

        public void PerformFeed()
        {
            InformationFeedSession = new InformationFeedCafeFSession(10);
            InformationFeedSession.FeedNewsList();
        }
    }

    public class FeedStoxInfo : FeedUnit
    {
        public FeedStoxInfo() { }

        public void PerformFeed()
        {
            InformationFeedSession = new InformationFeedStoxSession(10);
            InformationFeedSession.FeedNewsList();
        }
    }
}
