

CREATE VIEW [Beholder].[v_Chapter]
AS
SELECT chap.Id "Id"
      ,ChapterName
  FROM Beholder.Chapter chap
  LEFT JOIN Beholder.RemovalStatus rs
    ON rs.Id = chap.RemovalStatusId
  LEFT JOIN Beholder.ApprovalStatus astat
    ON astat.Id = chap.ApprovalStatusId
 WHERE isnull(astat.Name,'') <> 'Disapproved'
   AND ISNULL(rs.Name,'Disapproved') = 'Disapproved'
   AND chap.DateDeleted IS NULL



