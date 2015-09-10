CREATE TABLE [Beholder].[PersonMediaAudioVideoRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [PersonId]           INT           NOT NULL,
    [MediaAudioVideoId]  INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_PersonMediaAudioVideoRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_PersonMediaAudioVideoRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_PersonMediaAudioVideoRel_MediaAudioVideo] FOREIGN KEY ([MediaAudioVideoId]) REFERENCES [Beholder].[MediaAudioVideo] ([Id]),
    CONSTRAINT [FK_PersonMediaAudioVideoRel_Person] FOREIGN KEY ([PersonId]) REFERENCES [Beholder].[Person] ([Id]),
    CONSTRAINT [FK_PersonMediaAudioVideoRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_PersonMediaAudioVideoRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

