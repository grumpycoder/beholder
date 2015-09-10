




CREATE VIEW [Beholder].[v_PendingRemoval]
AS

	SELECT 'Person' Type, bper.Id, ISNULL(per.LName + ', ','') + ISNULL(per.FName,'') + ISNULL(' '+per.MName,'') Name, RemovalReason, u.UserName
	FROM Beholder.Person bper
	JOIN Common.Person   per
	  ON per.Id = bper.PersonId  
	JOIN Security.[User] u ON u.Id = bper.ModifiedUserId
	WHERE RemovalStatusId IS NOT NULL AND bper.DateDeleted IS NULL
	UNION ALL
	SELECT 'Organization' Type, t.Id, OrganizationName Name, RemovalReason, u.UserName
	FROM Beholder.Organization  t 
	JOIN Security.[User] u ON u.Id = t.ModifiedUserId
	WHERE RemovalStatusId IS NOT NULL AND t.DateDeleted IS NULL
	UNION ALL
	SELECT 'Chapter' Type, t.Id, ChapterName Name, RemovalReason, u.UserName
	FROM Beholder.Chapter  t 
	JOIN Security.[User] u ON u.Id = t.ModifiedUserId
	WHERE RemovalStatusId IS NOT NULL AND t.DateDeleted IS NULL
	UNION ALL
	SELECT 'Event' Type, t.Id, EventName Name, RemovalReason, u.UserName
	FROM Beholder.Event t 
	JOIN Security.[User] u ON u.Id = t.ModifiedUserId
	WHERE RemovalStatusId IS NOT NULL AND t.DateDeleted IS NULL
	UNION ALL
	SELECT 'Image' Type, t.Id, ImageTitle Name, RemovalReason, u.UserName
	FROM Beholder.MediaImage t 
	JOIN Security.[User] u ON u.Id = t.ModifiedUserId
	WHERE RemovalStatusId IS NOT NULL AND t.DateDeleted IS NULL
	UNION ALL
	SELECT 'AudioVideo' Type, t.Id Id, Title Name, RemovalReason, u.UserName
	FROM Beholder.MediaAudioVideo t 
	JOIN Security.[User] u ON u.Id = t.ModifiedUserId
	WHERE RemovalStatusId IS NOT NULL AND t.DateDeleted IS NULL
	UNION ALL
	SELECT 'Correspondence' Type, t.Id Id, CorrespondenceName Name, RemovalReason, u.UserName
	FROM Beholder.MediaCorrespondence t 
	JOIN Security.[User] u ON u.Id = t.ModifiedUserId
	WHERE RemovalStatusId IS NOT NULL AND t.DateDeleted IS NULL
	UNION ALL
	SELECT 'Published' Type, t.Id Id, Name Name, RemovalReason, u.UserName
	FROM Beholder.MediaPublished t 
	JOIN Security.[User] u ON u.Id = t.ModifiedUserId
	WHERE RemovalStatusId IS NOT NULL AND t.DateDeleted IS NULL
	UNION ALL
	SELECT 'Contact' Type, c.Id Id, ISNULL(p.LName + ', ','') + ISNULL(p.FName,'') + ISNULL(' '+ p.MName,'') Name, RemovalReason, u.UserName
	FROM Beholder.Contact c 
	JOIN Common.Person p 
	  ON p.Id = c.PersonId  
	JOIN Security.[User] u ON u.Id = c.ModifiedUserId
	WHERE RemovalStatusId IS NOT NULL AND c.DateDeleted IS NULL
	UNION ALL
	SELECT 'Subscription' Type, t.Id Id, PublicationName Name, RemovalReason, u.UserName
	FROM Beholder.Subscription t 
	JOIN Security.[User] u ON u.Id = t.ModifiedUserId
	WHERE RemovalStatusId IS NOT NULL AND t.DateDeleted IS NULL
	UNION ALL
	SELECT 'Website' Type, t.Id Id, Name Name, RemovalReason, u.UserName
	FROM Beholder.MediaWebsiteEGroup t 
	JOIN Security.[User] u ON u.Id = t.ModifiedUserId
	WHERE RemovalStatusId IS NOT NULL AND t.DateDeleted IS NULL
	UNION ALL
	SELECT 'Vehicle' Type, t.Id Id, CAST(t.VehicleYear as varchar(50)) + ' ' + vc.Name + ' ' + vm.Name + ' ' + vmd.Name  Name, RemovalReason, u.UserName
	FROM Beholder.Vehicle t 
	LEFT JOIN Beholder.VehicleColor vc ON vc.Id = t.VehicleColorId
	LEFT JOIN Beholder.VehicleMake vm ON vm.Id = t.VehicleMakeId
	LEFT JOIN Beholder.VehicleModel vmd ON vmd.Id = t.VehicleModelId
	JOIN Security.[User] u ON u.Id = t.ModifiedUserId
	WHERE RemovalStatusId IS NOT NULL AND t.DateDeleted IS NULL



