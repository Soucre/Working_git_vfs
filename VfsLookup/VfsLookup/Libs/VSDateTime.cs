using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace VfsLookup.Libs
{
    public class VSDateTime
    {
        public static DateTime getNextBusinessDay(DateTime date, int next)
        {
            DateTime result = date.AddDays(0);
            int n = next;
            while(n>0){
                result = result.AddDays(1);
                if (isBusinessDay(result)) n--;
            }
            return result;
        }
        public static DateTime getPreBusinessDay(DateTime date, int pre)
        {
            DateTime result = date.AddDays(0);
            int n = pre;
            while (n > 0)
            {
                result = result.AddDays(-1);
                if (isBusinessDay(result)) n--;
            }
            return result;
        }
        public static bool isBusinessDay(DateTime date)
        {

            if (Holidays.IndexOf(date) == -1) return true;
            return false;
        }
        public static bool isSameDay( DateTime datetime1, DateTime datetime2)
        {
            return datetime1.Year == datetime2.Year
                && datetime1.Month == datetime2.Month
                && datetime1.Day == datetime2.Day;
        }
        private static List<DateTime> holidays=new List<DateTime>();
        //private static DateTime maxdate=new DateTime(1970,1,1);
        //private static DateTime mindate = new DateTime(1970, 1, 1);
        public static List<DateTime> Holidays
        {
            get {
                if (VSDateTime.holidays.Count==0)
                {
                    string query = "select * from NoTransDate where NoTransDate>'" + String.Format("{0:yyyy-MM-dd}", new DateTime(2010,1,1)) + " 00:00:00" + "' and NoTransDate<'" + String.Format("{0:yyyy-MM-dd}", DateTime.Now)+" 00:00:00" + "'";
                    DataTable tbl=VSDBConnection.getDataTable(query,VSDBConnection.CSVSFServices);
                    foreach(DataRow r in tbl.Rows)
                    {
                        DateTime date=DateTime.Parse(r["NoTransDate"].ToString());
                        holidays.Add(date);
                    }
                }

                
                return VSDateTime.holidays; }
            set { VSDateTime.holidays = value; }
        }
       
       
        
    }
}
