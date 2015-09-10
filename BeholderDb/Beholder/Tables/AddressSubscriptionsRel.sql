CREATE TABLE [Beholder].[AddressSubscriptionsRel] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [SubscriptionsId]   INT           NOT NULL,
    [AddressId]         INT           NOT NULL,
    [AddressTypeId]     INT           NOT NULL,
    [FirstKnownUseDate] DATETIME2 (7) NOT NULL,
    [LastKNownUsedate]  DATETIME2 (7) NULL,
    [PrimaryStatusId]   INT           NULL,
    [DateCreated]       DATETIME2 (7) CONSTRAINT [DF_AddressSubscriptionsRel_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]     INT           NOT NULL,
    [DateModified]      DATETIME2 (7) NULL,
    [ModifiedUserId]    INT           NULL,
    [DateDeleted]       DATETIME2 (7) NULL,
    [DeletedUserId]     INT           NULL,
    CONSTRAINT [PK_AddressSubscriptionsRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_AddressSubscriptionsRel_Address] FOREIGN KEY ([AddressId]) REFERENCES [Common].[Address] ([Id]),
    CONSTRAINT [FK_AddressSubscriptionsRel_AddressType] FOREIGN KEY ([AddressTypeId]) REFERENCES [Common].[AddressType] ([Id]),
    CONSTRAINT [FK_AddressSubscriptionsRel_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AddressSubscriptionsRel_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AddressSubscriptionsRel_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AddressSubscriptionsRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_AddressSubscriptionsRel_Subscriptions] FOREIGN KEY ([SubscriptionsId]) REFERENCES [Beholder].[Subscription] ([Id])
);

