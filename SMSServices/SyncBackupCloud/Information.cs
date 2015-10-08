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
            
            while (true)
            {
                try
                {

                    Ultility.syncBalanceHistCluod();
                    Ultility.LogFile("----- Begin Sending SMS ----- so du tien ", FileOutPut);
                    Ultility.syncBalanceStockCluod();
                    Ultility.LogFile("----- Begin Sending SMS ----- so du chung khoan ", FileOutPut);

                    Ultility.syncBalanceMarginCluod();
                    Ultility.LogFile("----- Begin Sending SMS ----- so du no, mua quyen, ung truoc", FileOutPut);
                    Ultility.syncTradingResultHistCluod();
                    Ultility.LogFile("----- Begin Sending SMS ----- giao dich 3 ngay gan nhat ", FileOutPut);
                                     

                    Thread.Sleep(intervalMinutes * 1000 * 60); // start continue after intervalMinutes
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
