using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using Core.Domain.Model;
using Core.Domain.Repositories;
using System.Threading;
using System.Configuration;
using Bussiness;

namespace SMS
{
    public class Information : MainThread
    {
        StringBuilder ss = new StringBuilder();
        int dem = 0;
        public override void Execute()
        {
            int intervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"].ToString());
            string FileOutPut = ConfigurationManager.AppSettings["FileOutPut"].ToString();
            //string userName = ConfigurationManager.AppSettings["SmsUserName"];
            //string password = ConfigurationManager.AppSettings["SmsPassword"];
            //log4net.Config.XmlConfigurator.Configure();
            string userName = ConfigurationManager.AppSettings["SmsUserName"];
            string password = ConfigurationManager.AppSettings["SmsPassword"];
            SendSMS sendSMS = new SendSMS(userName, password);
            
            while (true)
            {
                try
                {
                    if (Ultility.CheckConnectionSQL())
                    {
                        Ultility.syncSentCash(sendSMS);
                        Ultility.LogFile("-----------", FileOutPut);
                        Thread.Sleep(intervalMinutes * 1000 * 60); // start continue after intervalMinutes
                    }
                    else
                    {
                        Thread.Sleep(2 * 1000 * 60); // nếu không connect được SQL thì nghỉ 2 phút, sau đó connect lại
                    }
                    
                }
                catch (Exception ex)
                {
                    Ultility.LogFile("----- Begin Sending SMS ----- Stop services: " + ex.Message, FileOutPut);
                    break;
                }
            }
        }

    }
}
