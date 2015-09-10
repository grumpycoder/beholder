
-- =============================================
-- Author:		Steven Jones
-- Create date: 2014-07-20
-- Description: Check if the published Datafeed is already been processed.
-- =============================================
CREATE PROCEDURE [Datafeed].[p_getIsDataFeedProcessed]
   @FileHashValue varchar(512) = NULL
  ,@FeedTypeValue varchar(100)
  ,@IsProcessed   bit = NULL output
AS
BEGIN
    -- we don't need row counts
    SET NOCOUNT ON;
    SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
    -- hack to circumvent Visual Studio bug, has no effect on SQL
    IF 1=0 BEGIN SET FMTONLY OFF END
  
  IF EXISTS(SELECT 1 
              FROM DataFeed.MasterFeedLog mstlog 
              JOIN Beholder.Datafeed.FeedType ft
                ON ft.Id = mstlog.FeedTypeId
             WHERE mstlog.FileHashValue = @FileHashValue 
			   AND ft.Name = @FeedTypeValue
           )
    BEGIN
      SET @IsProcessed = 1
    END 
  ELSE
    BEGIN
	  SET @IsProcessed = 0
    END
 
   return @IsProcessed 
   
END



