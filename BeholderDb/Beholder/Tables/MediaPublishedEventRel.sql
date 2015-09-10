CREATE TABLE [Beholder].[MediaPublishedEventRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [MediaPublishedId]   INT           NOT NULL,
    [EventId]            INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NOT NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_MediaPublishedEventRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaPublishedEventRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_MediaPublishedEventRel_MediaPublished] FOREIGN KEY ([MediaPublishedId]) REFERENCES [Beholder].[MediaPublished] ([Id]),
    CONSTRAINT [FK_MediaPublishedEventRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_MediaPublishedEventRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

