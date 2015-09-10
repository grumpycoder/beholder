

-- =============================================
-- Author:        Steven Jones
-- Modified date: 2014-07-17
-- Description:   Update the filehashvalue for the datafeed.
-- =============================================



CREATE PROCEDURE [Datafeed].[p_updDataFeedFileHashValue]
   @FileHashValue varchar(512) = NULL,
   @FeedTypeValue varchar(100) = NULL
AS
BEGIN
    -- we don't need row counts
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
    -- hack to circumvent Visual Studio bug, has no effect on SQL
    IF 1=0 BEGIN SET FMTONLY OFF END
  
   UPDATE mstlog 
      SET FileHashValue = @FileHashValue
     FROM DataFeed.MasterFeedLog mstlog
	 JOIN DataFeed.FeedType ft
	   ON ft.id = mstlog.FeedTypeId
    WHERE CAST(mstlog.EntryDate AS DATE) = CAST(GETDATE() AS DATE)
     AND ft.Name = @FeedTypeValue

END

