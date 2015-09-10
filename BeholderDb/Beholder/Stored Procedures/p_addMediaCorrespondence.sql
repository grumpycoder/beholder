
/*
exec [Beholder].[p_addMediaCorrespondence] 
 @MediaTypeId=2
,@CorrespondenceTypeId=2
,@CorrespondenceName=N'Correspondence'
,@DateReceived='2014-01-02 00:00:00'
,@MovementClassId=1
,@ConfidentialityTypeId=4
,@ToName=N'You'
,@FromName=N'Me'
,@DateRenewalPermission=NULL
,@RenewalPermissionTypeId=NULL
,@RenewalPermission=NULL
,@Summary=N'this is a test'
,@City=NULL
,@County=NULL
,@StateId=NULL
,@RemovalStatusId=NULL
,@RemovalReason=NULL
,@DateCreated='2014-01-02 14:51:45.4771101'
,@CreatedUserId=1
,@DateModified='2014-01-02 14:51:45.4771101'
,@ModifiedUserId=1
,@DateDeleted=NULL
,@DeletedUserId=NULL
*/
-- =============================================
-- Author:		<Steven Jones>
-- Create date: <1/2/2014>
-- Description:	<This procedure will add records to Media Correspondence>
-- =============================================
CREATE PROCEDURE [Beholder].[p_addMediaCorrespondence] 
	@MediaTypeId int = null
   ,@CorrespondenceTypeid int = null
   ,@CorrespondenceName varchar(100) = null
   ,@DateReceived datetime2(7) = null
   ,@MovementClassId int = null
   ,@ConfidentialityTypeId int = null
   ,@ToName varchar(80) = null
   ,@FromName varchar(80) = null
   ,@DateRenewalPermission datetime2(7) = null
   ,@RenewalPermissionTypeId int = null
   ,@RenewalPermission varchar(500) = null
   ,@Summary varchar(4000) = null
   ,@City varchar(40) = null
   ,@County varchar(40) = null
   ,@StateId int = null
   ,@DateCreated datetime2(7) = null
   ,@CreatedUserId int = null
   ,@DateModified datetime2(7) = null
   ,@ModifiedUserId int = null
   ,@RemovalStatusId int = null
   ,@RemovalReason varchar(50) = null
   ,@DateDeleted datetime2(7) = null
   ,@DeletedUserId int = null

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    INSERT INTO Beholder.MediaCorrespondence ([MediaTypeId]
           ,[CorrespondenceTypeId]
           ,[CorrespondenceName]
           ,[DateReceived]
           ,[MovementClassId]
           ,[ConfidentialityTypeId]
           ,[ToName]
           ,[FromName]
           ,[DateRenewalPermission]
           ,[RenewalPermissionTypeId]
           ,[RenewalPermission]
           ,[Summary]
           ,[City]
           ,[County]
           ,[StateId]
           ,[DateCreated]
           ,[CreatedUserId]
           ,[DateModified]
           ,[ModifiedUserId]
           ,[RemovalStatusId]
           ,[RemovalReason]
           ,[DateDeleted]
           ,[DeletedUserId])
	VALUES (@MediaTypeId
           ,@CorrespondenceTypeId
           ,@CorrespondenceName
           ,@DateReceived
           ,@MovementClassId
           ,@ConfidentialityTypeId
           ,@ToName
           ,@FromName
           ,@DateRenewalPermission
           ,@RenewalPermissionTypeId
           ,@RenewalPermission
           ,@Summary
           ,@City
           ,@County
           ,@StateId
           ,@DateCreated
           ,@CreatedUserId
           ,@DateModified
           ,@ModifiedUserId
           ,@RemovalStatusId
           ,@RemovalReason
           ,@DateDeleted
           ,@DeletedUserId)

	SELECT SCOPE_IDENTITY() As Id

END


