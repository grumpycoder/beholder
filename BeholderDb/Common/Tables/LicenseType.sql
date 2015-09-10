CREATE TABLE [Common].[LicenseType] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (100) NOT NULL,
    [SortOrder]      INT           NULL,
    [DateCreated]    DATETIME2 (7) CONSTRAINT [DF_LicenseType_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]  INT           NOT NULL,
    [DateModified]   DATETIME2 (7) NULL,
    [ModifiedUserId] INT           NULL,
    [DateDeleted]    DATETIME2 (7) NULL,
    [DeletedUserId]  INT           NULL,
    CONSTRAINT [PK_LicenseType] PRIMARY KEY CLUSTERED ([Id] ASC) ON [CommonDATA1],
    CONSTRAINT [FK_LicenseType_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_LicenseType_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_LicenseType_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id])
);

