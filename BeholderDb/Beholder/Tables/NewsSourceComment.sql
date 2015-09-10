CREATE TABLE [Beholder].[NewsSourceComment] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [NewsSourceId]   INT            NOT NULL,
    [Comment]        VARCHAR (4000) NOT NULL,
    [DateCreated]    DATETIME2 (7)  CONSTRAINT [DF_NewsSourceComment_DateCreated] DEFAULT (getdate()) NULL,
    [CreatedUserId]  INT            NULL,
    [DateModified]   DATETIME2 (7)  NULL,
    [ModifiedUserId] INT            NULL,
    [DateDeleted]    DATETIME2 (7)  NULL,
    [DeletedUserId]  INT            NULL,
    CONSTRAINT [PK_NewsSourceComments] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_NewsSourceComment_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_NewsSourceComment_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_NewsSourceComment_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_NewsSourceComment_NewsSource] FOREIGN KEY ([NewsSourceId]) REFERENCES [Beholder].[NewsSource] ([Id])
);

