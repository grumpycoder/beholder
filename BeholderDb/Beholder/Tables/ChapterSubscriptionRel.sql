CREATE TABLE [Beholder].[ChapterSubscriptionRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [ChapterId]          INT           NOT NULL,
    [SubscriptionId]     INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NOT NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_ChapterSubscriptionRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_ChapterSubscriptionRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_ChapterSubscriptionRel_Chapter] FOREIGN KEY ([ChapterId]) REFERENCES [Beholder].[Chapter] ([Id]),
    CONSTRAINT [FK_ChapterSubscriptionRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_ChapterSubscriptionRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id]),
    CONSTRAINT [FK_ChapterSubscriptionRel_Subscription] FOREIGN KEY ([SubscriptionId]) REFERENCES [Beholder].[Subscription] ([Id])
);

