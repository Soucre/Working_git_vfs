using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VfsLookup.Libs
{
    public class StockProcessBase
    {
        public StockProcessBase(string MaTK,string Type)
        {
            this.MaTK = MaTK;
            this.Type = Type;
            if (this.Type == "Y")
            {

                this.MaTK = MaTK.Replace(MaTK.Substring(0, 4), MaTK.Substring(0, 3) + "%");
            }
        }
        protected String MaTK = "094K002555";
        protected DateTime _TN;
        protected bool nowtimetn = false;
        protected bool nowtimedn = false;
        public DateTime TN
        {
            get { return _TN; }
            set
            {
                _TN = value;
                DateTime prebtn = _TN;
                DateTime nextbtn = _TN;
                DateTime curbtn = _TN;
                if (VSDateTime.isSameDay(curbtn, DateTime.Now))
                {
                    nowtimetn = true;
                }
                while (!VSDateTime.isBusinessDay(curbtn)) { curbtn = curbtn.AddDays(-1); }
                if (!VSDateTime.isBusinessDay(prebtn))
                {
                    prebtn = VSDateTime.getPreBusinessDay(prebtn, 1);
                }
                if (!VSDateTime.isBusinessDay(nextbtn))
                {
                    nextbtn = VSDateTime.getNextBusinessDay(nextbtn, 1);
                }
                tn = String.Format("{0:yyyyMMdd}", curbtn);
                tn00 = String.Format("{0:yyyyMMdd}", curbtn);
                tn2 = String.Format("{0:yyyyMMdd}", curbtn);
                tn_No = String.Format("{0:yyyyMMdd}", curbtn);
                tn1 = String.Format("{0:yyyyMMdd}", VSDateTime.getPreBusinessDay(curbtn, 1));
                tn0 = String.Format("{0:yyyyMMdd}", VSDateTime.getPreBusinessDay(curbtn, 2));

                tn01 = String.Format("{0:yyyyMMdd}", VSDateTime.getNextBusinessDay(curbtn, 1));
                tn02 = String.Format("{0:yyyyMMdd}", VSDateTime.getNextBusinessDay(curbtn, 2));
                tn03 = String.Format("{0:yyyyMMdd}", VSDateTime.getNextBusinessDay(curbtn, 3));
                DateTime tn1date=VSDateTime.getNextBusinessDay(curbtn, 1);
                tn1_TienHienCo = String.Format("{0:yyyy-MM-dd}", tn1date) + " 00:00:00";
                if (tn1date > DateTime.Now)
                {
                    tn1_TienHienCo=String.Format("{0:yyyy-MM-dd}", VSDateTime.getPreBusinessDay(tn1date, 1)) + " 00:00:00";
                }
                tn_TienHienCo = String.Format("{0:yyyy-MM-dd}", curbtn) + " 00:00:00";

            }
        }
        DateTime _DN;

        public DateTime DN
        {
            get { return _DN; }
            set
            {
                _DN = value;
                DateTime prebDn = _DN;
                DateTime nextbDn = _DN;
                DateTime curbdn = _DN;
                if (VSDateTime.isSameDay(curbdn, DateTime.Now))
                {
                    nowtimedn = true;
                }
                while (!VSDateTime.isBusinessDay(curbdn)) { curbdn = curbdn.AddDays(-1); }
                if (!VSDateTime.isBusinessDay(prebDn))
                {
                    prebDn = VSDateTime.getPreBusinessDay(prebDn, 1);
                }
                if (!VSDateTime.isBusinessDay(nextbDn))
                {
                    nextbDn = VSDateTime.getNextBusinessDay(nextbDn, 1);
                }
                dn = String.Format("{0:yyyyMMdd}", curbdn);
                dn00 = String.Format("{0:yyyyMMdd}", curbdn);
                dn2 = String.Format("{0:yyyyMMdd}", curbdn); ;
                dn_No = String.Format("{0:yyyyMMdd}", curbdn); ;
                dn1 = String.Format("{0:yyyyMMdd}", VSDateTime.getPreBusinessDay(curbdn, 1));
                dn0 = String.Format("{0:yyyyMMdd}", VSDateTime.getPreBusinessDay(curbdn, 2));

                dn01 = String.Format("{0:yyyyMMdd}", VSDateTime.getNextBusinessDay(curbdn, 1));
                dn02 = String.Format("{0:yyyyMMdd}", VSDateTime.getNextBusinessDay(curbdn, 2));
                dn03 = String.Format("{0:yyyyMMdd}", VSDateTime.getNextBusinessDay(curbdn, 3));
                DateTime dn1date = VSDateTime.getNextBusinessDay(curbdn, 1);
                dn1_TienHienCo = String.Format("{0:yyyy-MM-dd}", dn1date) + " 00:00:00";
                if (dn1date > DateTime.Now)
                {
                    dn1_TienHienCo = String.Format("{0:yyyy-MM-dd}", VSDateTime.getPreBusinessDay(dn1date, 1)) + " 00:00:00";
                }
                dn_TienHienCo = String.Format("{0:yyyy-MM-dd}", curbdn) + " 00:00:00";
            }
        }
        protected string tn_No = "";
        protected string dn_No = "";
        protected string tn = "";
        protected string dn = "";
        //string dn = "";
        //string tn = "";
        //'ngay t-2, t-1, t
        protected string tn0 = "";
        protected string tn1 = "";
        protected string tn2 = "";
        protected string dn0 = "";
        protected string dn1 = "";
        protected string dn2 = "";

        //'ngay t, t+1, t+2

        protected string tn00 = "";
        protected string tn01 = "";
        protected string tn02 = "";
        protected string tn03 = "";

        protected string dn00
        {
            get { return dn; }
            set { dn = value; }
        }
        protected string dn01 = "";
        protected string dn02 = "";
        protected string dn03 = "";
        protected string tn1_TienHienCo = "";
        protected string dn1_TienHienCo = "";
        protected string tn_TienHienCo = "";
        protected string dn_TienHienCo = "";
        private string Type="N";
    }
}