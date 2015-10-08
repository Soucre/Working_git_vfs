/****** Script for SelectTopNRows command from SSMS  ******/

-- Lưu ký chứng khoán
SELECT TOP 1000 DP.[Id]
      ,[Status]
      ,[TransactionDate]
      ,[Notes]
      ,[CustomerId]
      ,StockCode
      ,[Volume]
      ,[Value]
      ,[StockStatus]
      ,[DepositoryStockType]
      ,[Approved]
      ,[TransactionNumber]
      ,[OrderNo]
      ,[CertificateNumber]
      ,[UserCreate]
      ,[AvgPrice]
      ,[AcceptDate]
      ,[SecuritiesType]
      ,[ActiveDate]
  FROM [Depository] DP, GlStockCode GLS
  
  WHERE customerId = '094C003379'
  And DP.StockId = GLS.Id
  and [status] <> 'X'
  Order by [TransactionDate]
  
 
  -- co tuc
  	SELECT a.DatePay AS TransactionDate,
			a.DateNoRight,
			a.DateOwnerConfirm,
			a.DatePay,
			   b.CustomerId,
			   a.StockCode, 
			   a.StockType, 
			   a.BoardType,
			   a.RightType,
			   b.RegisteredQuantity AS Quantity,
			   CAST(a.RightExecPrice AS DECIMAL(28, 0)) AS Price,
			   'C' AS DebitOrCreadit,
			   0 AS CurrentQuantity,
			   0 AS BeginQuantity,
			   'RightExecRegister' AS TaskCode,
			   a.[Description],
			   0 AS RemainQuantityOfDay,
			   0 AS FeeRate,
			   0 AS AvgPrice
			   
		FROM RightExec a
		INNER JOIN RightExecRegister b ON a.Id = b.RightExecId
		INNER JOIN Customers c ON b.CustomerId = c.CustomerId
		WHERE a.RightType = 'R' AND b.RegisteredQuantity <> 0
			--AND (@StockCode = '' OR a.StockCode = @StockCode)
			--AND (@CustomerId = '' OR b.CustomerId = '094C003379')
			AND ( b.CustomerId = '094C003379')
			--AND a.DatePay BETWEEN @FromDate AND @ToDate
			--AND (@BranchCode = '' OR c.BranchCode = @BranchCode)
			UNION ALL
		-- Cổ tức bẳng cổ phiếu và Tiền	
				SELECT a.DatePay AS TransactionDate,
				   a.DateNoRight,
			   a.DateOwnerConfirm,
			a.DatePay,
			   b.CustomerId, 
			   a.StockCode, 
			   a.StockType, 
			   a.BoardType,
			   a.RightType,
			   CAST(ROUND((b.Quantity * a.RateA) / a.RateB, 0) AS DECIMAL(28, 0)) AS Quantity,
			   CAST(0 AS DECIMAL(28, 0)) AS Price,
			   'C' AS DebitOrCreadit,
			   0 AS CurrentQuantity,
			   0 AS BeginQuantity,
			   'RightExec' AS TaskCode,
			   a.[Description],
			   0 AS RemainQuantityOfDay,
			   0 AS FeeRate,
			   0 AS AvgPrice
			     
			
		FROM RightExec a
		INNER JOIN RightExecList b ON a.Id = b.RightExecId
		INNER JOIN Customers c ON b.CustomerId = c.CustomerId
		WHERE a.RightType IN( 'S','M')
		--	AND (@CustomerId = '' OR b.CustomerId = @CustomerId)
		AND (b.CustomerId = '094C003379')
			--AND (@StockCode = '' OR a.StockCode = @StockCode)
			--AND a.DatePay BETWEEN @FromDate AND @ToDate
			--AND (@BranchCode = '' OR c.BranchCode = @BranchCode)
		order by a.DateNoRight
		
		
		/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [Id]
      ,[OrderDate]
      ,[OrderNo]
      ,[ConfirmNo]
      ,[ConfirmTime]
      ,[BranchCode]
      ,[TradeCode]
      ,[OrderSeq]
      ,[OrderSide]
      ,[CustomerId]
      ,[CustomerBranchCode]
      ,[CustomerTradeCode]
      ,[BoardType]
      ,[StockCode]
      ,[StockType]
      ,[MatchedVolume]
      ,[MatchedPrice]
      ,[MatchedValue]
      ,[FeeRate]
      ,[NoPost]
      ,[Status]
      ,[IsCross]
      ,[T]
      ,[T3]
      ,[Deferred]
      ,[ClearingCode]
      ,[DayId]
      ,[DeferredAmount]
      ,[TransactionDate]
      ,[SpecialFee]
      ,[AccountId]
      ,[MarginType]
  FROM [SFS].[dbo].[TradingResultHist]
  WHERE CustomerId = '094C003379'
  order by TransactionDate, OrderSide