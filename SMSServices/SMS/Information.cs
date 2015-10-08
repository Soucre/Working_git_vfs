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
            string userName = ConfigurationManager.AppSettings["SmsUserName"];
            string password = ConfigurationManager.AppSettings["SmsPassword"];
            //log4net.Config.XmlConfigurator.Configure();
            int result = 1;
            while (true)
            {
                try
                {

                   

                    //get teamplate
                    IRepository<VFS_Template> rpteamplate = new VFS_TemplateRepository();
                    VFS_Template getTemplate = rpteamplate.GetById(1);
                    //end

                    #region test output

                    IVFS_CustomerRepository<VFS_Customer> customerlistBOB = new VFS_CustomerRepository();
                    IList<VFS_Customer> list = customerlistBOB.getListBirthday(DateTime.Now);

                    foreach (var item in list)
                    {
                        SendSMS sendSMS = new SendSMS(userName, password);
                        result = sendSMS.SendSPAM(item.Mobile, getTemplate.Content.Replace("{ten}",item.CustomerName));
                        
                        Ultility.LogFile("----- Begin Sending SMS ----- Da gui chuc mung sinh nhat " + item.CustomerId + " kq " + result, FileOutPut);
                    }


                    

                    //Ultility.LogFile("----- Begin Sending SMS ----- Da gui chuc mung sinh nhat", FileOutPut);
                    

                    #endregion

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
