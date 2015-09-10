CREATE TABLE [Beholder].[Contact] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [PersonId]              INT           NOT NULL,
    [ContactIdOld]          INT           NULL,
    [Profession]            VARCHAR (100) NULL,
    [ConfidentialityTypeId] INT           NULL,
    [ContactTypeId]         INT           NULL,
    [ContactTopicId]        INT           NULL,
    [DateCreated]           DATETIME2 (7) CONSTRAINT [DF_Contact_DateCreated] DEFAULT (getdate()) NULL,
    [CreatedUserId]         INT           NOT NULL,
    [DateModified]          DATETIME2 (7) NULL,
    [ModifiedUserId]        INT           NULL,
    [RemovalStatusId]       INT           NULL,
    [RemovalReason]         VARCHAR (50)  NULL,
    [DateDeleted]           DATETIME2 (7) NULL,
    [DeletedUserId]         INT           NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_Contact_ConfidentialityType] FOREIGN KEY ([ConfidentialityTypeId]) REFERENCES [Beholder].[ConfidentialityType] ([Id]),
    CONSTRAINT [FK_Contact_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_Contact_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_Contact_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_Contact_RemovalStatus] FOREIGN KEY ([RemovalStatusId]) REFERENCES [Beholder].[RemovalStatus] ([Id])
);

