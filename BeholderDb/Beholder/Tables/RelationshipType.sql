CREATE TABLE [Beholder].[RelationshipType] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [ObjectFrom]     VARCHAR (30)  NULL,
    [ObjectTo]       VARCHAR (30)  NULL,
    [Name]           VARCHAR (20)  NOT NULL,
    [SortOrder]      INT           NULL,
    [DateCreated]    DATETIME2 (7) NOT NULL,
    [CreatedUserId]  INT           NOT NULL,
    [DateModified]   DATETIME2 (7) NULL,
    [ModifiedUserId] INT           NULL,
    [DateDeleted]    DATETIME2 (7) NULL,
    [DeletedUserId]  INT           NULL,
    CONSTRAINT [PK_RelationshipType] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_RelationshipType_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_RelationshipType_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_RelationshipType_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id])
);

