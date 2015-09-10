CREATE TABLE [Beholder].[MediaPublishedComment] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [MediaPublishedId] INT            NOT NULL,
    [Comment]          VARCHAR (4000) NOT NULL,
    [DateCreated]      DATETIME2 (7)  NULL,
    [CreatedUserId]    INT            NULL,
    [DateModified]     DATETIME2 (7)  NULL,
    [ModifiedUserId]   INT            NULL,
    [DateDeleted]      DATETIME2 (7)  NULL,
    [DeletedUserId]    INT            NULL,
    CONSTRAINT [PK_MediaPublishedComment] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaPublishedComment_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_MediaPublishedComment_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_MediaPublishedComment_MediaPublished] FOREIGN KEY ([MediaPublishedId]) REFERENCES [Beholder].[MediaPublished] ([Id]),
    CONSTRAINT [FK_MediaPublishedComment_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id])
);

