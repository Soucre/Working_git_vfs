using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace VfsLookup.Libs
{
    public class OnlinePrice: StockProcessBase
    {
        public OnlinePrice(String MaTk,string Type):base(MaTk,Type)
        {
        }

        //ck ban ra trong ngay
        DataTable rnCKtn_BanRaTrongNgay;

        public DataTable RnCKtn_BanRaTrongNgay
        {
            get {
                string StSQL_CKtn_BanRaTrongNgay = "SELECT [StockCode],[MatchedVolume], [OrderSide]  FROM [VFS_Order] WHERE [AccountId] like '" + MaTK + "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103) = '" + tn2 + "') ";
                rnCKtn_BanRaTrongNgay = VSDBConnection.getDataTable(StSQL_CKtn_BanRaTrongNgay, VSDBConnection.CSVSFServices);
                return rnCKtn_BanRaTrongNgay; }
        }
        DataTable rnCKChoVe_tn;

        public DataTable RnCKChoVe_tn
        {
            get {
                string StSQL_CKChoVe_tn = "SELECT [StockCode],[MatchedVolume], [OrderSide], MatchedValue  FROM [VFS_Order] WHERE [AccountId] like '" + MaTK + "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103)  IN ('" + tn0 + "','" + tn1 + "','" + tn2 + "')) ";
                    rnCKChoVe_tn = VSDBConnection.getDataTable(StSQL_CKChoVe_tn, VSDBConnection.CSVSFServices);
                return rnCKChoVe_tn; }
        }
        DataTable rnCK_HienCo_tn;

        public DataTable RnCK_HienCo_tn
        {
            get {
                string StSQL_CKHienCo_tn = "SELECT [AccountId],StockCode, Quantity FROM SecuritiesHist WHERE [AccountId] like '" + MaTK + "' And TransactionDate = dbo.fc_GetPreTransDate('" + tn1_TienHienCo + "', '100') And ((BankGL = '012121' And SectionGL = '0121') or (BankGL = '012821' And SectionGL = '0128'))";
                if (nowtimetn)
                {
                    StSQL_CKHienCo_tn = @"DECLARE @AccountId varchar(10) SET @AccountId = '"+MaTK+@"' 
                                        IF((SELECT Count(*) FROM Securities) = 0) 
	                                        begin
		                                        SELECT [AccountId],StockCode, Quantity 
		                                        FROM SecuritiesHist 
		                                        WHERE [AccountId] like @AccountId 
		                                        And TransactionDate = (SELECT MAX(TransactionDate) FROM SecuritiesHist)
		                                        And ((BankGL = '012121' And SectionGL = '0121') or (BankGL = '012821' And SectionGL = '0128'))

	                                        End
	
                                        ELSE
	                                        begin
		                                        SELECT [AccountId],StockCode, Quantity 
		                                        FROM Securities
		                                        WHERE [AccountId] like '"+MaTK+@"' 
		                                        And ((BankGL = '012121' And SectionGL = '0121') or (BankGL = '012821' And SectionGL = '0128'))
	                                    End
                ";
                }
                //string StSQL_CKHienCo_tn = "SELECT [AccountId],StockCode, Quantity FROM SecuritiesHist WHERE [AccountId] like '" + MaTK + "' And TransactionDate = dbo.fc_GetPreTransDate('" + tn1_TienHienCo + "', '100') And ((BankGL = '012121' And SectionGL = '0121') or (BankGL = '012821' And SectionGL = '0128'))";
                    rnCK_HienCo_tn = VSDBConnection.getDataTable(StSQL_CKHienCo_tn, VSDBConnection.CSVSFServices);
                return rnCK_HienCo_tn; }
        }
        DataTable rnCoTuctn;

        public DataTable RnCoTuctn
        {
            get {
                string StSQL_CoTuctn = "SELECT [StockCodeCurrent],[RateA],[RateB],[RightType],[AccountId],[QuantityCurrent] FROM [VFS_RightExecDetailCustomer] WHERE Accountid like '" + MaTK + "' And DatePay >= '" + tn_TienHienCo + "' and DateOwnerConfirm <= '" + tn_TienHienCo + "'";
                //    string StSQL_CoTuctn = "SELECT [StockCodeCurrent],[RateA],[RateB],[RightType],[AccountId],[QuantityCurrent] FROM [VFS_RightExecDetailCustomer] WHERE Accountid like '" + MaTK + "' And DatePay >= '" + tn_TienHienCo + "' and DateOwnerConfirm <= '" + tn_TienHienCo + "'";
                rnCoTuctn = VSDBConnection.getDataTable(StSQL_CoTuctn, VSDBConnection.CSVSFServices);
                return rnCoTuctn; }
        }
        DataTable rnPhatHanhThemtn;

        public DataTable RnPhatHanhThemtn
        {
            get {
                string StSQL_PhathanhThemtn = "SELECT [CustomerId],RE.StockCode,[RegisteredAmount],[Status],[TransactionDate],[BuyRightQuantity] FROM [RightExecRegister] RER, [RightExec] RE Where  (RE.[Id] = RER.[RightExecId]) And Status = 'A' And CustomerId like '" + MaTK + "' And RE.[DatePay] >= '" + tn_TienHienCo + "'";
                     rnPhatHanhThemtn = VSDBConnection.getDataTable(StSQL_PhathanhThemtn, VSDBConnection.CSVSFServices);
                return rnPhatHanhThemtn; }
        }
         // Ck ban ra trong ngay
        DataTable rnCKdn_BanRaTrongNgay;

        public DataTable RnCKdn_BanRaTrongNgay
        {
            get {
                string StSQL_CKdn_BanRaTrongNgay = "SELECT [StockCode],[MatchedVolume], [OrderSide]  FROM [VFS_Order] WHERE [AccountId] like '" + MaTK + "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103) = '" + dn2 + "') ";
                    rnCKdn_BanRaTrongNgay = VSDBConnection.getDataTable(StSQL_CKdn_BanRaTrongNgay, VSDBConnection.CSVSFServices);
                return rnCKdn_BanRaTrongNgay; }
        }
        DataTable rnCKChoVe_dn;

        public DataTable RnCKChoVe_dn
        {
            get {
                string StSQL_CKChoVe_dn = "SELECT [StockCode],[MatchedVolume], [OrderSide],MatchedValue  FROM [VFS_Order] WHERE [AccountId] like '" + MaTK + "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103)  IN ('" + dn0 + "','" + dn1 + "','" + dn2 + "')) ";
                    rnCKChoVe_dn = VSDBConnection.getDataTable(StSQL_CKChoVe_dn, VSDBConnection.CSVSFServices);
                return rnCKChoVe_dn; }
        }
        DataTable rnCK_HienCo_dn;

        public DataTable RnCK_HienCo_dn
        {
            get {
                string StSQL_CKHienCo_dn = "SELECT [AccountId],StockCode, Quantity FROM SecuritiesHist WHERE [AccountId] like '" + MaTK + "' And TransactionDate = dbo.fc_GetPreTransDate('" + dn1_TienHienCo + "', '100') And ((BankGL = '012121' And SectionGL = '0121') or (BankGL = '012821' And SectionGL = '0128'))";
                //string StSQL_CKHienCo_dn = "SELECT [AccountId],StockCode, Quantity FROM SecuritiesHist WHERE [AccountId] like '" + MaTK + "' And TransactionDate = dbo.fc_GetPreTransDate('" + dn1_TienHienCo + "', '100') And ((BankGL = '012121' And SectionGL = '0121') or (BankGL = '012821' And SectionGL = '0128'))";
                if (nowtimedn)
                {
                    StSQL_CKHienCo_dn = @"DECLARE @AccountId varchar(10) SET @AccountId = '" + MaTK + @"' 
                                        IF((SELECT Count(*) FROM Securities) = 0) 
	                                        begin
		                                        SELECT [AccountId],StockCode, Quantity 
		                                        FROM SecuritiesHist 
		                                        WHERE [AccountId] like @AccountId 
		                                        And TransactionDate = (SELECT MAX(TransactionDate) FROM SecuritiesHist)
		                                        And ((BankGL = '012121' And SectionGL = '0121') or (BankGL = '012821' And SectionGL = '0128'))

	                                        End
	
                                        ELSE
	                                        begin
		                                        SELECT [AccountId],StockCode, Quantity 
		                                        FROM Securities
		                                        WHERE [AccountId] like '" + MaTK + @"' 
		                                        And ((BankGL = '012121' And SectionGL = '0121') or (BankGL = '012821' And SectionGL = '0128'))
	                                    End
                ";
                }
                rnCK_HienCo_dn = VSDBConnection.getDataTable(StSQL_CKHienCo_dn, VSDBConnection.CSVSFServices);
                return rnCK_HienCo_dn; }
        }
        DataTable rnCoTucdn;

        public DataTable RnCoTucdn
        {
            get {
                string StSQL_CoTucdn = "SELECT [StockCodeCurrent],[RateA],[RateB],[RightType],[AccountId],[QuantityCurrent] FROM [VFS_RightExecDetailCustomer] WHERE Accountid like '" + MaTK + "' And DatePay >= '" + dn_TienHienCo + "' and DateOwnerConfirm <= '" + dn_TienHienCo + "'";
                    rnCoTucdn = VSDBConnection.getDataTable(StSQL_CoTucdn, VSDBConnection.CSVSFServices);
                return rnCoTucdn; }
        }
        DataTable rnPhatHanhThemdn;

        public DataTable RnPhatHanhThemdn
        {
            get {
                string StSQL_PhathanhThemdn = "SELECT [CustomerId],RE.StockCode,[RegisteredAmount],[Status],[TransactionDate],[BuyRightQuantity] FROM [RightExecRegister] RER, [RightExec] RE Where  (RE.[Id] = RER.[RightExecId]) And Status = 'A' And CustomerId like '" + MaTK + "' And RE.[DatePay] >= '" + dn_TienHienCo + "'";
                    rnPhatHanhThemdn = VSDBConnection.getDataTable(StSQL_PhathanhThemdn, VSDBConnection.CSVSFServices);
                return rnPhatHanhThemdn; }
        }
        //Gia
        DataTable rnGiaCKtn;

        public DataTable RnGiaCKtn
        {
            get {
                string StSQL_GiaCKtn = "SELECT StockCode,(CASE WHEN (CASE WHEN BoardType = 'M' THEN LastPrice ELSE AveragePrice END) = 0 THEN BasicPrice ELSE (CASE WHEN BoardType = 'M' THEN LastPrice ELSE AveragePrice END) END)*1000 AS PriceStock FROM [OnlinePrice].[dbo].[StockPrice] WHERE BoardType IN ('M','S') And (convert(date, TradingDate, 103)) = '" + tn + "'";
                    rnGiaCKtn = VSDBConnection.getDataTable(StSQL_GiaCKtn, VSDBConnection.CSOnlinePrice);
                return rnGiaCKtn; }
        }
        DataTable rnGiaCKdn;
        


        public DataTable RnGiaCKdn
        {
            get {
                string StSQL_GiaCKdn = "SELECT StockCode,(CASE WHEN (CASE WHEN BoardType = 'M' THEN LastPrice ELSE AveragePrice END) = 0 THEN BasicPrice ELSE (CASE WHEN BoardType = 'M' THEN LastPrice ELSE AveragePrice END) END)*1000 AS PriceStock FROM [OnlinePrice].[dbo].[StockPrice] WHERE BoardType IN ('M','S') And (convert(date, TradingDate, 103)) = '" + dn + "'";
                rnGiaCKdn = VSDBConnection.getDataTable(StSQL_GiaCKdn, VSDBConnection.CSVSFServices);
                return rnGiaCKdn; }
        }
        /*
         '---Cau try van Bao Cao
    
    StSQLTenTK = "SELECT [CustomerNameViet] FROM [VFS_Client]  WHERE CustomerId = '" + TKc + "'"
    
    '---- tn
    StSQL_TienHienCo_tn = "SELECT CurrentBalance AS PreviousBalance  FROM   BalanceHist WHERE  BranchCode = '100' AND BankGL like '324%' AND SectionGL = '3241' AND AccountID like '" & MaTK & "' AND TransactionDate = dbo.fc_GetPreTransDate('" & tn1_TienHienCo & "', '100')"
    
    StSQL_NoKyQuy_tn = "SELECT TOP 1 Balance FROM MAccDetailLog MDL , [VFS_MAccDetailLogBlance] VFS WHERE VFS.LogId = MDL.LogId And AccountId like '" & MaTK & "' And (Convert(nvarchar(10),LogDate, 112) <= '" & tn_No & "') order by LogDate DESC, MDL.LogId DESC "
    StSQL_NoUngTruoc_tn = "SELECT ContractId, DateContract,   AccountId, AdvanceAmount,[Status] FROM dbo.AdvanceContractAll WHERE AccountId like '" & MaTK & "' And (Convert(nvarchar(10),DateContract,112)  IN ('" & tn0 & "' , '" & tn1 & "','" & tn2 & "')) And [Status] = 'E'"
    StSQL_NoMuaQuyen_tn = " SELECT ContractId, DateContract,  AccountId, AdvanceAmount,AdvanceFee,[Status] FROM dbo.BuyCashContract WHERE AccountId like '" & MaTK & "' And (Convert(nvarchar(10),PaymentDate,112)  IN ('" & tn01 & "' , '" & tn02 & "','" & tn03 & "')) And [Status] = 'E'"
    StSQL_CKtn_BanRaTrongNgay = "SELECT [StockCode],[MatchedVolume], [OrderSide]  FROM [VFS_Order] WHERE [AccountId] like '" & MaTK & "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103) = '" & tn2 & "') "
    'StSQL_CKChoVe_tn = "SELECT [StockCode],[MatchedVolume], [OrderSide] FROM [VFS_Order] WHERE [AccountId] like '" & MaTK & "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103)) IN(SELECT DISTINCT TOP 3 (convert(date, [OrderDate], 103)) FROM  [VFS_Order] WHERE [AccountId] like '" & MaTK & "' And convert(date, [OrderDate], 103) <= '" & tn & "' ORDER BY (convert(date, [OrderDate], 103)) DESC)"
    StSQL_CKChoVe_tn = "SELECT [StockCode],[MatchedVolume], [OrderSide], MatchedValue  FROM [VFS_Order] WHERE [AccountId] like '" & MaTK & "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103)  IN ('" & tn0 & "','" & tn1 & "','" & tn2 & "')) "
    StSQL_CKHienCo_tn = "SELECT [AccountId],StockCode, Quantity FROM SecuritiesHist WHERE [AccountId] like '" & MaTK & "' And TransactionDate = dbo.fc_GetPreTransDate('" & tn1_TienHienCo & "', '100') And ((BankGL = '012121' And SectionGL = '0121') or (BankGL = '012821' And SectionGL = '0128'))"
    '---- dn
    StSQL_TienHienCo_dn = "SELECT CurrentBalance AS PreviousBalance  FROM   BalanceHist WHERE  BranchCode = '100' AND BankGL like '324%' AND SectionGL = '3241' AND AccountID like '%" & MaTK & "' AND TransactionDate = dbo.fc_GetPreTransDate('" & dn1_TienHienCo & "', '100')"
    
    'StSQL_NoKyQuy_dn = "SELECT ContractId, AccountId, Amount AS AdvanceAmount,(AmountCalInterest) AS DebtValue,(InterestPayNow) AS InterestValue,[Status] FROM dbo.MAccDetailLog WHERE AccountId like '" & MaTK & "'  And (Convert(nvarchar(10),EffectiveOnDate, 112) <= '" & dn & "' ) And [Status] IN ('B','C')"
    StSQL_NoKyQuy_dn = "SELECT TOP 1 Balance FROM MAccDetailLog MDL , [VFS_MAccDetailLogBlance] VFS WHERE VFS.LogId = MDL.LogId And AccountId like '" & MaTK & "' And (Convert(nvarchar(10),LogDate, 112) <= '" & dn_No & "') order by LogDate DESC, MDL.LogId DESC "
    StSQL_NoUngTruoc_dn = "SELECT ContractId, DateContract,  AccountId, AdvanceAmount,[Status] FROM dbo.AdvanceContractAll WHERE AccountId like '" & MaTK & "' And (Convert(nvarchar(10),DateContract,112)  IN ('" & dn0 & "' , '" & dn1 & "','" & dn2 & "')) And [Status] = 'E'"
    StSQL_NoMuaQuyen_dn = "SELECT ContractId, DateContract,  AccountId, AdvanceAmount,AdvanceFee,[Status] FROM dbo.BuyCashContract WHERE AccountId like '" & MaTK & "' And (Convert(nvarchar(10),PaymentDate,112)  IN ('" & dn01 & "' , '" & dn02 & "','" & dn03 & "')) And [Status] = 'E'"
    StSQL_CKdn_BanRaTrongNgay = "SELECT [StockCode],[MatchedVolume], [OrderSide]  FROM [VFS_Order] WHERE [AccountId] like '" & MaTK & "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103) = '" & dn2 & "') "
    'StSQL_CKChoVe_dn = "SELECT [StockCode],[MatchedVolume] GiaTriMua, [OrderSide] FROM [VFS_Order] WHERE [AccountId] like '" & MaTK & "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103)) IN(SELECT DISTINCT TOP 3 (convert(date, [OrderDate], 103)) FROM  [VFS_Order] WHERE [AccountId] like '" & MaTK & "' And convert(date, [OrderDate], 103) <= '" & dn & "' ORDER BY (convert(date, [OrderDate], 103)) DESC)"
    StSQL_CKChoVe_dn = "SELECT [StockCode],[MatchedVolume], [OrderSide],MatchedValue  FROM [VFS_Order] WHERE [AccountId] like '" & MaTK & "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103)  IN ('" & dn0 & "','" & dn1 & "','" & dn2 & "')) "
    StSQL_CKHienCo_dn = "SELECT [AccountId],StockCode, Quantity FROM SecuritiesHist WHERE [AccountId] like '" & MaTK & "' And TransactionDate = dbo.fc_GetPreTransDate('" & dn1_TienHienCo & "', '100') And ((BankGL = '012121' And SectionGL = '0121') or (BankGL = '012821' And SectionGL = '0128'))"
    '-- Gia
    StSQL_GiaCKtn = "SELECT StockCode,(CASE WHEN (CASE WHEN BoardType = 'M' THEN LastPrice ELSE AveragePrice END) = 0 THEN BasicPrice ELSE (CASE WHEN BoardType = 'M' THEN LastPrice ELSE AveragePrice END) END)*1000 AS PriceStock FROM [OnlinePrice].[dbo].[StockPrice] WHERE BoardType IN ('M','S') And (convert(date, TradingDate, 103)) = '" & tn & "'"
    StSQL_GiaCKdn = "SELECT StockCode,(CASE WHEN (CASE WHEN BoardType = 'M' THEN LastPrice ELSE AveragePrice END) = 0 THEN BasicPrice ELSE (CASE WHEN BoardType = 'M' THEN LastPrice ELSE AveragePrice END) END)*1000 AS PriceStock FROM [OnlinePrice].[dbo].[StockPrice] WHERE BoardType IN ('M','S') And (convert(date, TradingDate, 103)) = '" & dn & "'"
    'Co tuc
    StSQL_CoTuctn = "SELECT [StockCodeCurrent],[RateA],[RateB],[RightType],[AccountId],[QuantityCurrent] FROM [VFS_RightExecDetailCustomer] WHERE Accountid like '" & MaTK & "' And DatePay >= '" & tn_TienHienCo & "' and DateOwnerConfirm <= '" & tn_TienHienCo & "'"
    StSQL_CoTucdn = "SELECT [StockCodeCurrent],[RateA],[RateB],[RightType],[AccountId],[QuantityCurrent] FROM [VFS_RightExecDetailCustomer] WHERE Accountid like '" & MaTK & "' And DatePay >= '" & dn_TienHienCo & "' and DateOwnerConfirm <= '" & dn_TienHienCo & "'"
    'Phat hanh them
    StSQL_PhathanhThemtn = "SELECT [CustomerId],RE.StockCode,[RegisteredAmount],[Status],[TransactionDate],[BuyRightQuantity] FROM [RightExecRegister] RER, [RightExec] RE Where  (RE.[Id] = RER.[RightExecId]) And Status = 'A' And CustomerId like '" & MaTK & "' And RE.[DatePay] >= '" & tn_TienHienCo & "'"
    StSQL_PhathanhThemdn = "SELECT [CustomerId],RE.StockCode,[RegisteredAmount],[Status],[TransactionDate],[BuyRightQuantity] FROM [RightExecRegister] RER, [RightExec] RE Where  (RE.[Id] = RER.[RightExecId]) And Status = 'A' And CustomerId like '" & MaTK & "' And RE.[DatePay] >= '" & dn_TienHienCo & "'"
          
         */
    }
}