CREATE TABLE [Beholder].[PersonScreenName] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [PersonId]          INT           NOT NULL,
    [ScreenName]        VARCHAR (100) NOT NULL,
    [UsedAt]            VARCHAR (100) NULL,
    [PrimaryStatusId]   INT           NULL,
    [FirstKnownUseDate] DATETIME2 (7) NULL,
    [LastKnownUseDate]  DATETIME2 (7) NULL,
    [DateCreated]       DATETIME2 (7) CONSTRAINT [DF_PersonScreenName_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]     INT           NOT NULL,
    [DateModified]      DATETIME2 (7) NULL,
    [ModifiedUserId]    INT           NULL,
    [DateDeleted]       DATETIME2 (7) NULL,
    [DeletedUserId]     INT           NULL,
    CONSTRAINT [PK_PersonScreenName] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_PersonScreenName_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_PersonScreenName_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_PersonScreenName_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_PersonScreenName_Person] FOREIGN KEY ([PersonId]) REFERENCES [Beholder].[Person] ([Id]),
    CONSTRAINT [FK_PersonScreenName_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id])
);

