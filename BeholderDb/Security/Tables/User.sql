CREATE TABLE [Security].[User] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [PersonId]       INT           NOT NULL,
    [LastLoginDate]  DATETIME2 (7) NULL,
    [UserName]       VARCHAR (256) NOT NULL,
    [PassWord]       VARCHAR (25)  NULL,
    [ActiveStatusId] INT           NULL,
    [UserTypeId]     INT           NULL,
    [SecurityId]     INT           NULL,
    [DateCreated]    DATETIME2 (7) CONSTRAINT [DF_User_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]  INT           NULL,
    [DateModified]   DATETIME2 (7) CONSTRAINT [DF_User_DateModified] DEFAULT (getdate()) NULL,
    [ModifiedUserId] INT           NULL,
    [Disabled]       BIT           NOT NULL,
    [DateDeleted]    DATETIME2 (7) NULL,
    [DeletedUserId]  INT           NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC) ON [CommonDATA1],
    CONSTRAINT [FK_User_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [FK_User_UserType] FOREIGN KEY ([UserTypeId]) REFERENCES [Security].[UserType] ([Id])
);

