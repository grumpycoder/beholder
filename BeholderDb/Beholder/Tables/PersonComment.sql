CREATE TABLE [Beholder].[PersonComment] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [PersonId]       INT            NOT NULL,
    [Comment]        VARCHAR (4000) NOT NULL,
    [DateCreated]    DATETIME2 (7)  CONSTRAINT [DF_PersonComment_DateCreated] DEFAULT (getdate()) NULL,
    [CreatedUserId]  INT            NULL,
    [DateModified]   DATETIME2 (7)  NULL,
    [ModifiedUserId] INT            NULL,
    [DateDeleted]    DATETIME2 (7)  NULL,
    [DeletedUserId]  INT            NULL,
    CONSTRAINT [PK_PersonComments] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_PersonComment_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_PersonComment_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_PersonComment_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_PersonComment_Person] FOREIGN KEY ([PersonId]) REFERENCES [Beholder].[Person] ([Id])
);

