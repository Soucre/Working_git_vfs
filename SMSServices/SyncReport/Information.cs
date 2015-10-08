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

                    //IRepository<VFS_Report_LaiLo_Customer> rp = new VFS_Report_LaiLo_CustomerRepository();
                    //var fda = rp.GetAll();
                    //IMAccDetailLogRepository<MAccDetailLog> repo = new MAccDetailLogRepository();
                    //IList<String> listCustomer = repo.getListAllCutomer();

                    Ultility.syncBalanceNoKyQuy();
                    Ultility.LogFile("----- Begin Sending SMS ----- Sync No ky quy ", FileOutPut);
                    Ultility.syncQuyen();
                    Ultility.LogFile("----- Begin Sending SMS ----- Sync Quyen ", FileOutPut);
                                                           
                    //Ultility.LogFile("----- Begin Sending SMS ----- Da gui chuc mung sinh nhat", FileOutPut);

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
