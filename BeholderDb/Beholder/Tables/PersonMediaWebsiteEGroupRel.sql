CREATE TABLE [Beholder].[PersonMediaWebsiteEGroupRel] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [PersonId]             INT           NOT NULL,
    [MediaWebsiteEGroupId] INT           NOT NULL,
    [DateStart]            DATETIME2 (7) NULL,
    [DateEnd]              DATETIME2 (7) NULL,
    [RelationshipTypeId]   INT           NOT NULL,
    [ApprovalStatusId]     INT           NULL,
    [PrimaryStatusId]      INT           NULL,
    CONSTRAINT [PK_PersonMediaWebsiteEGroupRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_PersonMediaWebsiteEGroupRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_PersonMediaWebsiteEGroupRel_MediaWebsiteEGroup] FOREIGN KEY ([MediaWebsiteEGroupId]) REFERENCES [Beholder].[MediaWebsiteEGroup] ([Id]),
    CONSTRAINT [FK_PersonMediaWebsiteEGroupRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_PersonMediaWebsiteEGroupRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

