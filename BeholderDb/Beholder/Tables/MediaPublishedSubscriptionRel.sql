CREATE TABLE [Beholder].[MediaPublishedSubscriptionRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [MediaPublishedId]   INT           NOT NULL,
    [SubscriptionId]     INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NOT NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_MediaPublishedSubscriptionRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaPublishedSubscriptionRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_MediaPublishedSubscriptionRel_MediaPublished] FOREIGN KEY ([MediaPublishedId]) REFERENCES [Beholder].[MediaPublished] ([Id]),
    CONSTRAINT [FK_MediaPublishedSubscriptionRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_MediaPublishedSubscriptionRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id]),
    CONSTRAINT [FK_MediaPublishedSubscriptionRel_Subscription] FOREIGN KEY ([SubscriptionId]) REFERENCES [Beholder].[Subscription] ([Id])
);

