/****** Object:  Stored Procedure [dbo].ContentTemplateGetList    Script Date: 28 May 2009 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[spReportGetList]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[spReportGetList]
GO
	
CREATE PROCEDURE [dbo].[spReportGetList]
	@OrderBy varchar(50),
	@OrderDirection varchar(5),
	@Page int,
	@PageSize int,
	@TotalRecords int output
AS

SET TRANSACTION ISOLATION LEVEL READ COMMITTED

CREATE TABLE #ReportTemplate (
	RowNumber INT IDENTITY (1, 1) NOT NULL,
	ReportID bigint	
)

DECLARE @sql nvarchar(2000)
DECLARE @Top int

SET @Top = @Page*@PageSize
IF @PageSize > 0
   SET ROWCOUNT @Top
-- insert primary keys into temp table
SET @sql =	N'INSERT INTO #ReportTemplate ([ReportID]) SELECT '
SET @sql = @sql + ' [Id] FROM [dbo].[Report]  ORDER BY [' + @OrderBy + N'] ' + @OrderDirection
EXEC (@sql)

SET ROWCOUNT 0

SELECT @TotalRecords = COUNT(*) FROM Report

SELECT
	[Id]
      ,[Title]
      ,[UploadDir]
      ,[DateViewCustomer]
      ,[TotalDownload]
      ,[FileSize]
      ,[IdReportType]
      ,[Ticker]
      ,[CreateDate]
      ,[FileType]
FROM
	#ReportTemplate AS tblTemp JOIN [dbo].Report ON
	tblTemp.ReportID = [dbo].Report.Id
WHERE (@PageSize = 0) OR (@PageSize > 0 AND (@Page - 1)*@PageSize < RowNumber AND RowNumber <= @Page*@PageSize)
ORDER BY RowNumber

DROP TABLE #ReportTemplate

GO

	
