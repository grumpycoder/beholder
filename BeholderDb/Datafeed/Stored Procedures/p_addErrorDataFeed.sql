

-- =============================================
-- Author:        Steven Jones
-- Modified date: 2014-07-20
-- Description:   Add datafeed error records.
-- =============================================
CREATE PROCEDURE [Datafeed].[p_addErrorDataFeed] 
	-- Add the parameters for the stored procedure here
	 @MasterFeedLogId int
	,@RecordSets xml
	,@ProcessedDate datetime = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO DataFeed.ErrorDataFeed
		(MasterFeedLogId, RecordSets, ProcessedDate)
	VALUES
		(@MasterFeedLogId, @RecordSets, ISNULL(@ProcessedDate, GETDATE()))
END


