CREATE TABLE [Beholder].[MediaImageComment] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [MediaImageId]   INT            NOT NULL,
    [Comment]        VARCHAR (4000) NOT NULL,
    [DateCreated]    DATETIME2 (7)  NULL,
    [CreatedUserId]  INT            NULL,
    [DateModified]   DATETIME2 (7)  NULL,
    [ModifiedUserId] INT            NULL,
    [DateDeleted]    DATETIME2 (7)  NULL,
    [DeletedUserId]  INT            NULL,
    CONSTRAINT [PK_MediaImageComment] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaImageComment_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_MediaImageComment_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_MediaImageComment_MediaImage] FOREIGN KEY ([MediaImageId]) REFERENCES [Beholder].[MediaImage] ([Id]),
    CONSTRAINT [FK_MediaImageComment_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id])
);

