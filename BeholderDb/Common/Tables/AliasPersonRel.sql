CREATE TABLE [Common].[AliasPersonRel] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [PersonId]          INT           NOT NULL,
    [AliasId]           INT           NOT NULL,
    [PrimaryStatusId]   INT           NULL,
    [FirstKnownUseDate] DATETIME2 (7) NULL,
    [LastKnownUseDate]  DATETIME2 (7) NULL,
    [DateCreated]       DATETIME2 (7) CONSTRAINT [DF_AliasPersonRel_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]     INT           NOT NULL,
    [DateModified]      DATETIME2 (7) NULL,
    [ModifiedUserId]    INT           NULL,
    [DateDeleted]       DATETIME2 (7) NULL,
    [DeletedUserId]     INT           NULL,
    CONSTRAINT [PK_AliasPersonRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [CommonDATA1],
    CONSTRAINT [FK_AliasPersonRel_Alias] FOREIGN KEY ([AliasId]) REFERENCES [Common].[Alias] ([Id]),
    CONSTRAINT [FK_AliasPersonRel_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AliasPersonRel_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AliasPersonRel_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AliasPersonRel_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [FK_AliasPersonRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id])
);

