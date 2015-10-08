CREATE TABLE [dbo].[VFS_Report_LaiLo_Customer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL Primary key,
	[TransactionDate] [varchar](14) NOT NULL,
	[ActiveDate] [varchar](14)  NULL, -- ngày hiệu lực để tính chứng khoán giao dịch và chờ gd
	[CreditOrDebit] [char](1) NULL, -- mua hay bán, ưu tiên sort theo mua C: tăng, D: giam
	[CustomerId] varchar(10) NOT NULL, -- mã tài khoản
	StockCode varchar(10) NOT NULL, -- mã CK
	[Volume] decimal(18,0) not NULL,
	TransactionType varchar(2) NOT NULL, -- loại giao dịch
										-- LK : Luu Ky (CreditOrDebit = C)
										-- QS : Quyền thưởng cổ phiếu (CreditOrDebit = C)
										-- QM : Quyền thưởng bằng tiền (CreditOrDebit = C)
										-- QR : Quyền mua thêm (CreditOrDebit = C)
										-- B : Mua cổ phiếu (CreditOrDebit = C)
										-- S : Bán cổ phiếu  (CreditOrDebit = D)
	CKGiaoDich decimal(18,0) not NULL, -- chứng khoán giao dịch ảnh hưởng bởi ngày [ActiveDate]
	CKChoGiaoDich decimal(18,0) not NULL, -- chứng khoán chờ giao dịch ảnh hưởng bởi ngày [ActiveDate]
	GiaThucHien decimal(18,3) not NULL Default(0),
	GiaBan decimal(18,3) NULL, -- giá khi bán cổ phiếu ra
	KhoiLuongNhapKho decimal(18,0) NOT NULL default(0), -- Khối lượng nhập vào là +, ban ra là -
	KhoiLuongTonKho decimal(18,0) NOT NULL default(0), -- Khối lượng tồn =  KL tồn trước đó + KL nhập kho
	GiaTriNhapKho decimal(28,0) NOT NULL default(0), -- Gia tri nhập kho =  GiaThucHien * KhoiLuongNhapKho
	GiaTriTonKho decimal(28,0) NOT NULL default(0), -- Gia tri tồn kho =  Giá trị tồn kho trước đó + Gia tri nhập kho
	GiaTrungBinh decimal(18,3) NOT NULL default(0), -- KhoiLuongTonKho / GiaTriTonKho
	RunAgain char(1) NULL, -- mỗi ngày phải check lại để chuyển về CK giao dịch
	LastVolumeBlance char(1), -- Thể hiện tồn kho cuối
	FeeRate decimal(10,5) NULL, -- phần trăm phí mua bán
	TaxRate decimal(10,5) NULL,
	FeeRateValue decimal(28,0) NULL, -- Gia Tri Phi
	TaxRateValue decimal(28,0) NULL	-- Gia tri thuế
)
 
--DROP TABLE [VFS_Report_LaiLo_Customer]



-- store procedure lấy tăng giảm cổ phiếu
-- by Nguyen Chi Hieu
-- Chay trong database SBS
-- Chay hang ngay

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[GetDataForAvgPrice]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDataForAvgPrice]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetDataForAvgPrice]
	@CustomerId VARCHAR(10) = '094C000015',
	@FromDate	SMALLDATETIME = '2008-01-25',
	@ToDate		SMALLDATETIME = '2014-04-20'
	--WITH ENCRYPTION
AS

BEGIN 
	CREATE TABLE #VFS_Report_LaiLo_Customer(
	--Id bigint IDENTITY(1,1) NOT NULL Primary key,
	TransactionDate SMALLDATETIME NOT NULL,
	ActiveDate SMALLDATETIME  NULL, -- ngày hiệu lực để tính chứng khoán giao dịch và chờ gd
	CreditOrDebit char(1) NULL, -- mua hay bán, ưu tiên sort theo mua C: tăng, D: giam
	CustomerId varchar(10) NOT NULL, -- mã tài khoản
	StockCode varchar(10) NOT NULL, -- mã CK
	Volume decimal(18,0) not NULL,	
	Price decimal(18,3) NULL, -- giá khi bán cổ phiếu ra	
	TransactionType varchar(2) NOT NULL,
	FeeRate decimal(10,5) NULL, -- phần trăm phí mua bán
	TaxRate decimal(10,5) NULL, -- phần trăm thuế
	MatchedValue decimal(28,0) default(0) -- giá trị mua bán	
)

-- thông tin luu ky

INSERT INTO #VFS_Report_LaiLo_Customer 
	SELECT TransactionDate
			,CASE WHEN ActiveDate IS NULL THEN AcceptDate ELSE ActiveDate END
			,'C'
			,CustomerId
			,GLS.StockCode
			,Volume
			,CASE WHEN AvgPrice = 0 OR AvgPrice IS NULL THEN 0 ELSE AvgPrice/1000 END
			,'LK'
			,0
			,0
			,0
	FROM Depository DP, GlStockCode GLS  
	WHERE   customerId = @CustomerId 
			AND DP.StockId = GLS.Id
			AND [Status] <> 'X'
			AND TransactionDate BETWEEN @FromDate AND @ToDate 
			

-- Thong tin mua ban Current
	INSERT INTO #VFS_Report_LaiLo_Customer 
	SELECT TransactionDate
	,CASE WHEN OrderSide = 'B' THEN [dbo].[TransDatePlus](TransactionDate,3,0) ElSE TransactionDate END 
	,CASE WHEN OrderSide = 'B' THEN 'C' ElSE 'D' END
	,CustomerId
	,StockCode
	,CASE WHEN OrderSide = 'B' THEN MatchedVolume ElSE -MatchedVolume END 
	,MatchedPrice
	,OrderSide
	,CASE WHEN OrderSide = 'B' THEN FeeRate ElSE -FeeRate END 
	,CASE WHEN OrderSide = 'B' THEN 0 ElSE -0.001 END
	,(CASE WHEN OrderSide = 'B' THEN MatchedValue ElSE -MatchedValue END)/1000
	FROM TradingResult
	WHERE customerId = @CustomerId 
			AND TransactionDate BETWEEN @FromDate AND @ToDate 


-- 
-- Thong tin mua ban HIST
	INSERT INTO #VFS_Report_LaiLo_Customer 
	SELECT TransactionDate
	,CASE WHEN OrderSide = 'B' THEN [dbo].[TransDatePlus](TransactionDate,3,0) ElSE TransactionDate END 
	,CASE WHEN OrderSide = 'B' THEN 'C' ElSE 'D' END
	,CustomerId
	,StockCode
	,CASE WHEN OrderSide = 'B' THEN MatchedVolume ElSE -MatchedVolume END 
	,MatchedPrice
	,OrderSide
	,CASE WHEN OrderSide = 'B' THEN FeeRate ElSE -FeeRate END 
	,CASE WHEN OrderSide = 'B' THEN 0 ElSE -0.001 END
	,(CASE WHEN OrderSide = 'B' THEN MatchedValue ElSE -MatchedValue END)/1000
	FROM TradingResultHist
	WHERE customerId = @CustomerId 
			AND TransactionDate BETWEEN @FromDate AND @ToDate 

 
---- Thông tin quyền phát hành thêm

	INSERT INTO #VFS_Report_LaiLo_Customer 
	SELECT RegisterDate,
			CASE WHEN ActiveDate IS NULL THEN a.DatePay ELSE ActiveDate END,
			'C',
			b.CustomerId,
			a.StockCode, 
			b.RegisteredQuantity AS Quantity,
			CAST(a.RightExecPrice AS DECIMAL(28, 0))/1000,
			'QR',
			0,
			0,
			0			
	FROM RightExec a
	INNER JOIN RightExecRegister b ON a.Id = b.RightExecId
	INNER JOIN Customers c ON b.CustomerId = c.CustomerId
	WHERE a.RightType = 'R' AND b.RegisteredQuantity <> 0			
		AND ( b.CustomerId = @CustomerId )
		AND RegisterDate BETWEEN @FromDate AND @ToDate 

---- Thông tin quyền Stock
	
INSERT INTO #VFS_Report_LaiLo_Customer 
	SELECT a.DateOwnerConfirm,
	CASE WHEN ActiveDate IS NULL THEN a.DatePay ELSE ActiveDate END,
	'C',
	 b.CustomerId, 
	  a.StockCode,
	  CAST(ROUND((b.Quantity * a.RateA) / a.RateB, 0) AS DECIMAL(28, 0)),
	0,
	'QS',
	0,
	0,
	0
	
	FROM RightExec a
		INNER JOIN RightExecList b ON a.Id = b.RightExecId
		WHERE a.RightType IN( 'S')
		AND a.DateOwnerConfirm BETWEEN @FromDate AND @ToDate 
		AND (b.CustomerId = @CustomerId )

	---- Thông tin quyền Money
	
INSERT INTO #VFS_Report_LaiLo_Customer 
	SELECT a.DateOwnerConfirm,
	a.DateOwnerConfirm,
	'C',
	b.CustomerId, 
	a.StockCode,
	 CAST(ROUND((b.Quantity * a.RateA) / a.RateB, 0) AS DECIMAL(28, 0)),
	10,
	'QM',
	0,
	0.05,
	(CAST(ROUND((b.Quantity * a.RateA) / a.RateB, 0) AS DECIMAL(28, 0))*10)
	FROM RightExec a
		INNER JOIN RightExecList b ON a.Id = b.RightExecId		
		WHERE a.RightType IN( 'M')
		AND a.DateOwnerConfirm BETWEEN @FromDate AND @ToDate 
		AND (b.CustomerId = @CustomerId )
	---- Huy niem yet, hoac chuyen khoan chung khoan
	/*
	INSERT INTO #VFS_Report_LaiLo_Customer 
	SELECT cd.TransactionDate,
	cd.TransactionDate,
	'C',
	replace(cd.AccountId,'K','C'), 
	  cs.StockCode,
	  cs.Quantity ,
	0,
	'C',
	0,
	0,
	0
	
	FROM ContigenDayHist cd
		INNER JOIN ContigenStockDayHist cs
			ON cd.TransactionDate = cs.TransactionDate
				AND cd.TransactionNumber = cs.TransactionNumber
	WHERE cd.Approved != 'X'
	And  cd.AccountId = @CustomerId
	And cd.TransactionDate BETWEEN @FromDate AND @ToDate 
	And cs.BankGL IN ('012121' ,'012723')
	And TransactionCode = ''
	And (Description like '%nhan%' or Description like '%Thuc hien quyen%')
	Order by cd.TransactionDate

	*/			
SELECT * FROM #VFS_Report_LaiLo_Customer ORDER BY TransactionDate, CreditOrDebit
END

---------------------------
-- chay database Report
-- By Nguyen Chi Hieu
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[GetDataLaiLoDaThucHien]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDataLaiLoDaThucHien]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetDataLaiLoDaThucHien]
	@CustomerId VARCHAR(10),
	@FromDate	int ,
	@ToDate		int
	--WITH ENCRYPTION
AS
BEGIN
		  SELECT StockCode,
			 -SUM(Volume) AS KLBan, 
			 AVG(GiaTrungBinh)*1000 AS GiaTrungBinh,
			 AVG(GiaBan)*1000 AS GiaBan,
			 (AVG(GiaBan) - AVG(GiaTrungBinh))* (-SUM(Volume))*1000 AS LaiLo
			 
	  FROM [VFS_Report_LaiLo_Customer]
	  WHERE TransactionDate between @FromDate  and @ToDate
	  AND ActiveDate <= @ToDate
	  AND CustomerId = @CustomerId 
	  And CreditOrDebit = 'D'  --AND StockCode = 'VNM'
	  GROUP BY StockCode
	  Order by StockCode
END
---------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[GetDataPhiVaThueGiaoDich]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetDataPhiVaThueGiaoDich]
GO
/****** Object:  StoredProcedure [dbo].[Do_AvgPrice]    Script Date: 08/19/2009 11:52:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO  
CREATE PROCEDURE [dbo].[GetDataPhiVaThueGiaoDich]
	@CustomerId VARCHAR(10),
	@FromDate	int ,
	@ToDate		int
	--WITH ENCRYPTION
AS
BEGIN
			SELECT 
			 SUM(FeeRateValue)*1000 AS FeeRateValue, 
			SUM(TaxRateValue)*1000 AS TaxRateValue
			 
	  FROM [VFS_Report_LaiLo_Customer]
	  WHERE TransactionDate between @FromDate  and @ToDate
	  AND ActiveDate <= @ToDate
	  AND CustomerId = @CustomerId 
	  And TransactionType IN ('B','S') 
	  
	  UNION ALL
	  
	  		SELECT 
			SUM(CKGiaoDich*10)*1000 , 
			SUM(TaxRateValue)*1000
			 
	  FROM [VFS_Report_LaiLo_Customer]
	  WHERE TransactionDate between @FromDate  and @ToDate
	  AND ActiveDate <= @ToDate
	  AND CustomerId = @CustomerId 
	  And TransactionType IN ('QM') 
END


---------------------------------------------------------------
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'getNoDaTraVaPhi') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].getNoDaTraVaPhi
GO
/****** Object:  StoredProcedure [dbo].[Do_AvgPrice]    Script Date: 08/19/2009 11:52:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].getNoDaTraVaPhi
	@CustomerId VARCHAR(10),
	@FromDate	VARCHAR(10) ,
	@ToDate		VARCHAR(10)
	--WITH ENCRYPTION
AS
BEGIN
		   SELECT SUM(AmountCalInterest) AS SoTienTraNo,
				SUM(InterestPayNow) AS PhiDaTra
		  FROM MAccDetailLog MDL 
		  WHERE 
		  CustomerId = @CustomerId 
		  And (LogDate Between @FromDate and @ToDate)
		  And  MDL.[Status] IN ('C')
  
  UNION ALL
  
		  SELECT SUM(AdvanceAmount),
				SUM(AdvanceFee )
		  FROM dbo.BuyCashContract 
		  WHERE  CustomerId = @CustomerId
		  And (PaymentDate Between @FromDate and @ToDate) and [Status] = 'E'
		  
  UNION ALL
    
		  SELECT SUM(AdvanceAmount),
				SUM(0)
		  FROM dbo.AdvanceContractAll 
		  WHERE CustomerId = @CustomerId 
		 And (PaymentDate Between @FromDate and @ToDate) and [Status] = 'E'
  
END
---------------------------------------------------------------------------------

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'CK_CoSan_Trong_TaiKhoan') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].CK_CoSan_Trong_TaiKhoan
GO
/****** Object:  StoredProcedure [dbo].[CK_CoSan_Trong_TaiKhoan]    Script Date: 21/04/2014  ******/

/****** Script for những CK có sẵn trong tài khoản  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].CK_CoSan_Trong_TaiKhoan
	@CustomerId VARCHAR(10),
	@ToDate	VARCHAR(10) 
	
	--WITH ENCRYPTION
AS

BEGIN 

	/****** Script for những CK có sẵn trong tài khoản  ******/
	SELECT  StockCode, KhoiLuongTonKho, GiaTrungBinh * 1000 AS GiaTrungBinh
	  FROM [VFS_Report_LaiLo_Customer]
	  WHERE CustomerId= @CustomerId AND KhoiLuongTonKho <> 0
	  --AND TransactionDate <= 20140327 
		AND id IN (  SELECT  (SELECT MAX(SUB.Id) /* lấy max id của cổ phiếu */
									FROM [VFS_Report_LaiLo_Customer] SUB 
									WHERE SUB.CustomerId= @CustomerId 
										AND SUB.TransactionDate <= @ToDate												
										AND SUB.StockCode = MAN.StockCode) 
					  FROM [VFS_Report_LaiLo_Customer] MAN /* lấy danh mục cổ phiếu cổ phiếu */
					  WHERE MAN.CustomerId= @CustomerId 
					  AND MAN.TransactionDate <= @ToDate   
					  AND MAN.KhoiLuongTonKho <> 0
					  GROUP BY MAN.StockCode )

END
GO

-------------------------------------------------------------------------------------------------------

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'CK_ChuaVe_TaiKhoan') AND type in (N'P', N'PC'))
DROP PROCEDURE dbo.CK_ChuaVe_TaiKhoan
GO
/****** Object:  StoredProcedure dbo.    Script Date: 21/04/2014  ******/

/****** Script for những CK chưa về tài khoản  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE dbo.CK_ChuaVe_TaiKhoan
	@CustomerId VARCHAR(10),
	@FromDate VARCHAR(10) ,
	@ToDate	VARCHAR(10) 
	
	--WITH ENCRYPTION
AS

BEGIN 

	/****** Script for những CK có sẵn trong tài khoản  ******/
SELECT  MAN.StockCode, SUM(MAN.KhoiLuongTonKho) AS KhoiLuongTonKho,
		 (SELECT MAX(SUB.GiaTrungBinh)
		  FROM  VFS_Report_LaiLo_Customer SUB
		  WHERE SUB.CustomerId= @CustomerId
		  AND (SUB.ActiveDate Between @FromDate And  @ToDate)
		  AND SUB.TransactionType = 'B'
		  AND SUB.StockCode = MAN.StockCode) * 1000 AS GiaTrungBinh
		  
  FROM  VFS_Report_LaiLo_Customer MAN
  WHERE MAN.CustomerId= @CustomerId
  AND (MAN.ActiveDate Between @FromDate And  @ToDate)
  AND MAN.TransactionType = 'B'
  GROUP BY MAN.StockCode

END
GO

---------------------------------------------------------------