CREATE TABLE [Beholder].[MediaCorrespondenceEventRel] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [MediaCorrespondenceId] INT           NOT NULL,
    [EventId]               INT           NOT NULL,
    [DateStart]             DATETIME2 (7) NULL,
    [DateEnd]               DATETIME2 (7) NULL,
    [RelationshipTypeId]    INT           NOT NULL,
    [ApprovalStatusId]      INT           NULL,
    [PrimaryStatusId]       INT           NULL,
    CONSTRAINT [PK_MediaCorrespondenceEventRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaCorrespondenceEventRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceEventRel_Event] FOREIGN KEY ([EventId]) REFERENCES [Beholder].[Event] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceEventRel_MediaCorrespondence] FOREIGN KEY ([MediaCorrespondenceId]) REFERENCES [Beholder].[MediaCorrespondence] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceEventRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceEventRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

