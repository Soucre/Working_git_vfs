IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VFS_DIV]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].VFS_DIV
GO

CREATE PROCEDURE [dbo].VFS_DIV
@YearReport int = 2014 -- test default
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
		CoTucKH2015 float
	)
	-- insert the current price into table 
	INSERT INTO #TABLETEMP 
		SELECT RTRIM(LTRIM(StockSymbol)) as StockSymbol, Last*10 , 0,0,0,0,0,0,0
		FROM [StoxData].[dbo].[stox_tb_HOSE_Trading]  
		where DateReport in (select top 1 DateReport from stox_tb_HOSE_Trading order by DateReport DESC) 
		union 
		select code, close_price ,0,0,0,0,0,0,0
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



	Select Top 20
		Stock ,
		Price ,
		DIV2014 ,
		DIV2013 ,
		DIV2012 ,
		DIV2011 ,
		DIV2010 ,
		round(((CoTucKH2014 - (DIV2014 * 100)) / Price) * 100 , 1) AS Nam2014ChuaChia  ,
		round(CoTucKH2015 / 100   ,1) AS Nam2015KeHoach,
		round((((CoTucKH2014 - (DIV2014 * 100)) / Price) * 100 ) + (CoTucKH2015 / 100) , 1) AS TongCong

	from #TABLETEMP
	order by TongCong DESC


END