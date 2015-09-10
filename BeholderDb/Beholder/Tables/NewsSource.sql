CREATE TABLE [Beholder].[NewsSource] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [NewsSourceName]   VARCHAR (100) NOT NULL,
    [NewsSourceTypeId] INT           NULL,
    [DateCreated]      DATETIME2 (7) CONSTRAINT [DF_NewsSource_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]    INT           NOT NULL,
    [DateModified]     DATETIME2 (7) NULL,
    [ModifiedUserId]   INT           NULL,
    [DateDeleted]      DATETIME2 (7) NULL,
    [DeletedUserId]    INT           NULL,
    CONSTRAINT [PK_NewsSource] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_NewsSource_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_NewsSource_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_NewsSource_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_NewsSource_NewsSourceType] FOREIGN KEY ([NewsSourceTypeId]) REFERENCES [Beholder].[NewsSourceType] ([Id])
);

