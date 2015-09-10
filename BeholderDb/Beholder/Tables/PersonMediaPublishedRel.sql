CREATE TABLE [Beholder].[PersonMediaPublishedRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [PersonId]           INT           NOT NULL,
    [MediaPublishedId]   INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NOT NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_PersonMediaPublishedRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_PersonMediaPublishedRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_PersonMediaPublishedRel_MediaPublished] FOREIGN KEY ([MediaPublishedId]) REFERENCES [Beholder].[MediaPublished] ([Id]),
    CONSTRAINT [FK_PersonMediaPublishedRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_PersonMediaPublishedRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

