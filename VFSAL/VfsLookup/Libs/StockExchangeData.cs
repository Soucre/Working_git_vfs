using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace VfsLookup.Libs
{
    public class StockExchangeData : StockProcessBase
    {
        public StockExchangeData(String MaTk,string Type) : base(MaTk,Type) { }

        //tn
        DataTable rnNoKyQuy_tn;

        public DataTable RnNoKyQuy_tn
        {
            get {
                string StSQL_NoKyQuy_tn = "SELECT TOP 1 Balance FROM MAccDetailLog MDL , [VFS_MAccDetailLogBlance] VFS WHERE VFS.LogId = MDL.LogId And AccountId like '" + MaTK +"' And (Convert(nvarchar(10),LogDate, 112) <= '" + tn_No + "') order by LogDate DESC, MDL.LogId DESC ";
                rnNoKyQuy_tn=VSDBConnection.getDataTable(StSQL_NoKyQuy_tn, VSDBConnection.CSVSFServices);
                return rnNoKyQuy_tn; }
            
        }
        DataTable rnNoUngTruoc_tn;

        public DataTable RnNoUngTruoc_tn
        {
            get {
                //string StSQL_NoUngTruoc_tn = "SELECT ContractId, DateContract,   AccountId, AdvanceAmount,[Status] FROM dbo.AdvanceContractAll WHERE AccountId like '" + MaTK + "' And (Convert(nvarchar(10),PaymentDate,112)  IN ('" + tn0 + "' , '" + tn1 + "','" + tn2 + "')) And [Status] = 'E'";
                string StSQL_NoUngTruoc_tn = "SELECT ContractId, DateContract,   AccountId, AdvanceAmount,[Status] FROM dbo.AdvanceContractAll WHERE AccountId like '" + MaTK + "' And (Convert(nvarchar(10),OrderDate,112)  IN ('" + tn0 + "' , '" + tn1 + "','" + tn2 + "')) And [Status] IN ('E','T')";
                //string StSQL_NoUngTruoc_tn = "SELECT ContractId, DateContract,   AccountId, AdvanceAmount,[Status] FROM dbo.AdvanceContractAll WHERE AccountId like '" + MaTK + "' And (Convert(nvarchar(10),DateContract,112)  IN ('" + tn0 + "' , '" + tn1 + "','" + tn2 + "')) And [Status] = 'E'";
                rnNoUngTruoc_tn = VSDBConnection.getDataTable(StSQL_NoUngTruoc_tn, VSDBConnection.CSVSFServices);
                return rnNoUngTruoc_tn; }
        }
        DataTable rnNoMuaQuyen_tn;

        public DataTable RnNoMuaQuyen_tn
        {
            get {
                //string StSQL_NoMuaQuyen_tn = " SELECT ContractId, DateContract,  AccountId, AdvanceAmount,AdvanceFee,[Status] FROM dbo.BuyCashContract WHERE AccountId like '" + MaTK + "' And (Convert(nvarchar(10),PaymentDate,112)  IN ('" + tn01 + "' , '" + tn02 + "','" + tn03 + "')) And [Status] = 'E'";
                string StSQL_NoMuaQuyen_tn = " SELECT ContractId, DateContract,  AccountId, AdvanceAmount,AdvanceFee,[Status] FROM dbo.BuyCashContract WHERE AccountId like '" + MaTK + "' And (Convert(nvarchar(10),OrderDate,112)  IN ('" + tn0 + "' , '" + tn1 + "','" + tn2 + "')) And [Status] IN ('E','T')";
                    rnNoMuaQuyen_tn=VSDBConnection.getDataTable(StSQL_NoMuaQuyen_tn, VSDBConnection.CSVSFServices);
                return rnNoMuaQuyen_tn; }
        }
        /**dn*/
        DataTable rnNoKyQuy_dn;

        public DataTable RnNoKyQuy_dn
        {
            get {
                string StSQL_NoKyQuy_dn = "SELECT TOP 1 Balance FROM MAccDetailLog MDL , [VFS_MAccDetailLogBlance] VFS WHERE VFS.LogId = MDL.LogId And AccountId like '" + MaTK + "' And (Convert(nvarchar(10),LogDate, 112) <= '" + dn_No + "') order by LogDate DESC, MDL.LogId DESC ";
                    rnNoKyQuy_dn=VSDBConnection.getDataTable(StSQL_NoKyQuy_dn, VSDBConnection.CSVSFServices);
                return rnNoKyQuy_dn; }
        }
        DataTable rnNoUngTruoc_dn;

        public DataTable RnNoUngTruoc_dn
        {
            get {
                //string StSQL_NoUngTruoc_dn = "SELECT ContractId, DateContract,  AccountId, AdvanceAmount,[Status] FROM dbo.AdvanceContractAll WHERE AccountId like '" + MaTK + "' And (Convert(nvarchar(10),PaymentDate,112)  IN ('" + dn0 + "' , '" + dn1 + "','" + dn2 + "')) And [Status] = 'E'";
                string StSQL_NoUngTruoc_dn = "SELECT ContractId, DateContract,  AccountId, AdvanceAmount,[Status] FROM dbo.AdvanceContractAll WHERE AccountId like '" + MaTK + "' And (Convert(nvarchar(10),OrderDate,112)  IN ('" + dn0 + "' , '" + dn1 + "','" + dn2 + "')) And [Status] IN ('E','T')";
                rnNoUngTruoc_dn=VSDBConnection.getDataTable(StSQL_NoUngTruoc_dn, VSDBConnection.CSVSFServices);
                return rnNoUngTruoc_dn; }
        }
        DataTable rnNoMuaQuyen_dn;

        public DataTable RnNoMuaQuyen_dn
        {
            get {
                //string StSQL_NoMuaQuyen_dn = "SELECT ContractId, DateContract,  AccountId, AdvanceAmount,AdvanceFee,[Status] FROM dbo.BuyCashContract WHERE AccountId like '" + MaTK + "' And (Convert(nvarchar(10),PaymentDate,112)  IN ('" + dn01 + "' , '" + dn02 + "','" + dn03 + "')) And [Status] = 'E'";
                string StSQL_NoMuaQuyen_dn = "SELECT ContractId, DateContract,  AccountId, AdvanceAmount,AdvanceFee,[Status] FROM dbo.BuyCashContract WHERE AccountId like '" + MaTK + "' And (Convert(nvarchar(10),OrderDate,112)  IN ('" + dn0 + "' , '" + dn1 + "','" + dn2 + "')) And [Status] IN ('E','T')";
                    rnNoMuaQuyen_dn=VSDBConnection.getDataTable(StSQL_NoMuaQuyen_dn, VSDBConnection.CSVSFServices);
                return rnNoMuaQuyen_dn; }
        }
        /*
        '---Cau try van Bao Cao
    
   StSQLTenTK = "SELECT [CustomerNameViet] FROM [VFS_Client]  WHERE CustomerId = '" & TKc & "'"
    
   '---- tn
   StSQL_TienHienCo_tn = "SELECT CurrentBalance AS PreviousBalance  FROM   BalanceHist WHERE  BranchCode = '100' AND BankGL like '324%' AND SectionGL = '3241' AND AccountID like '" & MaTK & "' AND TransactionDate = dbo.fc_GetPreTransDate('" & tn1_TienHienCo & "', '100')"
    
   StSQL_NoKyQuy_tn = "SELECT TOP 1 Balance FROM MAccDetailLog MDL , [VFS_MAccDetailLogBlance] VFS WHERE VFS.LogId = MDL.LogId And AccountId like '" & MaTK & "' And (Convert(nvarchar(10),LogDate, 112) <= '" & tn_No & "') order by LogDate DESC, MDL.LogId DESC "
   StSQL_NoUngTruoc_tn = "SELECT ContractId, DateContract,   AccountId, AdvanceAmount,[Status] FROM dbo.AdvanceContractAll WHERE AccountId like '" & MaTK & "' And (Convert(nvarchar(10),DateContract,112)  IN ('" & tn0 & "' , '" & tn1 & "','" & tn2 & "')) And [Status] = 'E'"
   StSQL_NoMuaQuyen_tn = " SELECT ContractId, DateContract,  AccountId, AdvanceAmount,AdvanceFee,[Status] FROM dbo.BuyCashContract WHERE AccountId like '" & MaTK & "' And (Convert(nvarchar(10),PaymentDate,112)  IN ('" & tn01 & "' , '" & tn02 & "','" & tn03 & "')) And [Status] = 'E'"
   StSQL_CKtn_BanRaTrongNgay = "SELECT [StockCode],[MatchedVolume], [OrderSide]  FROM [VFS_Order] WHERE [AccountId] like '" & MaTK & "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103) = '" & tn2 & "') "
   
   StSQL_CKChoVe_tn = "SELECT [StockCode],[MatchedVolume], [OrderSide], MatchedValue  FROM [VFS_Order] WHERE [AccountId] like '" & MaTK & "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103)  IN ('" & tn0 & "','" & tn1 & "','" & tn2 & "')) "
   StSQL_CKHienCo_tn = "SELECT [AccountId],StockCode, Quantity FROM SecuritiesHist WHERE [AccountId] like '" & MaTK & "' And TransactionDate = dbo.fc_GetPreTransDate('" & tn1_TienHienCo & "', '100') And ((BankGL = '012121' And SectionGL = '0121') or (BankGL = '012821' And SectionGL = '0128'))"
   '---- dn
   StSQL_TienHienCo_dn = "SELECT CurrentBalance AS PreviousBalance  FROM   BalanceHist WHERE  BranchCode = '100' AND BankGL like '324%' AND SectionGL = '3241' AND AccountID like '%" & MaTK & "' AND TransactionDate = dbo.fc_GetPreTransDate('" & dn1_TienHienCo & "', '100')"
    
   
   StSQL_NoKyQuy_dn = "SELECT TOP 1 Balance FROM MAccDetailLog MDL , [VFS_MAccDetailLogBlance] VFS WHERE VFS.LogId = MDL.LogId And AccountId like '" & MaTK & "' And (Convert(nvarchar(10),LogDate, 112) <= '" & dn_No & "') order by LogDate DESC, MDL.LogId DESC "
   StSQL_NoUngTruoc_dn = "SELECT ContractId, DateContract,  AccountId, AdvanceAmount,[Status] FROM dbo.AdvanceContractAll WHERE AccountId like '" & MaTK & "' And (Convert(nvarchar(10),DateContract,112)  IN ('" & dn0 & "' , '" & dn1 & "','" & dn2 & "')) And [Status] = 'E'"
   StSQL_NoMuaQuyen_dn = "SELECT ContractId, DateContract,  AccountId, AdvanceAmount,AdvanceFee,[Status] FROM dbo.BuyCashContract WHERE AccountId like '" & MaTK & "' And (Convert(nvarchar(10),PaymentDate,112)  IN ('" & dn01 & "' , '" & dn02 & "','" & dn03 & "')) And [Status] = 'E'"
   StSQL_CKdn_BanRaTrongNgay = "SELECT [StockCode],[MatchedVolume], [OrderSide]  FROM [VFS_Order] WHERE [AccountId] like '" & MaTK & "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103) = '" & dn2 & "') "
   
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