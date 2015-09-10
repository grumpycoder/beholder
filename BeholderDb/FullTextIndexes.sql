CREATE FULLTEXT INDEX ON [Beholder].[MediaCorrespondenceContext]
    ([ContextText] TYPE COLUMN [DocumentExtension] LANGUAGE 1033)
    KEY INDEX [PK_MediaCorrespondenceContext]
    ON [FileStreamFTSCatalog];


GO
CREATE FULLTEXT INDEX ON [Beholder].[MediaPublishedContext]
    ([ContextText] TYPE COLUMN [DocumentExtension] LANGUAGE 1033)
    KEY INDEX [PK_MediaPublishedContext]
    ON [FileStreamFTSCatalog];


GO
CREATE FULLTEXT INDEX ON [Beholder].[MediaWebsiteEGroupContext]
    ([ContextText] TYPE COLUMN [DocumentExtension] LANGUAGE 1033)
    KEY INDEX [PK_MediaWebsiteEGroupContext]
    ON [FileStreamFTSCatalog];

