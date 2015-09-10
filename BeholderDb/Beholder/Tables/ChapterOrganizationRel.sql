CREATE TABLE [Beholder].[ChapterOrganizationRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [ChapterId]          INT           NOT NULL,
    [OrganizationId]     INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NOT NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_ChapterOrganizationRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_ChapterOrganizationRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_ChapterOrganizationRel_Chapter] FOREIGN KEY ([ChapterId]) REFERENCES [Beholder].[Chapter] ([Id]),
    CONSTRAINT [FK_ChapterOrganizationRel_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Beholder].[Organization] ([Id]),
    CONSTRAINT [FK_ChapterOrganizationRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_ChapterOrganizationRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

