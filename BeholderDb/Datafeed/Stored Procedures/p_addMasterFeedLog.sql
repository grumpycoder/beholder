


-- =============================================
-- Author:		Steven Jones
-- Create date: 2014-07-17
-- Description:	
-- =============================================
CREATE PROCEDURE [Datafeed].[p_addMasterFeedLog] 
	-- Add the parameters for the stored procedure here
	 @FeedTypeId int
	,@RecordCount int
	,@ErrorRecordCount int
	,@ValidRecordCount int
	,@IsSuccess bit
	,@IsDebug bit = 0
	,@EntryDate datetime = NULL
	,@FileHashValue varchar(512) = NULL
	,@FeedDate date = NULL
	,@MasterFeedLogId int OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	IF @EntryDate IS NULL
		SET @EntryDate = GETDATE()
	
		
    -- Insert statements for procedure here
	INSERT INTO DataFeed.MasterFeedLog
		(FeedTypeId, RecordCount, ErrorRecordCount, ValidRecordCount, IsSuccess, IsDebug, EntryDate, FeedDate, FileHashValue)
	VALUES
		(@FeedTypeId, @RecordCount, @ErrorRecordCount, @ValidRecordCount, @IsSuccess, @IsDebug, @EntryDate, @FeedDate, @FileHashValue)

	SET @MasterFeedLogId = SCOPE_IDENTITY()
	
END



