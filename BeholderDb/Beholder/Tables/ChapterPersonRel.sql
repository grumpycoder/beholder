CREATE TABLE [Beholder].[ChapterPersonRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [ChapterId]          INT           NOT NULL,
    [PersonId]           INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_ChapterPersonRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_ChapterPersonRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_ChapterPersonRel_Chapter] FOREIGN KEY ([ChapterId]) REFERENCES [Beholder].[Chapter] ([Id]),
    CONSTRAINT [FK_ChapterPersonRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_ChapterPersonRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

