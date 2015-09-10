CREATE TABLE [Beholder].[OrganizationComment] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [OrganizationId] INT            NOT NULL,
    [Comment]        VARCHAR (4000) NOT NULL,
    [DateCreated]    DATETIME2 (7)  CONSTRAINT [DF_OrganizationComment_DateCreated] DEFAULT (getdate()) NULL,
    [CreatedUserId]  INT            NULL,
    [DateModified]   DATETIME2 (7)  NULL,
    [ModifiedUserId] INT            NULL,
    [DateDeleted]    DATETIME2 (7)  NULL,
    [DeletedUserId]  INT            NULL,
    CONSTRAINT [PK_OrganizationComments] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_OrganizationComment_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_OrganizationComment_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_OrganizationComment_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_OrganizationComment_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Beholder].[Organization] ([Id])
);

