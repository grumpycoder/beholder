



CREATE VIEW [Beholder].[v_BeholderPerson]
AS
SELECT bper.Id Id
      ,ISNULL(per.LName + ', ','') + ISNULL(per.FName,'') + ISNULL(' '+per.MName,'') DisplayName
  FROM Beholder.Person   bper
  JOIN Common.Person     per
    ON per.Id = bper.PersonId
  LEFT JOIN Common.Suffix suf
    ON suf.Id = per.SuffixId
  LEFT JOIN Beholder.RemovalStatus rs
    ON rs.Id = bper.RemovalStatusId
 WHERE ISNULL(rs.Name,'Disapproved') = 'Disapproved'
   AND bper.DateDeleted IS NULL



