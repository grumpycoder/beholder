CREATE TABLE [Beholder].[MediaCorrespondenceComment] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [MediaCorrespondenceId] INT            NOT NULL,
    [Comment]               VARCHAR (4000) NOT NULL,
    [DateCreated]           DATETIME2 (7)  NULL,
    [CreatedUserId]         INT            NULL,
    [DateModified]          DATETIME2 (7)  NULL,
    [ModifiedUserId]        INT            NULL,
    [DateDeleted]           DATETIME2 (7)  NULL,
    [DeletedUserId]         INT            NULL,
    CONSTRAINT [PK_MediaCorrespondenceComment] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaCorrespondenceComment_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceComment_MediaCorrespondence] FOREIGN KEY ([MediaCorrespondenceId]) REFERENCES [Beholder].[MediaCorrespondence] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceComment_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceComment_User] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id])
);

