
CREATE VIEW [Beholder].[v_MediaCorrespondenceContext] WITH SCHEMABINDING
AS
SELECT ID, FileStreamId, MimeTypeId, FileName, DocumentExtension, ContextText
  FROM Beholder.MediaCorrespondenceContext



