CREATE VIEW [dbo].[v_ChapterReport]
	AS 
SELECT 		
CONVERT(int, ROW_NUMBER() OVER(ORDER BY chap.Id)) [RowNum],
chap.Id, 
ChapterName, 
a.City, 
a.County, 
s.StateCode, 
a.Latitude, 
a.Longitude, 
ps.Name [PrimaryStatus],
chap.ActiveYear, 
m.Name [MovementClass], 
ac.Name [ActiveStatus], 
ap.Name [ApprovalStatus]
FROM Beholder.Chapter chap
JOIN Beholder.MovementClass m ON m.Id = chap.MovementClassId
JOIN Beholder.ApprovalStatus ap ON ap.Id = chap.ApprovalStatusId
JOIN Beholder.ActiveStatus ac ON ac.Id = chap.ActiveStatusId
LEFT JOIN Beholder.RemovalStatus rs ON rs.Id = chap.RemovalStatusId
LEFT JOIN Beholder.ApprovalStatus astat ON astat.Id = chap.ApprovalStatusId
LEFT JOIN Beholder.AddressChapterRel acr ON acr.ChapterId = chap.Id
LEFT JOIN Common.PrimaryStatus ps ON ps.Id = acr.PrimaryStatusId
LEFT JOIN Common.Address a ON a.Id = acr.AddressId
LEFT JOIN Common.State s ON s.Id = a.StateId
WHERE chap.DateDeleted IS NULL AND acr.DateDeleted is null 

