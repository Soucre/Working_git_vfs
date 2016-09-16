using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubmitToApi
{
    public class AppHelper
    {
        public static string DomainName
        {
            get { return ConfigurationManager.AppSettings["DomainName"]; }
        }

        public static string SubDomain
        {
            get { return ConfigurationManager.AppSettings["SubDomain"]; }
        }

        public static string Com_code
        {
            get { return ConfigurationManager.AppSettings["Com_code"]; }
        }

        public static string IdLogin
        {
            get { return ConfigurationManager.AppSettings["IdLogin"]; }
        }

        public static string Password
        {
            get { return ConfigurationManager.AppSettings["Password"]; }
        }

        public static string SelHour
        {
            get { return ConfigurationManager.AppSettings["SelHour"]; }
        }

        public static string SelMin
        {
            get { return new Random().Next(25, 30).ToString(); }
        }

        public static string SelAm
        {
            get { return ConfigurationManager.AppSettings["SelAm"]; }
        }
   
    }
}
