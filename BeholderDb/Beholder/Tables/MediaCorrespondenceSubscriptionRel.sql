CREATE TABLE [Beholder].[MediaCorrespondenceSubscriptionRel] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [MediaCorrespondenceId] INT           NOT NULL,
    [SubscriptionId]        INT           NOT NULL,
    [DateStart]             DATETIME2 (7) NULL,
    [DateEnd]               DATETIME2 (7) NULL,
    [RelationshipTypeId]    INT           NOT NULL,
    [ApprovalStatusId]      INT           NULL,
    [PrimaryStatusId]       INT           NULL,
    CONSTRAINT [PK_MediaCorrespondenceSubscriptionRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaCorrespondenceSubscriptionRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceSubscriptionRel_MediaCorrespondence] FOREIGN KEY ([MediaCorrespondenceId]) REFERENCES [Beholder].[MediaCorrespondence] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceSubscriptionRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceSubscriptionRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceSubscriptionRel_Subscription] FOREIGN KEY ([SubscriptionId]) REFERENCES [Beholder].[Subscription] ([Id])
);

