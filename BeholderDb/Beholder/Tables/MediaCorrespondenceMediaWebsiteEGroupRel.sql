CREATE TABLE [Beholder].[MediaCorrespondenceMediaWebsiteEGroupRel] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [MediaCorrespondenceId] INT           NOT NULL,
    [MediaWebsiteEGroupId]  INT           NOT NULL,
    [DateStart]             DATETIME2 (7) NULL,
    [DateEnd]               DATETIME2 (7) NULL,
    [RelationshipTypeId]    INT           NOT NULL,
    [ApprovalStatusId]      INT           NULL,
    [PrimaryStatusId]       INT           NULL,
    CONSTRAINT [PK_MediaCorrespondenceMediaWebsiteEGroupRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaCorrespondenceMediaWebsiteEGroupRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceMediaWebsiteEGroupRel_MediaWebsiteEGroup] FOREIGN KEY ([MediaWebsiteEGroupId]) REFERENCES [Beholder].[MediaWebsiteEGroup] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceMediaWebsiteEGroupRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceMediaWebsiteEGroupRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

