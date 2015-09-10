CREATE TABLE [Beholder].[ChapterMediaWebsiteEGroupRel] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [ChapterId]            INT           NOT NULL,
    [MediaWebsiteEGroupId] INT           NOT NULL,
    [DateStart]            DATETIME2 (7) NULL,
    [DateEnd]              DATETIME2 (7) NULL,
    [RelationshipTypeId]   INT           NOT NULL,
    [ApprovalStatusId]     INT           NULL,
    [PrimaryStatusId]      INT           NULL,
    CONSTRAINT [PK_ChapterMediaWebsiteEGroupRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_ChapterMediaWebsiteEGroupRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_ChapterMediaWebsiteEGroupRel_Chapter] FOREIGN KEY ([ChapterId]) REFERENCES [Beholder].[Chapter] ([Id]),
    CONSTRAINT [FK_ChapterMediaWebsiteEGroupRel_MediaWebsiteEGroup] FOREIGN KEY ([MediaWebsiteEGroupId]) REFERENCES [Beholder].[MediaWebsiteEGroup] ([Id]),
    CONSTRAINT [FK_ChapterMediaWebsiteEGroupRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_ChapterMediaWebsiteEGroupRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

