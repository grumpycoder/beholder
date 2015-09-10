CREATE TABLE [Beholder].[AddressEventRel] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [EventId]           INT           NOT NULL,
    [AddressId]         INT           NOT NULL,
    [AddressTypeId]     INT           NOT NULL,
    [FirstKnownUseDate] DATETIME2 (7) NULL,
    [LastKnownUseDate]  DATETIME2 (7) NULL,
    [PrimaryStatusId]   INT           NULL,
    [DateCreated]       DATETIME2 (7) CONSTRAINT [DF_AddressEventRel_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]     INT           NOT NULL,
    [DateModified]      DATETIME2 (7) NULL,
    [ModifiedUserId]    INT           NULL,
    [DateDeleted]       DATETIME2 (7) NULL,
    [DeletedUserId]     INT           NULL,
    CONSTRAINT [PK_AddressEventRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_AddressEventRel_Address] FOREIGN KEY ([AddressId]) REFERENCES [Common].[Address] ([Id]),
    CONSTRAINT [FK_AddressEventRel_AddressType] FOREIGN KEY ([AddressTypeId]) REFERENCES [Common].[AddressType] ([Id]),
    CONSTRAINT [FK_AddressEventRel_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AddressEventRel_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AddressEventRel_Event] FOREIGN KEY ([EventId]) REFERENCES [Beholder].[Event] ([Id]),
    CONSTRAINT [FK_AddressEventRel_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AddressEventRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id])
);

