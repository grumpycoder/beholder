

--SELECT TOP 100 * FROM [Beholder].[v_EntityList] WHERE Controller = 'Vehicles'



CREATE VIEW [Beholder].[v_EntityList]
AS
SELECT org.Id "Id"
      ,et.Name "Entity"
      ,et.Controller "Controller"
	  ,ISNULL(org.ChapterName,'') "EntityName"
	  ,ISNULL(org.ChapterDesc,'') "EntityDesc"
	  ,ot.Name "EntityType"
	  ,mc.Name "MovementClass"
	  ,astat.Name "ApprovalStatus"
	  ,act.Name "ActiveStatus"
  FROM Beholder.Chapter org
  JOIN Beholder.EntityType et
    ON et.Name = 'Chapter'
  LEFT JOIN Beholder.ChapterType ot
    ON ot.Id = org.ChapterTypeId
  LEFT JOIN Beholder.MovementClass mc
    ON mc.Id = org.MovementClassId
  LEFT JOIN Beholder.ApprovalStatus astat
    ON astat.Id = org.ApprovalStatusId
  LEFT JOIN Beholder.RemovalStatus rs
    ON rs.Id = org.RemovalStatusId
  LEFT JOIN Beholder.ActiveStatus act
    ON act.Id = org.ActiveStatusId
 WHERE isnull(astat.Name,'') <> 'Disapproved'
   AND ISNULL(rs.Name,'Disapproved') = 'Disapproved'
   AND org.DateDeleted IS NULL
UNION ALL
SELECT cont.Id "Id"
      ,et.Name "Entity"
      ,et.Controller "Controller"
      ,ISNULL(per.LName + ', ','') + ISNULL(per.FName,'') + ISNULL(' '+per.MName,'') EntityName
      ,ISNULL(per.LName + ', ','') + ISNULL(per.FName,'') + ISNULL(' '+per.MName,'') EntityDesc
	  ,ct.Name "EntityType"
	  ,'' "MovementClass"
	  ,'Approved' "ApprovalStatus"
	  ,'Active' "ActiveStatus"
  FROM Beholder.Contact cont
  JOIN Common.Person per
    ON per.id = cont.PersonId
  JOIN Beholder.EntityType et
    ON et.Name = 'Contact'
  LEFT JOIN Beholder.ContactType ct
    ON ct.Id = cont.ContactTypeId
  LEFT JOIN Beholder.RemovalStatus rs
    ON rs.Id = cont.RemovalStatusId
 WHERE ISNULL(rs.Name,'Disapproved') = 'Disapproved'
   AND cont.DateDeleted IS NULL
UNION ALL
SELECT org.Id "Id"
      ,et.Name "Entity"
      ,et.Controller "Controller"
	  ,ISNULL(org.EventName,'') "EntityName"
	  ,ISNULL(org.Summary,'') "EntityDesc"
	  ,ot.Name "EntityType"
	  ,mc.Name "MovementClass"
	  ,astat.Name "ApprovalStatus"
	  ,act.Name "ActiveStatus"
  FROM Beholder.[Event] org
  JOIN Beholder.EntityType et
    ON et.Name = 'Event'
  LEFT JOIN Beholder.EventType ot
    ON ot.Id = org.EventDocumentationTypeId
  LEFT JOIN Beholder.MovementClass mc
    ON mc.Id = org.MovementClassId
  LEFT JOIN Beholder.ApprovalStatus astat
    ON astat.Id = org.ApprovalStatusId
  LEFT JOIN Beholder.RemovalStatus rs
    ON rs.Id = org.RemovalStatusId
  LEFT JOIN Beholder.ActiveStatus act
    ON act.Id = org.ActiveStatusId
 WHERE isnull(astat.Name,'') <> 'Disapproved'
   AND ISNULL(rs.Name,'Disapproved') = 'Disapproved'
   AND org.DateDeleted IS NULL
UNION ALL
SELECT ent.Id "Id"
      ,et.Name "Entity"
      ,et.Controller "Controller"
	  ,ISNULL(ent.Title,'') "EntityName"
	  ,ISNULL(ent.Summary,'') "EntityDesc"
	  ,mt.Name "EntityType"
	  ,mc.Name "MovementClass"
	  ,'Approved' "ApprovalStatus"
	  ,'Active' "ActiveStatus"
  FROM Beholder.MediaAudioVideo ent
  JOIN Beholder.EntityType et
    ON et.Name = 'MediaAudioVideo'
  LEFT JOIN Beholder.MovementClass mc
    ON mc.Id = ent.MovementClassId
  LEFT JOIN Beholder.MediaType mt
    ON mt.Id = ent.MediaTypeId
  LEFT JOIN Beholder.RemovalStatus rs
    ON rs.Id = ent.RemovalStatusId
 WHERE ISNULL(rs.Name,'Disapproved') = 'Disapproved'
   AND ent.DateDeleted IS NULL
UNION ALL
SELECT ent.Id "Id"
      ,et.Name "Entity"
      ,et.Controller "Controller"
	  ,ISNULL(ent.CorrespondenceName,'') "EntityName"
	  ,ISNULL(ent.Summary,'') "EntityDesc"
	  ,mt.Name "EntityType"
	  ,mc.Name "MovementClass"
	  ,'Approved' "ApprovalStatus"
	  ,'Active' "ActiveStatus"
  FROM Beholder.MediaCorrespondence ent
  JOIN Beholder.EntityType et
    ON et.Name = 'MediaCorrespondence'
  LEFT JOIN Beholder.MediaCorrespondenceContext context
    ON context.Id = ent.MediaCorrespondenceContextId
  LEFT JOIN Beholder.MovementClass mc
    ON mc.Id = ent.MovementClassId
  LEFT JOIN Beholder.MediaType mt
    ON mt.Id = ent.MediaTypeId
  LEFT JOIN Beholder.RemovalStatus rs
    ON rs.Id = ent.RemovalStatusId
 WHERE ISNULL(rs.Name,'Disapproved') = 'Disapproved'
   AND ent.DateDeleted IS NULL
UNION ALL
SELECT ent.Id "Id"
      ,et.Name "Entity"
      ,et.Controller "Controller"
	  ,ISNULL(ent.ImageTitle,'') "EntityName"
	  ,ISNULL(ent.Summary,'') "EntityDesc"
	  ,mt.Name "EntityType"
	  ,mc.Name "MovementClass"
	  ,'Approved' "ApprovalStatus"
	  ,'Active' "ActiveStatus"
  FROM Beholder.MediaImage ent
  JOIN Beholder.EntityType et
    ON et.Name = 'MediaImage'
  LEFT JOIN Beholder.MovementClass mc
    ON mc.Id = ent.MovementClassId
  LEFT JOIN Beholder.MediaType mt
    ON mt.Id = ent.MediaTypeId
  LEFT JOIN Beholder.RemovalStatus rs
    ON rs.Id = ent.RemovalStatusId
 WHERE ISNULL(rs.Name,'Disapproved') = 'Disapproved'
   AND ent.DateDeleted IS NULL
UNION ALL
SELECT ent.Id "Id"
      ,et.Name "Entity"
      ,et.Controller "Controller"
	  ,ISNULL(ent.Name,'') "EntityName"
	  ,ISNULL(ent.Summary,'') "EntityDesc"
	  ,mt.Name "EntityType"
	  ,mc.Name "MovementClass"
	  ,'Approved' "ApprovalStatus"
	  ,'Active' "ActiveStatus"
  FROM Beholder.MediaPublished ent
  JOIN Beholder.EntityType et
    ON et.Name = 'MediaPublished'
  LEFT JOIN Beholder.MediaPublishedContext context
    ON context.Id = ent.MediaPublishedContextId
  LEFT JOIN Beholder.MovementClass mc
    ON mc.Id = ent.MovementClassId
  LEFT JOIN Beholder.MediaType mt
    ON mt.Id = ent.MediaTypeId
  LEFT JOIN Beholder.RemovalStatus rs
    ON rs.Id = ent.RemovalStatusId
 WHERE ISNULL(rs.Name,'Disapproved') = 'Disapproved'
   AND ent.DateDeleted IS NULL
UNION ALL
SELECT ent.Id "Id"
      ,et.Name "Entity"
      ,et.Controller "Controller"
	  ,ISNULL(ent.Name,'') "EntityName"
	  ,ISNULL(ent.Summary,'') "EntityDesc"
	  ,mt.Name "EntityType"
	  ,mc.Name "MovementClass"
	  ,'Approved' "ApprovalStatus"
	  ,'Active' "ActiveStatus"
  FROM Beholder.MediaWebsiteEGroup ent
  JOIN Beholder.EntityType et
    ON et.Name = 'MediaWebsiteEGroup'
  LEFT JOIN Beholder.MediaWebsiteEGroupContext context
    ON context.Id = ent.MediaWebsiteEGroupContextId
  LEFT JOIN Beholder.MovementClass mc
    ON mc.Id = ent.MovementClassId
  LEFT JOIN Beholder.MediaType mt
    ON mt.Id = ent.MediaTypeId
  LEFT JOIN Beholder.RemovalStatus rs
    ON rs.Id = ent.RemovalStatusId
 WHERE ISNULL(rs.Name,'Disapproved') = 'Disapproved'
   AND ent.DateDeleted IS NULL
UNION ALL
SELECT org.Id "Id"
      ,et.Name "Entity"
      ,et.Controller "Controller"
	  ,ISNULL(org.NewsSourceName,'') "EntityName"
	  ,'' "EntityDesc"
	  ,ot.Name "EntityType"
	  ,'' "MovementClass"
	  ,'Approved' "ApprovalStatus"
	  ,'Active' "ActiveStatus"
  FROM Beholder.NewsSource org
  JOIN Beholder.EntityType et
    ON et.Name = 'NewsSource'
  LEFT JOIN Beholder.NewsSourceType ot
    ON ot.Id = org.NewsSourceTypeId
 WHERE org.DateDeleted IS NULL
UNION ALL
SELECT org.Id "Id"
      ,et.Name "Entity"
      ,et.Controller "Controller"
	  ,ISNULL(org.OrganizationName,'') "EntityName"
	  ,ISNULL(org.OrganizationDesc,'') "EntityDesc"
	  ,ot.Name "EntityType"
	  ,mc.Name "MovementClass"
	  ,astat.Name "ApprovalStatus"
	  ,act.Name "ActiveStatus"
  FROM Beholder.Organization org
  JOIN Beholder.EntityType et
    ON et.Name = 'Organization'
  LEFT JOIN Beholder.OrganizationType ot
    ON ot.Id = org.OrganizationTypeId
  LEFT JOIN Beholder.MovementClass mc
    ON mc.Id = org.MovementClassId
  LEFT JOIN Beholder.ApprovalStatus astat
    ON astat.Id = org.ApprovalStatusId
  LEFT JOIN Beholder.RemovalStatus rs
    ON rs.Id = org.RemovalStatusId
  LEFT JOIN Beholder.ActiveStatus act
    ON act.Id = org.ActiveStatusId
 WHERE isnull(astat.Name,'') <> 'Disapproved'
   AND ISNULL(rs.Name,'Disapproved') = 'Disapproved'
   AND org.DateDeleted IS NULL
UNION ALL
SELECT org.Id "Id"
      ,et.Name "Entity"
      ,et.Controller "Controller"
      ,ISNULL(per.LName + ', ','') + ISNULL(per.FName,'') + ISNULL(' '+per.MName,'') EntityName
      ,ISNULL(per.LName + ', ','') + ISNULL(per.FName,'') + ISNULL(' '+per.MName,'') EntityDesc
	  ,DistinguishableMarks "EntityType"
	  ,mc.Name "MovementClass"
	  ,'Approved' "ApprovalStatus"
	  ,'Active' "ActiveStatus"
  FROM Beholder.Person org
  JOIN Common.Person per
    ON per.id = org.PersonId
  JOIN Beholder.EntityType et
    ON et.Name = 'Person'
  LEFT JOIN Beholder.MovementClass mc
    ON mc.Id = org.MovementClassId
  LEFT JOIN Beholder.RemovalStatus rs
    ON rs.Id = org.RemovalStatusId
 WHERE ISNULL(rs.Name,'Disapproved') = 'Disapproved'
   AND org.DateDeleted IS NULL
UNION ALL
SELECT org.Id "Id"
      ,et.Name "Entity"
      ,et.Controller "Controller"
	  ,ISNULL(org.PublicationName,'') "EntityName"
	  ,ISNULL(org.SubscriptionRate,'') "EntityDesc"
	  ,'' "EntityType"
	  ,'' "MovementClass"
	  ,'Approved' "ApprovalStatus"
	  ,act.Name "ActiveStatus"
  FROM Beholder.Subscription org
  JOIN Beholder.EntityType et
    ON et.Name = 'Subscription'
  LEFT JOIN Beholder.RemovalStatus rs
    ON rs.Id = org.RemovalStatusId
  LEFT JOIN Beholder.ActiveStatus act
    ON act.Id = org.ActiveStatusId
 WHERE ISNULL(rs.Name,'Disapproved') = 'Disapproved'
   AND org.DateDeleted IS NULL
UNION ALL
SELECT org.Id "Id"
      ,et.Name "Entity"
      ,et.Controller "Controller"
	  ,ISNULL(org.VIN,'') "EntityName"
	  ,ISNULL(color.Name+' ','') + ISNULL(vm.Name+' ','') + ISNULL(model.Name+' ', '') + ISNULL(typ.Name+' ', '') "EntityDesc"
	  ,'' "EntityType"
	  ,'' "MovementClass"
	  ,'Approved' "ApprovalStatus"
	  ,'Active' "ActiveStatus"
  FROM Beholder.Vehicle org
  JOIN Beholder.EntityType et
    ON et.Name = 'Vehicle'
  LEFT JOIN Beholder.VehicleMake vm
    ON vm.Id = org.VehicleMakeId
  LEFT JOIN Beholder.VehicleModel model
    ON model.Id = org.VehicleModelId
  LEFT JOIN Beholder.VehicleType typ
    ON typ.Id = org.VehicleTypeId
  LEFT JOIN Beholder.VehicleColor color
    ON color.Id = org.VehicleColorId
  LEFT JOIN Beholder.RemovalStatus rs
    ON rs.Id = org.RemovalStatusId
 WHERE ISNULL(rs.Name,'Disapproved') = 'Disapproved'
   AND org.DateDeleted IS NULL



