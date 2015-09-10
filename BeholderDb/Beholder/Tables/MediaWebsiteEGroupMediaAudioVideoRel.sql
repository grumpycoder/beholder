CREATE TABLE [Beholder].[MediaWebsiteEGroupMediaAudioVideoRel] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [MediaWebsiteEGroupId] INT           NOT NULL,
    [MediaAudioVideoId]    INT           NOT NULL,
    [DateStart]            DATETIME2 (7) NULL,
    [DateEnd]              DATETIME2 (7) NULL,
    [RelationshipTypeId]   INT           NOT NULL,
    [ApprovalStatusId]     INT           NULL,
    [PrimaryStatusId]      INT           NULL,
    CONSTRAINT [PK_MediaWebsiteEGroupMediaAudioVideoRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaWebsiteEGroupMediaAudioVideoRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_MediaWebsiteEGroupMediaAudioVideoRel_MediaWebsiteEGroup] FOREIGN KEY ([MediaWebsiteEGroupId]) REFERENCES [Beholder].[MediaWebsiteEGroup] ([Id]),
    CONSTRAINT [FK_MediaWebsiteEGroupMediaAudioVideoRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_MediaWebsiteEGroupMediaAudioVideoRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

