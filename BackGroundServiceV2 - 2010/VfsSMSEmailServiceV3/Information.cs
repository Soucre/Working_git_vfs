using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using Core.Domain.Model;
using Core.Domain.Repositories;
using System.Threading;
using System.Configuration;
using SyncReport;


namespace SMS
{
    public class Information : MainThread
    {
        StringBuilder ss = new StringBuilder();
        int dem = 0;
        public override void Execute()
        {
            int intervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"].ToString());
            int commandBlockSize = Convert.ToInt32(ConfigurationManager.AppSettings["numberOfItem"]);
            string userName = ConfigurationManager.AppSettings["SmsUserName"];
            string password = ConfigurationManager.AppSettings["SmsPassword"];
            log4net.Config.XmlConfigurator.Configure();
            
            while (true)
            {
                try
                {

                    SendEmailSession sendEmailSession = new SendEmailSession(commandBlockSize);
                    SendSMSSession smsSession = new SendSMSSession(commandBlockSize, userName, password);

                    //Ultility.Info("----- Begin Sending email -----");
                    //Ultility.Info(DateTime.Now.ToLongTimeString());
                    //sendEmailSession.SendAllMailNotStart();
                    //Ultility.Info("----- Execution complete-----");
                    //Ultility.Info(DateTime.Now.ToLongTimeString());
                    //Ultility.Info("----- End of sending email-----");

                    Ultility.Info("----- Begin Sending SMS -----");
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    smsSession.SendAllSMSNotStart();
                    Ultility.Info("----- Execution complete-----");
                    Ultility.Info(DateTime.Now.ToLongTimeString());
                    Ultility.Info("----- End of sending SMS-----");        
                                     

                    Thread.Sleep(intervalMinutes * 1000 * 60); // start continue after intervalMinutes
                }
                catch (Exception ex)
                {
                    Ultility.Error(ex);

                }
            }
        }

    }
}
