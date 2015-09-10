


-- =============================================
-- Author:        Steven Jones
-- Modified date: 2014-07-17
-- Description:   Check if the DataFeed is already been processed.
-- =============================================
CREATE PROCEDURE [Datafeed].[p_getPublishedDataFeedSummary]
	 @FeedTypeValue		varchar(100) = NULL
	,@LogId   			int          = NULL
	,@RecordsProcessed  int          = null OUTPUT
AS
BEGIN
	-- we don't need row counts
	SET NOCOUNT ON;
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
	-- hack to circumvent Visual Studio bug, has no effect on SQL
	IF 1=0 BEGIN SET FMTONLY OFF END
	
	DECLARE @DataFeedTypeId int
	
	SELECT DISTINCT @DataFeedTypeId = Id
	  FROM Beholder.DataFeed.FeedType
	 WHERE Name = @FeedTypeValue

    SELECT @RecordsProcessed = ValidRecordCount
	  FROM DataFeed.MasterFeedLog 
     WHERE MasterFeedLogId = @LogId
END

