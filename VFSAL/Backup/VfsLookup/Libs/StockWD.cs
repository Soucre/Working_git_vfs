using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace VfsLookup.Libs
{
    public class StockWD:StockProcessBase
    {
        public StockWD(String MaTk,String type) : base(MaTk,type) { }
        /*
        public StockWD(string MaTK)
        {
            this.MaTK = MaTK;
        }
        String MaTK = "094K002555";
        DateTime _TN;

        public DateTime TN
        {
            get { return _TN; }
            set
            {
                _TN = value;
                tn = String.Format("{0:yyyymmdd}", _TN);
                tn00 = tn;
                tn2 = tn;
                tn_No = tn;
                tn1 = String.Format("{0:yyyymmdd}", VSDateTime.getPreBusinessDay(_TN, 1));
                tn0 = String.Format("{0:yyyymmdd}", VSDateTime.getPreBusinessDay(_TN, 2));

                tn01 = String.Format("{0:yyyymmdd}", VSDateTime.getNextBusinessDay(_TN, 1));
                tn02 = String.Format("{0:yyyymmdd}", VSDateTime.getNextBusinessDay(_TN, 2));
                tn1_TienHienCo = String.Format("{0:yyyy-mm-dd hh:mm:ss}", VSDateTime.getNextBusinessDay(_TN, 1));
                tn_TienHienCo = String.Format("{0:yyyy-mm-dd hh:mm:ss}", _TN);

            }
        }
        DateTime _DN;

        public DateTime DN
        {
            get { return _DN; }
            set
            {
                _DN = value;
                dn = String.Format("{0:yyyymmdd}", _DN);
                dn00 = dn;
                dn2 = dn;
                dn_No = dn;
                dn1 = String.Format("{0:yyyymmdd}", VSDateTime.getPreBusinessDay(_DN, 1));
                dn0 = String.Format("{0:yyyymmdd}", VSDateTime.getPreBusinessDay(_DN, 2));

                dn01 = String.Format("{0:yyyymmdd}", VSDateTime.getNextBusinessDay(_DN, 1));
                dn02 = String.Format("{0:yyyymmdd}", VSDateTime.getNextBusinessDay(_DN, 2));
                dn1_TienHienCo = String.Format("{0:yyyy-mm-dd hh:mm:ss}", VSDateTime.getNextBusinessDay(_DN, 1));
                dn_TienHienCo = String.Format("{0:yyyy-mm-dd hh:mm:ss}", _DN);
            }
        }
        string tn_No = "";
        string dn_No = "";
        string tn = "";
        string dn = "";
        //string dn = "";
        //string tn = "";
        //'ngay t-2, t-1, t
        string tn0 = "";
        string tn1 = "";
        string tn2 = "";
        string dn0 = "";
        string dn1 = "";
        string dn2 = "";

        //'ngay t, t+1, t+2

        string tn00 = "";
        string tn01 = "";
        string tn02 = "";
        string tn03 = "";

        string dn00
        {
            get { return dn; }
            set { dn = value; }
        }
        string dn01 = "";
        string dn02 = "";
        string dn03 = "";
        string tn1_TienHienCo = "";
        string dn1_TienHienCo = "";
        string tn_TienHienCo = "";
        string dn_TienHienCo = "";
        */
        
            DataTable rnTienHienCotn;

        public DataTable RnTienHienCotn
        {
            get {
                string StSQL_TienHienCo_tn = "SELECT CurrentBalance AS PreviousBalance  FROM   BalanceHist WHERE  BranchCode = '100' AND BankGL like '324%' AND SectionGL = '3241' AND AccountID like '" + MaTK + "' AND TransactionDate = dbo.fc_GetPreTransDate('" + tn1_TienHienCo + "', '100')";
                rnTienHienCotn = VSDBConnection.getDataTable(StSQL_TienHienCo_tn, VSDBConnection.CSVSFServices);
                return rnTienHienCotn;
            }
            
        }
        DataTable rnTienHienCodn;

        public DataTable RnTienHienCodn
        {
            get {
                string StSQL_TienHienCo_dn = "SELECT CurrentBalance AS PreviousBalance  FROM   BalanceHist WHERE  BranchCode = '100' AND BankGL like '324%' AND SectionGL = '3241' AND AccountID like '%" + MaTK + "' AND TransactionDate = dbo.fc_GetPreTransDate('" + dn1_TienHienCo + "', '100')";
                rnTienHienCodn = VSDBConnection.getDataTable(StSQL_TienHienCo_dn, VSDBConnection.CSVSFServices);
                return rnTienHienCodn; 
            
            }
        }
        
    }
}