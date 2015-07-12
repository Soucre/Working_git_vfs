using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace VfsLookup.Libs
{
    public class StockTienVaNo
    {
        public StockTienVaNo(string MaTk, string Type)
        {
            this.MaTk = MaTk;
            this.Type = Type;
            if (this.Type == "Y")
            {
                this.MaTk = MaTk.Replace(MaTk.Substring(0, 4), MaTk.Substring(0, 3) + "%");
            }
        }
        DateTime _TN;

        public DateTime TN
        {
            get { return _TN; }
            set { _TN = value;
            DateTime prebtn = _TN;
            DateTime nextbtn = _TN;
            if (!VSDateTime.isBusinessDay(prebtn))
            {
                prebtn = VSDateTime.getPreBusinessDay(prebtn, 1);
            }
            if (!VSDateTime.isBusinessDay(nextbtn))
            {
                nextbtn = VSDateTime.getNextBusinessDay(nextbtn, 1);
            }
            tn = String.Format("{0:yyyyMMdd}", _TN);
            tnt3 = String.Format("{0:yyyyMMdd}", VSDateTime.getPreBusinessDay(prebtn, 3));
            tn_TTDK = String.Format("{0:yyyy-MM-dd}", _TN) + " 00:00:00";
            //tn00 = String.Format("{0:yyyyMMdd}", prebtn);
            //tn2 = String.Format("{0:yyyyMMdd}", prebtn);
            //tn_No = String.Format("{0:yyyyMMdd}", prebtn);
            //tn1 = String.Format("{0:yyyyMMdd}", VSDateTime.getPreBusinessDay(prebtn, 1));
            //tn0 = String.Format("{0:yyyyMMdd}", VSDateTime.getPreBusinessDay(prebtn, 2));

            //tn01 = String.Format("{0:yyyyMMdd}", VSDateTime.getNextBusinessDay(nextbtn, 1));
            //tn02 = String.Format("{0:yyyyMMdd}", VSDateTime.getNextBusinessDay(nextbtn, 2));
            //tn03 = String.Format("{0:yyyyMMdd}", VSDateTime.getNextBusinessDay(nextbtn, 3));
            //tn1_TienHienCo = String.Format("{0:yyyy-MM-dd}", VSDateTime.getNextBusinessDay(nextbtn, 1)) + " 00:00:00";
            //tn_TienHienCo = String.Format("{0:yyyy-MM-dd hh:mm:ss}", prebtn) + " 00:00:00";
            }
        }
        DateTime _DN;

        public DateTime DN
        {
            get { return _DN; }
            set { _DN = value;
                _DN = value;
                DateTime prebDn = _DN;
                DateTime nextbDn = _DN;
                if (!VSDateTime.isBusinessDay(prebDn))
                {
                    prebDn = VSDateTime.getPreBusinessDay(prebDn, 1);
                }
                if (!VSDateTime.isBusinessDay(nextbDn))
                {
                    nextbDn = VSDateTime.getNextBusinessDay(nextbDn, 1);
                }
                dn_TTDK = String.Format("{0:yyyy-MM-dd}", _DN) + " 00:00:00";
                dn=String.Format("{0:yyyyMMdd}", _DN);
                dnt3 = String.Format("{0:yyyyMMdd}", VSDateTime.getPreBusinessDay(prebDn, 3));
                dn0 = String.Format("{0:yyyyMMdd}", VSDateTime.getPreBusinessDay(prebDn, 2));
                dn1 = String.Format("{0:yyyyMMdd}", VSDateTime.getPreBusinessDay(prebDn, 1));
                dn2 = String.Format("{0:yyyyMMdd}", prebDn);
            }
        }
        private string MaTk;
        private string tn_TTDK;
        private string tn;
        private string dn;
        private string dn_TTDK;
        private string tnt3;
        private string dnt3;
        private string dn0;
        private string dn1;
        private string dn2;




        

        DataTable rnTinhTrangTienDauKy;// = Sheet5.Range("I10")

        public DataTable RnTinhTrangTienDauKy
        {
            get {
                string StSQL_TinhTrangTienDauKy = "SELECT CurrentBalance AS PreviousBalance  FROM   BalanceHist WHERE  BranchCode = '100' AND BankGL like '324%' AND SectionGL = '3241' AND AccountID like '" + MaTk + "' AND TransactionDate = dbo.fc_GetPreTransDate('" + tn_TTDK + "', '100')";
                rnTinhTrangTienDauKy = VSDBConnection.getDataTable(StSQL_TinhTrangTienDauKy, VSDBConnection.CSVSFServices);
                return rnTinhTrangTienDauKy; }
        }
        DataTable rnTinhTrangDauKy;// = Sheet6.Range("A1")

        public DataTable RnTinhTrangDauKy
        {
            get {
                string StSQLTinhTrangDauKy = "SELECT TransactionDate, AccountId, Amount, DebitOrCredit, Description Description FROM TransactionHist WHERE Approved != 'X' AND BankGl LIKE '324%' AND BankGl LIKE '324%' And AccountId like '" + MaTk + "' And TransactionDate between '" + tn + "' and '" + dn + "' Order by TransactionDate, Id";
                rnTinhTrangDauKy = VSDBConnection.getDataTable(StSQLTinhTrangDauKy, VSDBConnection.CSVSFServices);
                return rnTinhTrangDauKy; }
        }
        DataTable rnNhanCoTuc;// = Sheet1.Range("F14")

        public DataTable RnNhanCoTuc
        {
            get {
                string StSQLNhanCoTuc = "SELECT ISNULL(SUM([ReceiveValue]),0) - ISNULL(SUM([ReceiveValue]),0)*0.05 NCT FROM [VFS_Rights] WHERE RightType = 'M' and AccountID like '" + MaTk + "' And (Convert(nvarchar(10),DatePay, 112)  Between  '" + tn + "' And  '" + dn + "') ";
                rnNhanCoTuc = VSDBConnection.getDataTable(StSQLNhanCoTuc, VSDBConnection.CSVSFServices);
                
                return rnNhanCoTuc; }
        }
        DataTable rnTienNopVao;// = Sheet1.Range("F18")

        public DataTable RnTienNopVao
        {
            get {
                string StSQLTienNopVao = "SELECT SUM(CreditAmount) as sumcreadit FROM [VFS_Cash] WHERE (LEFT(BankGl,4) = '3241') And (LEFT(CorAccount,3) = '112' or CorAccount IN('3388110005','1362110002','3362110002') or LEFT(CorAccount,3) =  '094' )   AND  (Convert(nvarchar(10),[TransactionDate], 112)  Between  '" + tn + "' And '" + dn + "') AND AccountID like '" + MaTk + "' ";
                rnTienNopVao = VSDBConnection.getDataTable(StSQLTienNopVao, VSDBConnection.CSVSFServices);
                return rnTienNopVao; }
        }
        DataTable rnTienRut;// = Sheet1.Range("F29")

        public DataTable RnTienRut
        {
            get {
                string StSQLTienRut = "SELECT SUM(DebitAmount) as SumDebitAmount FROM [VFS_Cash] WHERE (LEFT(BankGl,4) = '3241') And (LEFT(CorAccount,3) = '112' or CorAccount IN('3388110005','1362110002','3362110002') or LEFT(CorAccount,3) =  '094' )   AND  (Convert(nvarchar(10),[TransactionDate], 112)  Between  '" + tn + "' And '" + dn + "') AND AccountID like '" + MaTk + "' ";
                rnTienRut = VSDBConnection.getDataTable(StSQLTienRut, VSDBConnection.CSVSFServices);
                return rnTienRut; }
        }


        DataTable rnTraNoMuaQuyen;// = Sheet7.Range("M3")

        public DataTable RnTraNoMuaQuyen
        {
            get {
                string StSQLTraNoMuaQuyen = "SELECT ContractId, UserCreate,DateContract,  PaymentDate,ContractType, CustomerId, AccountId, AdvanceAmount,AdvanceAmount,AdvanceFee,[Status] FROM dbo.BuyCashContract WHERE AccountId like '" + MaTk + "' And (Convert(nvarchar(10),DateContract, 112)  Between '" + tn + "' and '" + dn + "') and [Status] IN('E','T')";
                rnTraNoMuaQuyen = VSDBConnection.getDataTable(StSQLTraNoMuaQuyen, VSDBConnection.CSVSFServices);
                return rnTraNoMuaQuyen; }
        }
        DataTable rnTraNoUngtruoc;// = Sheet7.Range("A3")

        public DataTable RnTraNoUngtruoc
        {
            get {
                string StSQLTraNoUngTruoc = "SELECT ContractId, UserCreate,DateContract,  PaymentDate,ContractType, CustomerId, AccountId, AdvanceAmount,AdvanceAmount,AdvanceFee,[Status] FROM dbo.AdvanceContractAll WHERE AccountId like '" + MaTk + "' And (Convert(nvarchar(10),DateContract,112)  Between '" + tn + "' and '" + dn + "') And [Status] IN('E','T')";
                rnTraNoUngtruoc = VSDBConnection.getDataTable(StSQLTraNoUngTruoc, VSDBConnection.CSVSFServices);
                return rnTraNoUngtruoc; }
        }
        DataTable rnTraNoKyQuy;// = Sheet7.Range("Y3")

        public DataTable RnTraNoKyQuy
        {
            get {
                string StSQLTraNoKyQuy = "SELECT ContractId, LogUser AS UserCreate, EffectiveOnDate AS DateContract, EffectiveToDate AS PaymentDate,LoanType AS ContractType, CustomerId, AccountId, Amount AS AdvanceAmount,(AmountCalInterest) AS DebtValue,(InterestPayNow) AS InterestValue,[Status] FROM dbo.MAccDetailLog WHERE AccountId like '" + MaTk + "'  And (Convert(nvarchar(10),EffectiveOnDate, 112)  Between '" + tn + "' and '" + dn + "') And [Status] IN ('B') And IsUsed != 'N'";
                rnTraNoKyQuy = VSDBConnection.getDataTable(StSQLTraNoKyQuy, VSDBConnection.CSVSFServices);
                return rnTraNoKyQuy; }
        }

        DataTable rnMuaBanCK;// = Sheet7.Range("AQ3")

        public DataTable RnMuaBanCK
        {
            get {
                string StSQL_MuaBanCK = "SELECT StockCode,OrderDate,[MatchedValue] GiaTriMua,[MatchedValue] * FeeRate AS MFR , [MatchedValue] * 0.0001 TaxValue ,[OrderSide] FROM TradingResultHist WHERE  AccountId like '" + MaTk + "' And Convert(nvarchar(10), TransactionDate, 112) between '" + tnt3 + "' and '" + dnt3 + "'";
                rnMuaBanCK = VSDBConnection.getDataTable(StSQL_MuaBanCK, VSDBConnection.CSVSFServices);
                return rnMuaBanCK; }
        }
        DataTable rnMuaBanCK1;// = Sheet7.Range("AK3")

        public DataTable RnMuaBanCK1
        {
            get {
                string StSQL_MuaBanCK1 = "SELECT [MatchedValue] GiaTriMua,FeeValue Fee, TaxValue Tax,[OrderSide]  FROM [VFS_Order] WHERE [AccountId] like '" + MaTk + "' and [MatchedValue] != 0 And (Convert(varchar(10), CONVERT(DATETIME, [OrderDate], 103), 112) between '" + tn + "' and '" + dn + "')";
                rnMuaBanCK1 = VSDBConnection.getDataTable(StSQL_MuaBanCK1, VSDBConnection.CSVSFServices);
                return rnMuaBanCK1; }
        }
        DataTable rnTienBanCKChoVe;// = Sheet7.Range("AX3")

        public DataTable RnTienBanCKChoVe
        {
            get {
                string StSQLTienBanCKChoVe = "SELECT [MatchedValue] GiaTriMua,FeeValue Fee, TaxValue Tax,[OrderSide]  FROM [VFS_Order] WHERE [AccountId] like '" + MaTk + "' and [MatchedValue] != 0 And (Convert(nvarchar(10), CONVERT(DATETIME, [OrderDate], 103), 112)  IN ('" + dn0 + "','" + dn1 + "','" + dn2 + "'))";
                rnTienBanCKChoVe = VSDBConnection.getDataTable(StSQLTienBanCKChoVe, VSDBConnection.CSVSFServices);
                return rnTienBanCKChoVe; }
        }
        DataTable rnCoTucChoVe;// = Sheet7.Range("BB3")

        public DataTable RnCoTucChoVe
        {
            get {
                string StSQLCoTucChoVe = "SELECT [StockCodeCurrent],[RateA],[RateB],[RightType],[AccountId],[QuantityCurrent] FROM [VFS_RightExecDetailCustomer] WHERE Accountid like '" + MaTk + "'  and (DatePay between '" + tn_TTDK + "' and '" + dn_TTDK + "' )";
                                          //SELECT [StockCodeCurrent],[RateA],[RateB],[RightType],[AccountId],[QuantityCurrent] FROM [VFS_RightExecDetailCustomer] WHERE Accountid like '" & MaTK & "'  and (DatePay between '" & tn_TTDK & "' and '" & dn_TTDK & "' )
                //string StSQLCoTucChoVe = "SELECT [StockCodeCurrent],[RateA],[RateB],[RightType],[AccountId],[QuantityCurrent] FROM [VFS_RightExecDetailCustomer] WHERE Accountid like '" + MaTk + "' And DatePay >= '" + dn_TTDK + "' and (DateOwnerConfirm between '" + tn_TTDK + "' and '" + dn_TTDK + "' )";
                rnCoTucChoVe = VSDBConnection.getDataTable(StSQLCoTucChoVe, VSDBConnection.CSVSFServices);
                return rnCoTucChoVe; }
        }

        DataTable rnTraNoKyQuyFix;// = Sheet7.Range("BV3")

        public DataTable RnTraNoKyQuyFix
        {
            get {
                string SQLTraNoKyQuyFix = "SELECT ContractId,LogDate,AmountCalInterest,InterestPayNow FROM MAccDetailLog MDL WHERE AccountId like '" + MaTk + "' And (Convert(nvarchar(10),LogDate, 112) Between '" + tn + "' and '" + dn + "') And  MDL.[Status] IN ('C')";
                rnTraNoKyQuyFix = VSDBConnection.getDataTable(SQLTraNoKyQuyFix, VSDBConnection.CSVSFServices);
                return rnTraNoKyQuyFix; }
        }
        DataTable rnTraNoMuaQuyenFix;// = Sheet7.Range("CA3")

        public DataTable RnTraNoMuaQuyenFix
        {
            get {
                string SQLTraNoMuaQuyenFix = "SELECT ContractId,AccountId,AdvanceAmount,AdvanceFee FROM dbo.BuyCashContract WHERE AccountId like '" + MaTk + "' And (Convert(nvarchar(10),PaymentDate, 112)  Between '" + tn + "' and '" + dn + "') and [Status] = 'E'";
                rnTraNoMuaQuyenFix = VSDBConnection.getDataTable(SQLTraNoMuaQuyenFix, VSDBConnection.CSVSFServices);
                return rnTraNoMuaQuyenFix; }
        }
        DataTable rnTraNoUngtruocFix;// = Sheet7.Range("CF3")
        private string Type="N";

        

        public DataTable RnTraNoUngtruocFix
        {
            get {
                string StSQLTraNoUngTruocFix = "SELECT ContractId,  PaymentDate,AdvanceAmount,[Status] FROM dbo.AdvanceContractAll WHERE AccountId like '" + MaTk + "' And (Convert(nvarchar(10),PaymentDate, 112)  Between '" + tn + "' and '" + dn + "') and [Status] = 'E'";
                rnTraNoUngtruocFix = VSDBConnection.getDataTable(StSQLTraNoUngTruocFix, VSDBConnection.CSVSFServices);
                return rnTraNoUngtruocFix; }
        }
        
    }
}