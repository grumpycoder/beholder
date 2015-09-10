CREATE TABLE [Beholder].[MediaPublishedMediaImageRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [MediaPublishedId]   INT           NOT NULL,
    [MediaImageId]       INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NOT NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_MediaPublishedMediaImageRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaPublishedMediaImageRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_MediaPublishedMediaImageRel_MediaImage] FOREIGN KEY ([MediaImageId]) REFERENCES [Beholder].[MediaImage] ([Id]),
    CONSTRAINT [FK_MediaPublishedMediaImageRel_MediaPublished] FOREIGN KEY ([MediaPublishedId]) REFERENCES [Beholder].[MediaPublished] ([Id]),
    CONSTRAINT [FK_MediaPublishedMediaImageRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_MediaPublishedMediaImageRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

