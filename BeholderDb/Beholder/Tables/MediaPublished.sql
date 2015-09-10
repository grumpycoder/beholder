CREATE TABLE [Beholder].[MediaPublished] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [MediaTypeId]             INT            NOT NULL,
    [PublishedTypeId]         INT            NULL,
    [LibraryCategoryTypeId]   INT            NULL,
    [Name]                    VARCHAR (200)  NULL,
    [DatePublished]           DATETIME2 (7)  NULL,
    [DateReceived]            DATETIME2 (7)  NULL,
    [Author]                  VARCHAR (80)   NULL,
    [NewsSourceId]            INT            NULL,
    [MovementClassId]         INT            NULL,
    [ConfidentialityTypeId]   INT            NULL,
    [DateRenewalPermission]   DATETIME2 (7)  NULL,
    [RenewalPermissionTypeId] INT            NULL,
    [RenewalPermission]       VARCHAR (500)  NULL,
    [Summary]                 VARCHAR (4000) NULL,
    [City]                    VARCHAR (40)   NULL,
    [County]                  VARCHAR (40)   NULL,
    [StateId]                 INT            NULL,
    [DateCreated]             DATETIME2 (7)  NOT NULL,
    [CreatedUserId]           INT            NOT NULL,
    [DateModified]            DATETIME2 (7)  NULL,
    [ModifiedUserId]          INT            NULL,
    [RemovalStatusId]         INT            NULL,
    [RemovalReason]           VARCHAR (50)   NULL,
    [DateDeleted]             DATETIME2 (7)  NULL,
    [DeletedUserId]           INT            NULL,
    [MediaPublishedContextId] INT            NULL,
    [CatalogId]               VARCHAR (15)   NULL,
    CONSTRAINT [PK_MediaPublished] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1]
);


GO
CREATE NONCLUSTERED INDEX [idxBeholder_MediaPublished_RemovalStatusID]
    ON [Beholder].[MediaPublished]([RemovalStatusId] ASC)
    INCLUDE([Name], [DatePublished], [ConfidentialityTypeId], [City])
    ON [BeholderData1];

