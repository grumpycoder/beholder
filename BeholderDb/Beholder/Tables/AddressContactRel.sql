CREATE TABLE [Beholder].[AddressContactRel] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [ContactId]         INT           NOT NULL,
    [AddressId]         INT           NOT NULL,
    [AddressTypeId]     INT           NOT NULL,
    [FirstKnownUseDate] DATETIME2 (7) NOT NULL,
    [LastKNownUsedate]  DATETIME2 (7) NULL,
    [PrimaryStatusId]   INT           NOT NULL,
    [DateCreated]       DATETIME2 (7) CONSTRAINT [DF_AddressContactRel_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]     INT           NOT NULL,
    [DateModified]      DATETIME2 (7) NULL,
    [ModifiedUserId]    INT           NULL,
    [DateDeleted]       DATETIME2 (7) NULL,
    [DeletedUserId]     INT           NULL,
    CONSTRAINT [PK_AddressContactRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_AddressContactRel_Address] FOREIGN KEY ([AddressId]) REFERENCES [Common].[Address] ([Id]),
    CONSTRAINT [FK_AddressContactRel_AddressType] FOREIGN KEY ([AddressTypeId]) REFERENCES [Common].[AddressType] ([Id]),
    CONSTRAINT [FK_AddressContactRel_Contact] FOREIGN KEY ([ContactId]) REFERENCES [Beholder].[Contact] ([Id]),
    CONSTRAINT [FK_AddressContactRel_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AddressContactRel_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AddressContactRel_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AddressContactRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id])
);

