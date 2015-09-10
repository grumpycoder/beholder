CREATE TABLE [Common].[Alias] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [AliasName]      VARCHAR (100) NOT NULL,
    [DateCreated]    DATETIME2 (7) CONSTRAINT [DF_Alias_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]  INT           NOT NULL,
    [DateModified]   DATETIME2 (7) NULL,
    [ModifiedUserId] INT           NULL,
    [DateDeleted]    DATETIME2 (7) NULL,
    [DeletedUserId]  INT           NULL,
    CONSTRAINT [PK_Alias] PRIMARY KEY CLUSTERED ([Id] ASC) ON [CommonDATA1],
    CONSTRAINT [FK_Alias_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_Alias_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_Alias_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id])
);

