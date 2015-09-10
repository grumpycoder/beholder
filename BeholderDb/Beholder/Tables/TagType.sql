CREATE TABLE [Beholder].[TagType] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (100) NOT NULL,
    [SortOrder]      INT           NULL,
    [DateCreated]    DATETIME2 (7) CONSTRAINT [DF_TagType_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]  INT           NOT NULL,
    [DateModified]   DATETIME2 (7) NULL,
    [ModifiedUserId] INT           NULL,
    [DateDeleted]    DATETIME2 (7) NULL,
    [DeletedUserId]  INT           NULL,
    CONSTRAINT [PK_TagType] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_TagType_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_TagType_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_TagType_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id])
);

