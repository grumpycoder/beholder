CREATE TABLE [Beholder].[Subscription] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [OrderIdOld]            INT           NULL,
    [PublicationName]       VARCHAR (256) NOT NULL,
    [ActiveStatusId]        INT           NULL,
    [RenewalPermissionDate] DATETIME2 (7) NULL,
    [SubscriptionRate]      VARCHAR (40)  NULL,
    [DateCreated]           DATETIME2 (7) CONSTRAINT [DF_Subscription_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]         INT           NOT NULL,
    [DateModified]          DATETIME2 (7) NULL,
    [ModifiedUserId]        INT           NULL,
    [RemovalStatusId]       INT           NULL,
    [RemovalReason]         VARCHAR (50)  NULL,
    [DateDeleted]           DATETIME2 (7) NULL,
    [DeletedUserId]         INT           NULL,
    CONSTRAINT [PK_Subscriptions] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_Subscription_RemovalStatus] FOREIGN KEY ([RemovalStatusId]) REFERENCES [Beholder].[RemovalStatus] ([Id]),
    CONSTRAINT [FK_Subscriptions_ActiveStatus] FOREIGN KEY ([ActiveStatusId]) REFERENCES [Beholder].[ActiveStatus] ([Id]),
    CONSTRAINT [FK_Subscriptions_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_Subscriptions_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_Subscriptions_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id])
);

