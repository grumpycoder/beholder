CREATE TABLE [Beholder].[MediaImageMediaImageRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [MediaImageId]       INT           NOT NULL,
    [MediaImage2Id]      INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NOT NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_MediaImageMediaImageRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaImageMediaImageRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_MediaImageMediaImageRel_MediaImage] FOREIGN KEY ([MediaImageId]) REFERENCES [Beholder].[MediaImage] ([Id]),
    CONSTRAINT [FK_MediaImageMediaImageRel_MediaImage1] FOREIGN KEY ([MediaImage2Id]) REFERENCES [Beholder].[MediaImage] ([Id]),
    CONSTRAINT [FK_MediaImageMediaImageRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_MediaImageMediaImageRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

