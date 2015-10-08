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
            int TimeStart = Convert.ToInt32(ConfigurationManager.AppSettings["TimeStart"].ToString());
            string FileOutPut = ConfigurationManager.AppSettings["FileOutPut"].ToString();
            //string userName = ConfigurationManager.AppSettings["SmsUserName"];
            //string password = ConfigurationManager.AppSettings["SmsPassword"];
            //log4net.Config.XmlConfigurator.Configure();
            
            while (true)
            {
                try
                {

                    Ultility.LaiLo();
                    /*bool checkConnection = Ultility.CheckConnectionSQL();
                    if (Ultility.CheckConnectionSQL())
                    {
                        if (DateTime.Now.Hour == TimeStart)
                        {
                            Ultility.LogFile("----- test ne-------- ", FileOutPut);
                            
                        }                        
                        Thread.Sleep(1 * 1000 * 10); // nếu hẹn giờ không đúng thì 50 phút sau chạy tiếp                        
                    }
                    else
                    {
                        Thread.Sleep(1 * 1000 * 10); // nếu không connect được SQL thì nghỉ 2 phút, sau đó connect lại
                    } */

                   
                    //Thread.Sleep(intervalMinutes * 1000 * 60); // start continue after intervalMinutes
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
