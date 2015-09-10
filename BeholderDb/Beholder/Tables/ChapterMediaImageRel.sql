CREATE TABLE [Beholder].[ChapterMediaImageRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [ChapterId]          INT           NOT NULL,
    [MediaImageId]       INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_ChapterMediaImageRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_ChapterMediaImageRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_ChapterMediaImageRel_Chapter] FOREIGN KEY ([ChapterId]) REFERENCES [Beholder].[Chapter] ([Id]),
    CONSTRAINT [FK_ChapterMediaImageRel_MediaImage] FOREIGN KEY ([MediaImageId]) REFERENCES [Beholder].[MediaImage] ([Id]),
    CONSTRAINT [FK_ChapterMediaImageRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_ChapterMediaImageRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

