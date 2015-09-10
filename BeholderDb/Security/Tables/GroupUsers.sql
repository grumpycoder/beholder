CREATE TABLE [Security].[GroupUsers] (
    [Id]             INT      IDENTITY (1, 1) NOT NULL,
    [UserId]         INT      NOT NULL,
    [GroupId]        INT      NOT NULL,
    [DateCreated]    DATETIME NULL,
    [CreatedUserId]  INT      NULL,
    [DateModified]   DATETIME NULL,
    [ModifiedUserId] INT      NULL,
    [DateDeleted]    DATETIME NULL,
    [DeletedUserId]  INT      NULL,
    CONSTRAINT [PK_GroupUsers] PRIMARY KEY CLUSTERED ([Id] ASC) ON [CommonDATA1],
    CONSTRAINT [FK_GroupUsers_Groups] FOREIGN KEY ([GroupId]) REFERENCES [Security].[Groups] ([Id]),
    CONSTRAINT [FK_GroupUsers_User] FOREIGN KEY ([UserId]) REFERENCES [Security].[User] ([Id])
);

