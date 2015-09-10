CREATE TABLE [Common].[AddressPersonRel] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [PersonId]          INT           NOT NULL,
    [AddressId]         INT           NOT NULL,
    [AddressTypeId]     INT           NULL,
    [FirstKnownUseDate] DATETIME2 (7) NULL,
    [LastKNownUsedate]  DATETIME2 (7) NULL,
    [PrimaryStatusId]   INT           NOT NULL,
    [DateCreated]       DATETIME2 (7) CONSTRAINT [DF_AddressPersonRel_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]     INT           NOT NULL,
    [DateModified]      DATETIME2 (7) NULL,
    [ModifiedUserId]    INT           NULL,
    [DateDeleted]       DATETIME2 (7) NULL,
    [DeletedUserId]     INT           NULL,
    CONSTRAINT [PK_AddressPersonRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [CommonDATA1],
    CONSTRAINT [FK_AddressPersonRel_Address] FOREIGN KEY ([AddressId]) REFERENCES [Common].[Address] ([Id]),
    CONSTRAINT [FK_AddressPersonRel_AddressType] FOREIGN KEY ([AddressTypeId]) REFERENCES [Common].[AddressType] ([Id]),
    CONSTRAINT [FK_AddressPersonRel_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AddressPersonRel_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AddressPersonRel_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AddressPersonRel_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [FK_AddressPersonRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id])
);

