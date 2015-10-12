IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VFS_DIV]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].VFS_DIV
GO

CREATE PROCEDURE [dbo].VFS_DIV
@YearReport int, -- test default
@TotalRow int
AS 

---------------------------------------------------------

BEGIN 
	Create table #TABLETEMP(
		Stock nvarchar(20),
		Price float,
		DIV2014 float,
		DIV2013 float,
		DIV2012 float,
		DIV2011 float,
		DIV2010 float,
		CoTucKH2014 float,
		CoTucKH2015 float,
		DoanhThuKeHoach float,
		LoiNhuanKeHoach float,
		DoanhThuLyKe float,
		LoiNhuanTruocThue float,
		EventNote nvarchar(max)
	)
	-- insert the current price into table 
	INSERT INTO #TABLETEMP 
		SELECT RTRIM(LTRIM(StockSymbol)) as StockSymbol, Last*10 , 0,0,0,0,0,0,0,0,0,0,0,''
		FROM [StoxData].[dbo].[stox_tb_HOSE_Trading]  
		where DateReport in (select top 1 DateReport from stox_tb_HOSE_Trading order by DateReport DESC) 
		union 
		select code, close_price ,0,0,0,0,0,0,0,0,0,0,0,''
		FROM [StoxData].[dbo].[stox_tb_StocksInfo] 
		where trading_date in ( select top 1 trading_date from stox_tb_StocksInfo order by trading_date DESC) 
		

	-- co tuch 2014
	UPDATE #TABLETEMP SET  DIV2014 = CoTuc	
	FROM (SELECT [Ticker],SUM([Value])/100 CoTuc FROM [stox_tb_corp_CashDividend] WHERE [CashYear] = @YearReport Group by Ticker) CoTuc2014
	WHERE CoTuc2014.Ticker = #TABLETEMP.Stock
	
	-- co tuc 2013
	UPDATE #TABLETEMP SET  DIV2013 = CoTuc	
	FROM (SELECT [Ticker],SUM([Value])/100 CoTuc FROM [stox_tb_corp_CashDividend] WHERE [CashYear] = @YearReport - 1 Group by Ticker) CoTuc2014
	WHERE CoTuc2014.Ticker = #TABLETEMP.Stock
	
		-- co tuc 2012
	UPDATE #TABLETEMP SET  DIV2012 = CoTuc	
	FROM (SELECT [Ticker],SUM([Value])/100 CoTuc FROM [stox_tb_corp_CashDividend] WHERE [CashYear] = @YearReport - 2 Group by Ticker) CoTuc2014
	WHERE CoTuc2014.Ticker = #TABLETEMP.Stock

		-- co tuc 2011
	UPDATE #TABLETEMP SET  DIV2011 = CoTuc	
	FROM (SELECT [Ticker],SUM([Value])/100 CoTuc FROM [stox_tb_corp_CashDividend] WHERE [CashYear] = @YearReport -3  Group by Ticker) CoTuc2014
	WHERE CoTuc2014.Ticker = #TABLETEMP.Stock

		-- co tuc 2010
	UPDATE #TABLETEMP SET  DIV2010 = CoTuc	
	FROM (SELECT [Ticker],SUM([Value])/100 CoTuc FROM [stox_tb_corp_CashDividend] WHERE [CashYear] = @YearReport - 4 Group by Ticker) CoTuc2014
	WHERE CoTuc2014.Ticker = #TABLETEMP.Stock

	-- co tuc ke hoach 2014
	UPDATE #TABLETEMP SET  CoTucKH2014 = TableTemp.CoTuc2014 
	FROM (SELECT [Ticker],[Cotuccuanam] CoTuc2014 FROM [stox_tb_corp_DividendYield] WHERE [YearReport] = @YearReport) TableTemp
	WHERE TableTemp.Ticker = #TABLETEMP.Stock

	-- co tuc ke hoach 2015
	UPDATE #TABLETEMP SET  CoTucKH2015 = TableTemp.CoTuc2015 
	FROM (SELECT [Ticker],[Cotuccuanam] CoTuc2015 FROM [stox_tb_corp_DividendYield] WHERE [YearReport] = @YearReport + 1) TableTemp
	WHERE TableTemp.Ticker = #TABLETEMP.Stock

	-- doanh thu và loi nhuan ke hoach
	UPDATE #TABLETEMP SET  DoanhThuKeHoach = TableTemp.DoanhThu, LoiNhuanKeHoach =  LoiNhanTruocThue
	FROM (select [Ticker],DoanhThu DoanhThu, LoiNhuanTruocThue LoiNhanTruocThue FROM stox_tb_fund_BCTC_KeHoach WHERE YEAR(UpdateDate)  = @YearReport + 1) TableTemp
	WHERE TableTemp.Ticker = #TABLETEMP.Stock

	-- doanh thu và loi nhuan truoc thue
	UPDATE #TABLETEMP SET  DoanhThuLyKe = TableTemp.DoanhThu, LoiNhuanTruocThue =  TableTemp.LNST
	FROM (select [Ticker],SUM(F2_92) DoanhThu, SUM(F2_106) LNST FROM [stox_tb_fund_CompanyIncomeStatement] WHERE YearReport  = @YearReport + 1 AND LengthReport in (1,2,3,4) GROUP BY [Ticker]) TableTemp
	WHERE TableTemp.Ticker = #TABLETEMP.Stock
	
	-- Lich su kien 
	UPDATE #TABLETEMP SET  EventNote = [Note] 
	FROM (SELECT [Ticker] ,cast([Note] as nvarchar(max)) + ' ngày ' + cast(LEFT(CONVERT(VARCHAR, [ExDate], 103), 10) as nvarchar(10)) [Note]  FROM [stox_tb_Events] where EventCode = 'DIV' and ExDate >= getdate()) TableTemp
	WHERE TableTemp.Ticker = #TABLETEMP.Stock 


	Select Top (@TotalRow)
		Stock,
		Price,
		round(DIV2014,1) DIV2014,
		round(DIV2013,1) DIV2013,
		round(DIV2012,1) DIV2012,
		round(DIV2011,1) DIV2011,
		round(DIV2010,1) DIV2010,
		round(((CoTucKH2014 - (DIV2014 * 100)) / Price) * 100 , 1) AS Nam2014ChuaChia  ,
		round((CoTucKH2015 / Price) *100  ,1) AS Nam2015KeHoach,
		round((((CoTucKH2014 - (DIV2014 * 100)) / Price) * 100 ) + ((CoTucKH2015 / Price) *100) , 1) AS TongCong,
		round((CASE WHEN DoanhThuKeHoach = 0 THEN 0 ELSE DoanhThuLyKe/DoanhThuKeHoach END) * 100 ,1) as DoanhThuKeHoach,
		round((CASE WHEN LoiNhuanKeHoach = 0 THEN 0 ELSE LoiNhuanTruocThue/LoiNhuanKeHoach END) * 100 ,1) as LoiNhuanKeHoach,
		EventNote
		
		------ sample 
		--,CoTucKH2015,
		--DIV2014
	from #TABLETEMP
	WHere ((CASE WHEN DoanhThuKeHoach = 0 THEN 0 ELSE DoanhThuLyKe/DoanhThuKeHoach END) >= 0.4)
	And ((CASE WHEN LoiNhuanKeHoach = 0 THEN 0 ELSE LoiNhuanTruocThue/LoiNhuanKeHoach END) >= 0.4)
	order by Nam2015KeHoach DESC

END

------------------------------------------------------------------------

USE VfsCustomerService

ALTER TABLE [Customer]
ADD VType BIT NULL -- 1: khach hang VIP, 0 hoac Null khách hàng thuong

CREATE TABLE REPORTING (
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	Title nvarchar(512) NULL,
	UploadDir nvarchar(512) NULL,
	CreateDate [datetime2](7) NULL, -- ngày t?o báo cáo
	DateViewCustomer [datetime2](7) NULL, -- ngay KH Thu?ng xem báo cáo
	TotalDownload bigint default(0)
)


