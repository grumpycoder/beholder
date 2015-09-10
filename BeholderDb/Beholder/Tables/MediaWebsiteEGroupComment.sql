CREATE TABLE [Beholder].[MediaWebsiteEGroupComment] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [MediaWebsiteEGroupId] INT            NOT NULL,
    [Comment]              VARCHAR (4000) NOT NULL,
    [DateCreated]          DATETIME2 (7)  NULL,
    [CreatedUserId]        INT            NULL,
    [DateModified]         DATETIME2 (7)  NULL,
    [ModifiedUserId]       INT            NULL,
    [DateDeleted]          DATETIME2 (7)  NULL,
    [DeletedUserId]        INT            NULL,
    CONSTRAINT [PK_MediaWebsiteEGroupComment] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaWebsiteEGroupComment_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_MediaWebsiteEGroupComment_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_MediaWebsiteEGroupComment_MediaWebsiteEGroup] FOREIGN KEY ([MediaWebsiteEGroupId]) REFERENCES [Beholder].[MediaWebsiteEGroup] ([Id]),
    CONSTRAINT [FK_MediaWebsiteEGroupComment_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id])
);

