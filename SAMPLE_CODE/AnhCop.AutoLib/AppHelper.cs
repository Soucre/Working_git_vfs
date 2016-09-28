using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhCop.AutoLib
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

        public static string SelMin
        {
            get { return DateTime.Now.Minute.ToString(); }
        }


        public static string InSelHour
        {
            get { return ConfigurationManager.AppSettings["InSelHour"]; }
        }

        /// <summary>
        /// InSelAm
        /// AM: Morning, 
        /// PM: Afternoon
        /// </summary>
        public static string InSelAm
        {
            get { return ConfigurationManager.AppSettings["InSelAm"]; }
        }

        public static string ClockStatus
        {
            get { return ConfigurationManager.AppSettings["ClockStatus"]; }
        }

        public static string OutSelHour
        {
            get { return ConfigurationManager.AppSettings["OutSelHour"]; }
        }

        /// <summary>
        /// InSelAm
        /// AM: Morning, 
        /// PM: Afternoon
        /// </summary>
        public static string OutSelAm
        {
            get { return ConfigurationManager.AppSettings["InSelAm"]; }
        }

    }
}
