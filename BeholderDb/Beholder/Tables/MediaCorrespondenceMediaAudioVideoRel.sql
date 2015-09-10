CREATE TABLE [Beholder].[MediaCorrespondenceMediaAudioVideoRel] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [MediaCorrespondenceId] INT           NOT NULL,
    [MediaAudioVideoId]     INT           NOT NULL,
    [DateStart]             DATETIME2 (7) NULL,
    [DateEnd]               DATETIME2 (7) NULL,
    [RelationshipTypeId]    INT           NOT NULL,
    [ApprovalStatusId]      INT           NULL,
    [PrimaryStatusId]       INT           NULL,
    CONSTRAINT [PK_MediaCorrespondenceMediaAudioVideoRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaCorrespondenceMediaAudioVideoRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceMediaAudioVideoRel_MediaAudioVideo] FOREIGN KEY ([MediaAudioVideoId]) REFERENCES [Beholder].[MediaAudioVideo] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceMediaAudioVideoRel_MediaCorrespondence] FOREIGN KEY ([MediaCorrespondenceId]) REFERENCES [Beholder].[MediaCorrespondence] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceMediaAudioVideoRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceMediaAudioVideoRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

