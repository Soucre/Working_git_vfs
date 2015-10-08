using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Core;
using Core.Domain.Repositories;
using Core.Domain.Model;
using System.Threading;
using System.Configuration;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
               

        protected override void OnStart(string[] args)
        {
            int intervalMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["IntervalMinutes"].ToString());
            string FileOutPut = ConfigurationManager.AppSettings["FileOutPut"].ToString();
            ////string userName = ConfigurationManager.AppSettings["SmsUserName"];
            ////string password = ConfigurationManager.AppSettings["SmsPassword"];
            while (true)
            {
                try
                {

                    //SendSMS sendSMS = new SendSMS(userName, password);
                    //result = sendSMS.Send("0909070481", "test gui tin nhan sinh nhat");



                    #region test output

                    IRightExecRepository<RightExec> rpo = new RightExecRepository();
                    IList<RightExec> listRightExec = rpo.getRightExecListFromStock("KLS");



                    //TextWriter tw = new StreamWriter(FileOutPut);
                    //tw.WriteLine(result == 1 ? "Gui thanh cong" : "gui khong thanh cong" + "\n");
                    //tw.Close();

                    Ultility.LogFile("----- Begin Sending SMS -----", FileOutPut);
                    //Ultility.Info(DateTime.Now.ToLongTimeString());

                    #endregion

                    Thread.Sleep(intervalMinutes * 1000 * 30); // start continue after intervalMinutes
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected override void OnStop()
        {
        }
    }
}
