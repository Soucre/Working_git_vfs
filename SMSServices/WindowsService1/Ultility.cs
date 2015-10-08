using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace WindowsService1
{
    public static class Ultility
    {
        //private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



        //public static void Info(object message)
        //{
        //    if (log.IsErrorEnabled)
        //    {
        //        log.Debug(message);
        //    }
        //}

        //public static void Info(object message, Exception ex)
        //{
        //    if (log.IsErrorEnabled)
        //    {
        //        log.Info(message, ex);
        //    }
        //}

        //public static void Error(object message)
        //{
        //    if (log.IsErrorEnabled)
        //    {
        //        log.Error(message);
        //    }
        //}

        //public static void Error(object message, Exception ex)
        //{
        //    if (log.IsErrorEnabled)
        //    {
        //        log.Error(message, ex);
        //    }
        //}

        public static string CutAddressHead(string source)
        {
            string s = source;
            if (s.Length <= 160) return s;
            string temp = s.Substring(0, 160);
            int pos = temp.LastIndexOf(" ");
            s = temp.Substring(0, pos);
            return s;
        }

        public static string CutAddressEnd(string source)
        {
            string s = source;
            if (s.Length <= 160) return string.Empty;
            string temp = s.Substring(0, 160);
            int pos = temp.LastIndexOf(" ");
            s = s.Substring(pos + 1, ((s.Length - 1) - pos));
            return s;
        }
        public static void LogFile(string sExceptionName, string directory)
        {

            StreamWriter log;

            if (!File.Exists(directory))
            {

                log = new StreamWriter(directory);

            }

            else
            {

                log = File.AppendText(directory);

            }

            // Write to the file:

            log.WriteLine("Data Time:" + DateTime.Now + "-------" + sExceptionName);



            // Close the stream:

            log.Close();

        }

    }
    
}
