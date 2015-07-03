using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Configuration;

namespace VfsSMSEmailServiceV2
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

                    Thread.Sleep(intervalMinutes * 1000 * 60);
                }
                catch (Exception ex)
                {
                    VfsSMSEmailServiceV2.Ultility.Error(ex);
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
}
