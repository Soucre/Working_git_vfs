using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SyncReport.App_Code
{
    public static class ApplicationHelper
    {      
        public static string movefile
        {
            get
            {
                return ConfigurationManager.AppSettings["movefile"].ToString();
            }
        }
    }
}
