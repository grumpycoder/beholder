


CREATE VIEW [Beholder].[v_Organization]
AS
SELECT org.Id "Id"
      ,OrganizationName
  FROM Beholder.Organization org
  LEFT JOIN Beholder.RemovalStatus rs
    ON rs.Id = org.RemovalStatusId
  LEFT JOIN Beholder.ApprovalStatus astat
    ON astat.Id = org.ApprovalStatusId
 WHERE isnull(astat.Name,'') <> 'Disapproved'
   AND ISNULL(rs.Name,'Disapproved') = 'Disapproved'
   AND org.DateDeleted IS NULL



