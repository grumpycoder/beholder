CREATE TABLE [Beholder].[LibraryCategoryType] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Number]         NUMERIC (6, 3) NOT NULL,
    [Name]           VARCHAR (100)  NOT NULL,
    [SortOrder]      INT            NULL,
    [DateCreated]    DATETIME2 (7)  NOT NULL,
    [CreatedUserId]  INT            NOT NULL,
    [DateModified]   DATETIME2 (7)  NULL,
    [ModifiedUserId] INT            NULL,
    [DateDeleted]    DATETIME2 (7)  NULL,
    [DeletedUserId]  INT            NULL,
    CONSTRAINT [PK_LibraryCategoryType] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_LibraryCategoryType_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_LibraryCategoryType_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_LibraryCategoryType_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id])
);

