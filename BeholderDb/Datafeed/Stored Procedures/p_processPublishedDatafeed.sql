
-- =============================================
-- Author:		Steven Jones
-- Create date: 2014-07-20
-- Description:	Process the Published Datafeed records
-- =============================================
CREATE PROCEDURE [Datafeed].[p_processPublishedDatafeed]
    @FeedDate date = NULL,
    @Debug bit =0,
	@LogId int = 0 OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	-- SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

	-- hack to circumvent Visual Studio bug, has no effect on SQL
	IF 1=0 BEGIN SET FMTONLY OFF END
	--Declare variables
	DECLARE @DebugLocal        bit = @Debug
	
	DECLARE @RecordCount       int
	DECLARE @ErrorRecordCount  int
	DECLARE @ValidRecordCount  int
	DECLARE @FeedTypeId        int
	DECLARE @CreatedOn         datetime = getdate()
	DECLARE @UserId            int
	DECLARE @ErrorText         varchar(255)
	
	DECLARE @tblPublished table ( Id int identity(1,1)
	                        ,BatchId int
	                        ,BatchNumber varchar(30)
	                        ,PublicationInfo varchar(200)
							,PublicationDate datetime
							,DateReceived datetime
							,MovementClassId int
							,FileLocation varchar(512) )
	                        
	-- variables used when processing records
	DECLARE @Id                int
	DECLARE @BatchId           int
    DECLARE @BatchNumber       varchar(30)
	DECLARE @PublicationInfo   varchar(200)
	DECLARE @PublicationDate   datetime
	DECLARE @DateReceived      datetime
	DECLARE @MovementClassId   int
	DECLARE @FileLocation      varchar(512)
	
   BEGIN TRY
		    
      SET @RecordCount      = 0
      SET @ErrorRecordCount = 0
      SET @ValidRecordCount = 0

      SELECT DISTINCT @FeedTypeId = Id
        FROM Datafeed.FeedType
       WHERE Name = 'Published'

      SELECT @UserId = Id
        FROM [Security].[User]
       WHERE UserName = 'cuser'
	
      SELECT @RecordCount = COUNT(*) FROM DataFeed.Published
		
      INSERT INTO DataFeed.MasterFeedLog
         (FeedTypeId, RecordCount, ErrorRecordCount, ValidRecordCount, IsSuccess, IsDebug, EntryDate, FeedDate)
      VALUES 
         (@FeedTypeId, @RecordCount, 0, 0, 0, ISNULL(@DebugLocal, 0), GETDATE(), @FeedDate)

      SELECT @LogId = SCOPE_IDENTITY()

	  -- Error handling would go here.  Nothing has been specificied to check at this point
      --INSERT INTO DataFeed.ErrorDatafeed (MasterFeedLogId, ProcessedDate, Comment, RecordSets)
      --SELECT @LogId, GETDATE(), Comment,
      --       (SELECT t.* FOR XML RAW('DataItem')) RecordSet 
      --        FROM ( SELECT feed.*,
      --                  CASE
      --                     WHEN ISNUMERIC(ISNULL(feed.BatchNumber, -99)) != 1 THEN 'Invalid Batch Number'
      --                     WHEN feed.FileLocation IS NULL THEN 'Invalid FileLocation'
      --                  END [Comment]
			   --        FROM DataFeed.Published feed
			   --       WHERE ( ISNUMERIC(ISNULL(feed.BatchNumber, -99 ) ) != 1
			   --          OR   feed.FileLocation IS NULL ) ) t
			
      --DECLARE @ErrorCount int = @@RowCount

      UPDATE DataFeed.MasterFeedLog 
	     SET ErrorRecordCount = 0
            ,ValidRecordCount = @RecordCount - 0
       WHERE MasterFeedLogId = @LogId

	  -- Error handling would go here.  Nothing has been specificied to check at this point
    --  DELETE feed
    --    FROM DataFeed.Published feed
	   --WHERE ( ISNUMERIC(ISNULL(feed.BatchNumber, -99 ) ) != 1
    --      OR   feed.FileLocation IS NULL )
	  SELECT Id
	        ,BatchId
			,BatchNumber
			,PublicationInfo
			,PublicationDate
			,DateReceived
			,MovementClassId
			,FileLocation
		FROM Datafeed.Published

	END TRY
	
	BEGIN CATCH
      DECLARE @ErrorMessage NVARCHAR(4000);
      DECLARE @ErrorSeverity INT;
      DECLARE @ErrorState INT;

      SELECT 
         @ErrorMessage = ERROR_MESSAGE(),
         @ErrorSeverity = ERROR_SEVERITY(),
         @ErrorState = ERROR_STATE();

      SET @ErrorText = @ErrorMessage
      
      INSERT INTO DataFeed.ErrorDataFeed (MasterFeedLogId, ProcessedDate, Comment)
      SELECT @LogId, GETDATE(), @ErrorText 
					
   END CATCH		
		
END



