-- =============================================
-- Author:		Mark Lawrence
-- Create date: 4/24/2014
-- Description:	Update ActiveStatus to review for defined ActiveYear for later review process
-- =============================================
/*
exec Beholder.p_ApprovalReset 2013, 4
*/
CREATE PROCEDURE [Beholder].[p_ApprovalReset]
	@activeYear int, 
	@userId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO Beholder.OrganizationStatusHistory
		(OrganizationId, ActiveStatusId, ActiveYear, ReportStatusFlag, DateCreated, DateModified, CreatedUserId, ModifiedUserId)
	SELECT Id, ActiveStatusId, ActiveYear, ReportStatusFlag, GETDATE(), GETDATE(), @userId, @userId
	FROM Beholder.Organization
	WHERE ActiveYear = @activeYear AND ActiveStatusId = 1 AND ApprovalStatusId = 1

	INSERT INTO Beholder.ChapterStatusHistory
		(ChapterId, ActiveStatusId, ActiveYear, ReportStatusFlag, DateCreated, DateModified, CreatedUserId, ModifiedUserId)
	SELECT Id, ActiveStatusId, ActiveYear, ReportStatusFlag, GETDATE(), GETDATE(), @userId, @userId
	FROM Beholder.Chapter
	WHERE ActiveYear = @activeYear AND ActiveStatusId = 1 AND ApprovalStatusId = 1

	INSERT INTO Beholder.EventStatusHistory
		(EventId, ActiveStatusId, ActiveYear, ReportStatusFlag, DateCreated, DateModified, CreatedUserId, ModifiedUserId)
	SELECT Id, ActiveStatusId, ActiveYear, ReportStatusFlag, GETDATE(), GETDATE(), @userId, @userId
	FROM Beholder.Event
	WHERE ActiveYear = @activeYear AND ActiveStatusId = 1 AND ApprovalStatusId = 1

	UPDATE Beholder.Organization SET 
		ActiveStatusId = 3, 
		ModifiedUserId = @userId
	WHERE ActiveYear = @activeYear AND ActiveStatusId = 1 AND ApprovalStatusId = 1
	

	UPDATE Beholder.Chapter SET 
		ActiveStatusId = 3, 
		ModifiedUserId = @userId
	WHERE ActiveYear = @activeYear AND ActiveStatusId = 1 AND ApprovalStatusId = 1
	

	UPDATE Beholder.MediaWebsiteEGroup SET 
		ActiveStatusId = 3, 
		ModifiedUserId = @userId
	WHERE ActiveYear = @activeYear AND ActiveStatusId = 1 AND ApprovalStatusId = 1
	

END

