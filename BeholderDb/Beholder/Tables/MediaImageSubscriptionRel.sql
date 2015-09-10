CREATE TABLE [Beholder].[MediaImageSubscriptionRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [MediaImageId]       INT           NOT NULL,
    [SubscriptionId]     INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NOT NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_MediaImageSubscriptionRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaImageSubscriptionRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_MediaImageSubscriptionRel_MediaImage] FOREIGN KEY ([MediaImageId]) REFERENCES [Beholder].[MediaImage] ([Id]),
    CONSTRAINT [FK_MediaImageSubscriptionRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_MediaImageSubscriptionRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id]),
    CONSTRAINT [FK_MediaImageSubscriptionRel_Subscription] FOREIGN KEY ([SubscriptionId]) REFERENCES [Beholder].[Subscription] ([Id])
);

