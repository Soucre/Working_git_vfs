using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data;

namespace VfsLookup.Libs
{
    public class StockProvider
    {
        /*
        void BaoCaoTaiSan()
        {
// Custommize for macro
//Application.ScreenUpdating = False
//Application.Calculation = xlCalculationManual
        
    ///Dim workBookTemp As Workbook
    //Dim workSheetTemp As Worksheet
            SqlConnection cnt;
    //Dim cnt As ADODB.Connection
       SqlConnection cnn;
    //Dim cnn As ADODB.Connection
            SqlCommand cmd;
    //Dim cmd As ADODB.Command
           DataTable rcs=new DataTable(); 
    //Dim rcs As ADODB.Recordset
            String rn;
    //Dim tn As String
            String dn;
    //Dim dn As String
            String MaTK;
    //Dim MaTK As String
            String BankGL;
    //Dim BankGL As String
            string tn_TienHienCo;
    //Dim tn_TienHienCo As String
            String dn_TienHienCo;
    //Dim dn_TienHienCo As String
            string tn_No;
    //Dim tn_No As String
            String dn_No;

    //Dim dn_No As String
            String TKc;
    //Dim TKc As String
            String datePriceFormat;
    //Dim datePriceFormat As String
            String tn0;
    //Dim tn0 As String
            String tn1;
    //Dim tn1 As String
            String tn2;
    //Dim tn2 As String
            String dn0;
    //Dim dn0 As String
            String dn1;
    //Dim dn1 As String
            String Dn2;
    //Dim dn2 As String
    
    //On Error Resume Next
    //--------
    //TKc = Sheet5.Range("J9")
            TKc="094C002555";//ma khach hang
    //tn = Sheet5.Range("K19")
            tn_No="20121228";//xủ lý thêm...........................
    //dn = Sheet5.Range("N19")
            dn="20130930";//xử lý thêm............................
    //tn_No = Sheet5.Range("L2")
            tn_No = "20130101";//.....................................
    //dn_No = Sheet5.Range("L3")
        dn_No="20130930";//............................................

    //'MaTK = Sheet3.Range("H4")
    MaTK ="094K002555";// Sheet5.Range("M9").............................
    BankGL ="324131";// Sheet5.Range("K15").....................................
    tn_TienHienCo = "2012-12-28 00:00:00";//Sheet5.Range("L6").....................
    dn_TienHienCo = "2013-09-30 00:00:00";///Sheet5.Range("M6").......................
    //'--- tien hien co + 1
    tn1_TienHienCo ="2013-01-02 00:00:00";// Sheet5.Range("N6")
    dn1_TienHienCo ="2013-10-01 00:00:00";// Sheet5.Range("O6")
    
    //'ngay t-2, t-1, t
    tn0 = Sheet5.Range("I19")
    tn1 = Sheet5.Range("J19")
    tn2 = Sheet5.Range("K19")
    dn0 = Sheet5.Range("L19")
    dn1 = Sheet5.Range("M19")
    dn2 = Sheet5.Range("N19")
    
    //'ngay t, t+1, t+2
    tn00 = Sheet5.Range("Q19")
    tn01 = Sheet5.Range("R19")
    tn02 = Sheet5.Range("S19")
    tn03 = Sheet5.Range("P19")
    
    dn00 = Sheet5.Range("T19")
    dn01 = Sheet5.Range("U19")
    dn02 = Sheet5.Range("V19")
    dn03 = Sheet5.Range("W19")
    
    
    //'--------------
    
    Dim wbBook As Workbook
    Dim wsSheet As Worksheet
   
    Dim rnTienHienCotn As Range
    Dim rnTienHienCodn As Range
    //'---tn
    Dim rnNoKyQuy_tn As Range
    Dim rnMoUngTruoc_tn As Range
    Dim rnNoMuaQuyen_tn As Range
    Dim rnCKtn_BanRaTrongNgay As Range
    Dim rnCKChoVe_tn As Range
    Dim rnCK_HienCo_tn As Range
    //'---dn
    Dim rnNoKyQuy_dn As Range
    Dim rnNoUngTruoc_dn As Range
    Dim rnNoMuaQuyen_dn As Range
    Dim rnCKdn_BanRaTrongNgay As Range
    Dim rnCKChoVe_dn As Range
    Dim rnCK_HienCo_dn As Range
    
    Dim rnTenTaiKhoan As Range
    Dim rnGiaCK As Range
    Dim rnCoTuctn As Range
    Dim rnCoTucdn As Range
    Dim rnPhatHanhThemtn As Range
    Dim rnPhatHanhThemdn As Range
    
    With wsSheet
        Sheet5.Range("J22:L1500").ClearContents
        Sheet3.Range("F2").ClearContents
        Set rnTenTaiKhoan = Sheet3.Range("F2")
        Set rnTienHienCotn = Sheet5.Range("J22")
        Set rnTienHienCodn = Sheet5.Range("L22")
        Sheet8.Range("A3:BE2000").ClearContents
        //'tn
        Set rnNoKyQuy_tn = Sheet8.Range("A3")
        Set rnNoUngTruoc_tn = Sheet8.Range("G3")
        Set rnNoMuaQuyen_tn = Sheet8.Range("M3")
        //'CK ban ra trong ngay
        Sheet9.Range("BA3:BC500").ClearContents
        Set rnCKtn_BanRaTrongNgay = Sheet9.Range("BA3")
        Set rnCKChoVe_tn = Sheet9.Range("E3")
        Sheet9.Range("P3:R1500").ClearContents
        Set rnCK_HienCo_tn = Sheet9.Range("P3")
        //'Sheet9.Range("Y3:AD500").ClearContents
        Set rnCoTuctn = Sheet9.Range("Y3")
        Set rnPhatHanhThemtn = Sheet9.Range("AM3")
        
        //'dn
        
        Set rnNoKyQuy_dn = Sheet8.Range("S3")
        Set rnNoUngTruoc_dn = Sheet8.Range("Y3")
        Set rnNoMuaQuyen_dn = Sheet8.Range("AE3")
        //' Ck ban ra trong ngay
        Sheet9.Range("BE3:BG500").ClearContents
        Set rnCKdn_BanRaTrongNgay = Sheet9.Range("BE3")
        Set rnCKChoVe_dn = Sheet9.Range("J3")
        Sheet9.Range("T3:V1500").ClearContents
        Set rnCK_HienCo_dn = Sheet9.Range("T3")
        Set rnCoTucdn = Sheet9.Range("AF3")
        Set rnPhatHanhThemdn = Sheet9.Range("AT3")
        //'Gia
        Sheet9.Range("A3:H1500").ClearContents
        Sheet9.Range("J3:M1500").ClearContents
        Sheet9.Range("Y3:AD1500").ClearContents
        Sheet9.Range("AF3:AK1500").ClearContents
        Sheet9.Range("AM3:AY1500").ClearContents
        Set rnGiaCKtn = Sheet9.Range("A3")
        Set rnGiaCKdn = Sheet9.Range("C3")
        
    End With
    
    //'stADO1 = "Provider=SQLOLEDB.1;server=dbtest;database=KEN_SFS_TEST;uid=sa;pwd=@password123456;"
    stADO1 = "Provider=SQLOLEDB.1;server=localhost\sqlexpress;database=VFSServices;uid=sa;pwd=123123;"
    stADO2 = "Provider=SQLOLEDB.1;server=localhost\sqlexpress;database=OnlinePrice;uid=sa;pwd=123123;"
    //'---Cau try van Bao Cao
    
    StSQLTenTK = "SELECT [CustomerNameViet] FROM [VFS_Client]  WHERE CustomerId = '" & TKc & "'"
    
    //'---- tn
    StSQL_TienHienCo_tn = "SELECT CurrentBalance AS PreviousBalance  FROM   BalanceHist WHERE  BranchCode = '100' AND BankGL like '324%' AND SectionGL = '3241' AND AccountID like '" & MaTK & "' AND TransactionDate = dbo.fc_GetPreTransDate('" & tn1_TienHienCo & "', '100')"
    
    StSQL_NoKyQuy_tn = "SELECT TOP 1 Balance FROM MAccDetailLog MDL , [VFS_MAccDetailLogBlance] VFS WHERE VFS.LogId = MDL.LogId And AccountId like '" & MaTK & "' And (Convert(nvarchar(10),LogDate, 112) <= '" & tn_No & "') order by LogDate DESC, MDL.LogId DESC "
    StSQL_NoUngTruoc_tn = "SELECT ContractId, DateContract,   AccountId, AdvanceAmount,[Status] FROM dbo.AdvanceContractAll WHERE AccountId like '" & MaTK & "' And (Convert(nvarchar(10),DateContract,112)  IN ('" & tn0 & "' , '" & tn1 & "','" & tn2 & "')) And [Status] = 'E'"
    StSQL_NoMuaQuyen_tn = " SELECT ContractId, DateContract,  AccountId, AdvanceAmount,AdvanceFee,[Status] FROM dbo.BuyCashContract WHERE AccountId like '" & MaTK & "' And (Convert(nvarchar(10),PaymentDate,112)  IN ('" & tn01 & "' , '" & tn02 & "','" & tn03 & "')) And [Status] = 'E'"
    StSQL_CKtn_BanRaTrongNgay = "SELECT [StockCode],[MatchedVolume], [OrderSide]  FROM [VFS_Order] WHERE [AccountId] like '" & MaTK & "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103) = '" & tn2 & "') "
    //'StSQL_CKChoVe_tn = "SELECT [StockCode],[MatchedVolume], [OrderSide] FROM [VFS_Order] WHERE [AccountId] like '" & MaTK & "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103)) IN(SELECT DISTINCT TOP 3 (convert(date, [OrderDate], 103)) FROM  [VFS_Order] WHERE [AccountId] like '" & MaTK & "' And convert(date, [OrderDate], 103) <= '" & tn & "' ORDER BY (convert(date, [OrderDate], 103)) DESC)"
    StSQL_CKChoVe_tn = "SELECT [StockCode],[MatchedVolume], [OrderSide], MatchedValue  FROM [VFS_Order] WHERE [AccountId] like '" & MaTK & "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103)  IN ('" & tn0 & "','" & tn1 & "','" & tn2 & "')) "
    StSQL_CKHienCo_tn = "SELECT [AccountId],StockCode, Quantity FROM SecuritiesHist WHERE [AccountId] like '" & MaTK & "' And TransactionDate = dbo.fc_GetPreTransDate('" & tn1_TienHienCo & "', '100') And ((BankGL = '012121' And SectionGL = '0121') or (BankGL = '012821' And SectionGL = '0128'))"
    //'---- dn
    StSQL_TienHienCo_dn = "SELECT CurrentBalance AS PreviousBalance  FROM   BalanceHist WHERE  BranchCode = '100' AND BankGL like '324%' AND SectionGL = '3241' AND AccountID like '%" & MaTK & "' AND TransactionDate = dbo.fc_GetPreTransDate('" & dn1_TienHienCo & "', '100')"
    
    //'StSQL_NoKyQuy_dn = "SELECT ContractId, AccountId, Amount AS AdvanceAmount,(AmountCalInterest) AS DebtValue,(InterestPayNow) AS InterestValue,[Status] FROM dbo.MAccDetailLog WHERE AccountId like '" & MaTK & "'  And (Convert(nvarchar(10),EffectiveOnDate, 112) <= '" & dn & "' ) And [Status] IN ('B','C')"
    StSQL_NoKyQuy_dn = "SELECT TOP 1 Balance FROM MAccDetailLog MDL , [VFS_MAccDetailLogBlance] VFS WHERE VFS.LogId = MDL.LogId And AccountId like '" & MaTK & "' And (Convert(nvarchar(10),LogDate, 112) <= '" & dn_No & "') order by LogDate DESC, MDL.LogId DESC "
    StSQL_NoUngTruoc_dn = "SELECT ContractId, DateContract,  AccountId, AdvanceAmount,[Status] FROM dbo.AdvanceContractAll WHERE AccountId like '" & MaTK & "' And (Convert(nvarchar(10),DateContract,112)  IN ('" & dn0 & "' , '" & dn1 & "','" & dn2 & "')) And [Status] = 'E'"
    StSQL_NoMuaQuyen_dn = "SELECT ContractId, DateContract,  AccountId, AdvanceAmount,AdvanceFee,[Status] FROM dbo.BuyCashContract WHERE AccountId like '" & MaTK & "' And (Convert(nvarchar(10),PaymentDate,112)  IN ('" & dn01 & "' , '" & dn02 & "','" & dn03 & "')) And [Status] = 'E'"
    StSQL_CKdn_BanRaTrongNgay = "SELECT [StockCode],[MatchedVolume], [OrderSide]  FROM [VFS_Order] WHERE [AccountId] like '" & MaTK & "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103) = '" & dn2 & "') "
    //'StSQL_CKChoVe_dn = "SELECT [StockCode],[MatchedVolume] GiaTriMua, [OrderSide] FROM [VFS_Order] WHERE [AccountId] like '" & MaTK & "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103)) IN(SELECT DISTINCT TOP 3 (convert(date, [OrderDate], 103)) FROM  [VFS_Order] WHERE [AccountId] like '" & MaTK & "' And convert(date, [OrderDate], 103) <= '" & dn & "' ORDER BY (convert(date, [OrderDate], 103)) DESC)"
    StSQL_CKChoVe_dn = "SELECT [StockCode],[MatchedVolume], [OrderSide],MatchedValue  FROM [VFS_Order] WHERE [AccountId] like '" & MaTK & "' and [MatchedValue] != 0 And (convert(date, [OrderDate], 103)  IN ('" & dn0 & "','" & dn1 & "','" & dn2 & "')) "
    StSQL_CKHienCo_dn = "SELECT [AccountId],StockCode, Quantity FROM SecuritiesHist WHERE [AccountId] like '" & MaTK & "' And TransactionDate = dbo.fc_GetPreTransDate('" & dn1_TienHienCo & "', '100') And ((BankGL = '012121' And SectionGL = '0121') or (BankGL = '012821' And SectionGL = '0128'))"
    //'-- Gia
    StSQL_GiaCKtn = "SELECT StockCode,(CASE WHEN (CASE WHEN BoardType = 'M' THEN LastPrice ELSE AveragePrice END) = 0 THEN BasicPrice ELSE (CASE WHEN BoardType = 'M' THEN LastPrice ELSE AveragePrice END) END)*1000 AS PriceStock FROM [OnlinePrice].[dbo].[StockPrice] WHERE BoardType IN ('M','S') And (convert(date, TradingDate, 103)) = '" & tn & "'"
    StSQL_GiaCKdn = "SELECT StockCode,(CASE WHEN (CASE WHEN BoardType = 'M' THEN LastPrice ELSE AveragePrice END) = 0 THEN BasicPrice ELSE (CASE WHEN BoardType = 'M' THEN LastPrice ELSE AveragePrice END) END)*1000 AS PriceStock FROM [OnlinePrice].[dbo].[StockPrice] WHERE BoardType IN ('M','S') And (convert(date, TradingDate, 103)) = '" & dn & "'"
    //'Co tuc
    StSQL_CoTuctn = "SELECT [StockCodeCurrent],[RateA],[RateB],[RightType],[AccountId],[QuantityCurrent] FROM [VFS_RightExecDetailCustomer] WHERE Accountid like '" & MaTK & "' And DatePay >= '" & tn_TienHienCo & "' and DateOwnerConfirm <= '" & tn_TienHienCo & "'"
    StSQL_CoTucdn = "SELECT [StockCodeCurrent],[RateA],[RateB],[RightType],[AccountId],[QuantityCurrent] FROM [VFS_RightExecDetailCustomer] WHERE Accountid like '" & MaTK & "' And DatePay >= '" & dn_TienHienCo & "' and DateOwnerConfirm <= '" & dn_TienHienCo & "'"
    //'Phat hanh them
    StSQL_PhathanhThemtn = "SELECT [CustomerId],RE.StockCode,[RegisteredAmount],[Status],[TransactionDate],[BuyRightQuantity] FROM [RightExecRegister] RER, [RightExec] RE Where  (RE.[Id] = RER.[RightExecId]) And Status = 'A' And CustomerId like '" & MaTK & "' And RE.[DatePay] >= '" & tn_TienHienCo & "'"
    StSQL_PhathanhThemdn = "SELECT [CustomerId],RE.StockCode,[RegisteredAmount],[Status],[TransactionDate],[BuyRightQuantity] FROM [RightExecRegister] RER, [RightExec] RE Where  (RE.[Id] = RER.[RightExecId]) And Status = 'A' And CustomerId like '" & MaTK & "' And RE.[DatePay] >= '" & dn_TienHienCo & "'"
    Set cnt = New ADODB.Connection
    Set cnn = New ADODB.Connection
    //'Set cnn2 = New ADODB.Connection

    //'----- Tien hien co tn
    With cnt
        .Open stADO1
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcsTC = .Execute(StSQL_TienHienCo_tn)
    End With
    rnTienHienCotn.CopyFromRecordset rcsTC
    //'---- Ten Tai Khoan
    With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcsTC = .Execute(StSQLTenTK)
    End With
    rnTenTaiKhoan.CopyFromRecordset rcsTC
   //'----- Tien hien co dn
     With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcsTC = .Execute(StSQL_TienHienCo_dn)
    End With
    rnTienHienCodn.CopyFromRecordset rcsTC
    
    //'----- No ky quy tu ngay
     With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcsTC = .Execute(StSQL_NoKyQuy_tn)
    End With
    rnNoKyQuy_tn.CopyFromRecordset rcsTC
    //'----- No ung truoc tu ngay
     With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcsTC = .Execute(StSQL_NoUngTruoc_tn)
    End With
    rnNoUngTruoc_tn.CopyFromRecordset rcsTC
    //'----- No Mua Quyen tu ngay
    With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcsTC = .Execute(StSQL_NoMuaQuyen_tn)
    End With
    rnNoMuaQuyen_tn.CopyFromRecordset rcsTC
    
    //'----- Chung khoan ban ra trong ngay
    With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcsTC = .Execute(StSQL_CKtn_BanRaTrongNgay)
    End With
    rnCKtn_BanRaTrongNgay.CopyFromRecordset rcsTC
    //'Chung khoan cho ve tn
    With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcsTC = .Execute(StSQL_CKChoVe_tn)
    End With
    rnCKChoVe_tn.CopyFromRecordset rcsTC
    //'Chung khoan hien co tn
      With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcsTC = .Execute(StSQL_CKHienCo_tn)
    End With
    rnCK_HienCo_tn.CopyFromRecordset rcsTC
    
    //'---Co tuc tn -------------
   
    With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcs = .Execute(StSQL_CoTuctn)
    End With
    rnCoTuctn.CopyFromRecordset rcs
    
    //'Phat Hanh them tn
    With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcs = .Execute(StSQL_PhathanhThemtn)
    End With
    rnPhatHanhThemtn.CopyFromRecordset rcs
    
    
    //'--------------- dn ---------------
    //'----- No ky quy den ngay
     With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcsTC = .Execute(StSQL_NoKyQuy_dn)
    End With
    rnNoKyQuy_dn.CopyFromRecordset rcsTC
    ///'----- No ung truoc den ngay
     With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcsTC = .Execute(StSQL_NoUngTruoc_dn)
    End With
    rnNoUngTruoc_dn.CopyFromRecordset rcsTC
    //'----- No Mua Quyen den ngay
     With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcsTC = .Execute(StSQL_NoMuaQuyen_dn)
    End With
    rnNoMuaQuyen_dn.CopyFromRecordset rcsTC
    
    //'----- Chung khoan ban ra trong ngay
    With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcsTC = .Execute(StSQL_CKdn_BanRaTrongNgay)
    End With
    rnCKdn_BanRaTrongNgay.CopyFromRecordset rcsTC
    
   // 'Chung khoan cho ve dn
      With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcsTC = .Execute(StSQL_CKChoVe_dn)
    End With
    rnCKChoVe_dn.CopyFromRecordset rcsTC
    
    //'Chung khoan cho ve dn
      With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcsTC = .Execute(StSQL_CKHienCo_dn)
    End With
    rnCK_HienCo_dn.CopyFromRecordset rcsTC
    
   // '---Co tuc dn ------------
   
    With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcs = .Execute(StSQL_CoTucdn)
    End With
    
    rnCoTucdn.CopyFromRecordset rcs
    //' Phat hanh them dn
     With cnt
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcs = .Execute(StSQL_PhathanhThemdn)
    End With
    rnPhatHanhThemdn.CopyFromRecordset rcs
    // '----- Gia Chung khoan ----------------------------
     With cnn
        .Open stADO2
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcsTC = .Execute(StSQL_GiaCKtn)
    End With
    rnGiaCKtn.CopyFromRecordset rcsTC
    ///'dn
    With cnn
        .CursorLocation = adUseClient
        .CommandTimeout = 0
        Set rcsTC = .Execute(StSQL_GiaCKdn)
    End With
    rnGiaCKdn.CopyFromRecordset rcsTC
    //'--Do'ng du lieu
   
    rcsTC.Close
    rcs.Close
    Set cnt = Nothing
    Set cnn = Nothing
    
    //'customermize for macro
    Application.ScreenUpdating = True
    Application.Calculation = xlCalculationAutomatic
        }

void BienDongTS()
{
//' Custommize for macro
Application.ScreenUpdating = False
Application.Calculation = xlCalculationManual
    Sheet3.Range("D33:E44").ClearContents

    Dim tam As String
    tam = Sheet3.Range("F4")
    Call BaoCaoTaiSan
    
    Sheet3.Range("F4") = Sheet5.Range("S4")
    Call BaoCaoTaiSan
    Sheet3.Range("D33") = Sheet3.Range("E12")
    Sheet3.Range("E33") = Sheet3.Range("E18")
  
    Sheet3.Range("F4") = Sheet5.Range("S5")
    Call BaoCaoTaiSan
    Sheet3.Range("D34") = Sheet3.Range("E12")
    Sheet3.Range("E34") = Sheet3.Range("E18")
    
    Sheet3.Range("F4") = Sheet5.Range("S6")
    Call BaoCaoTaiSan
    Sheet3.Range("D35") = Sheet3.Range("E12")
    Sheet3.Range("E35") = Sheet3.Range("E18")
    
    Sheet3.Range("F4") = Sheet5.Range("S7")
    Call BaoCaoTaiSan
    Sheet3.Range("D36") = Sheet3.Range("E12")
    Sheet3.Range("E36") = Sheet3.Range("E18")
    
    Sheet3.Range("F4") = Sheet5.Range("S8")
    Call BaoCaoTaiSan
    Sheet3.Range("D37") = Sheet3.Range("E12")
    Sheet3.Range("E37") = Sheet3.Range("E18")
    
    Sheet3.Range("F4") = Sheet5.Range("S9")
    Call BaoCaoTaiSan
    Sheet3.Range("D38") = Sheet3.Range("E12")
    Sheet3.Range("E38") = Sheet3.Range("E18")
    
    Sheet3.Range("F4") = Sheet5.Range("S10")
    Call BaoCaoTaiSan
    Sheet3.Range("D39") = Sheet3.Range("E12")
    Sheet3.Range("E39") = Sheet3.Range("E18")
    
    Sheet3.Range("F4") = Sheet5.Range("S11")
    Call BaoCaoTaiSan
    Sheet3.Range("D40") = Sheet3.Range("E12")
    Sheet3.Range("E40") = Sheet3.Range("E18")
    
    Sheet3.Range("F4") = Sheet5.Range("S12")
    Call BaoCaoTaiSan
    Sheet3.Range("D41") = Sheet3.Range("E12")
    Sheet3.Range("E41") = Sheet3.Range("E18")
    
    Sheet3.Range("F4") = Sheet5.Range("S13")
    Call BaoCaoTaiSan
    Sheet3.Range("D42") = Sheet3.Range("E12")
    Sheet3.Range("E42") = Sheet3.Range("E18")
    
    Sheet3.Range("F4") = Sheet5.Range("S14")
    Call BaoCaoTaiSan
    Sheet3.Range("D43") = Sheet3.Range("E12")
    Sheet3.Range("E43") = Sheet3.Range("E18")
    
    Sheet3.Range("F4") = Sheet5.Range("S15")
    Call BaoCaoTaiSan
    Sheet3.Range("D44") = Sheet3.Range("E12")
    Sheet3.Range("E44") = Sheet3.Range("E18")
   
    Sheet3.Range("F4") = tam
    Call BaoCaoTaiSan
    
    Application.ScreenUpdating = True
    Application.Calculation = xlCalculationAutomatic
}*/

    }
}