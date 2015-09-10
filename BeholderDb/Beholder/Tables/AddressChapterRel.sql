CREATE TABLE [Beholder].[AddressChapterRel] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [ChapterId]         INT           NOT NULL,
    [AddressId]         INT           NOT NULL,
    [AddressTypeId]     INT           NULL,
    [FirstKnownUseDate] DATETIME2 (7) NOT NULL,
    [LastKNownUsedate]  DATETIME2 (7) NULL,
    [PrimaryStatusId]   INT           NOT NULL,
    [DateCreated]       DATETIME2 (7) CONSTRAINT [DF_AddressChapterRel_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]     INT           NOT NULL,
    [DateModified]      DATETIME2 (7) NULL,
    [ModifiedUserId]    INT           NULL,
    [DateDeleted]       DATETIME2 (7) NULL,
    [DeletedUserId]     INT           NULL,
    CONSTRAINT [PK_AddressChapterRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_AddressChapterRel_Address] FOREIGN KEY ([AddressId]) REFERENCES [Common].[Address] ([Id]),
    CONSTRAINT [FK_AddressChapterRel_AddressType] FOREIGN KEY ([AddressTypeId]) REFERENCES [Common].[AddressType] ([Id]),
    CONSTRAINT [FK_AddressChapterRel_Chapter] FOREIGN KEY ([ChapterId]) REFERENCES [Beholder].[Chapter] ([Id]),
    CONSTRAINT [FK_AddressChapterRel_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AddressChapterRel_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AddressChapterRel_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AddressChapterRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id])
);

