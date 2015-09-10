CREATE TABLE [Beholder].[MediaWebsiteEGroupSubscriptionRel] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [MediaWebsiteEGroupId] INT           NOT NULL,
    [SubscriptionId]       INT           NOT NULL,
    [DateStart]            DATETIME2 (7) NULL,
    [DateEnd]              DATETIME2 (7) NULL,
    [RelationshipTypeId]   INT           NOT NULL,
    [ApprovalStatusId]     INT           NULL,
    [PrimaryStatusId]      INT           NULL,
    CONSTRAINT [PK_MediaWebsiteEGroupSubscriptionRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaWebsiteEGroupSubscriptionRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_MediaWebsiteEGroupSubscriptionRel_MediaWebsiteEGroup] FOREIGN KEY ([MediaWebsiteEGroupId]) REFERENCES [Beholder].[MediaWebsiteEGroup] ([Id]),
    CONSTRAINT [FK_MediaWebsiteEGroupSubscriptionRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_MediaWebsiteEGroupSubscriptionRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id]),
    CONSTRAINT [FK_MediaWebsiteEGroupSubscriptionRel_Subscription] FOREIGN KEY ([SubscriptionId]) REFERENCES [Beholder].[Subscription] ([Id])
);

