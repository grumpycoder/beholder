CREATE TABLE [Beholder].[AliasChapterRel] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [ChapterId]         INT           NOT NULL,
    [AliasId]           INT           NOT NULL,
    [PrimaryStatusId]   INT           NULL,
    [FirstKnownUseDate] DATETIME2 (7) NULL,
    [LastKnownUseDate]  DATETIME2 (7) NULL,
    [DateCreated]       DATETIME2 (7) CONSTRAINT [DF_AliasChapterRel_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]     INT           NOT NULL,
    [DateModified]      DATETIME2 (7) NULL,
    [ModifiedUserId]    INT           NULL,
    [DateDeleted]       DATETIME2 (7) NULL,
    [DeletedUserId]     INT           NULL,
    CONSTRAINT [PK_AliasChapterRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_AliasChapterRel_Alias] FOREIGN KEY ([AliasId]) REFERENCES [Common].[Alias] ([Id]),
    CONSTRAINT [FK_AliasChapterRel_Chapter] FOREIGN KEY ([ChapterId]) REFERENCES [Beholder].[Chapter] ([Id]),
    CONSTRAINT [FK_AliasChapterRel_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AliasChapterRel_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AliasChapterRel_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AliasChapterRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id])
);

