
CREATE VIEW [Beholder].[v_MediaPublishedContext] WITH SCHEMABINDING
AS
SELECT ID, FileStreamId, MimeTypeId, FileName, DocumentExtension, ContextText
  FROM Beholder.MediaPublishedContext



