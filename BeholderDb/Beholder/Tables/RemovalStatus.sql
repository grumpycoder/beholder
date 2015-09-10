CREATE TABLE [Beholder].[RemovalStatus] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (100) NOT NULL,
    [SortOrder]      INT           NULL,
    [DateCreated]    DATETIME2 (7) NOT NULL,
    [CreatedUserId]  INT           NOT NULL,
    [DateModified]   DATETIME2 (7) NULL,
    [ModifiedUserId] INT           NULL,
    [DateDeleted]    DATETIME2 (7) NULL,
    [DeletedUserId]  INT           NULL,
    CONSTRAINT [PK_RemovalStatus] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_RemovalStatus_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_RemovalStatus_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_RemovalStatus_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id])
);

