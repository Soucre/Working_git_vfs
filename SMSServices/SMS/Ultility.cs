using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SMS
{
    public static class Ultility
    {
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
