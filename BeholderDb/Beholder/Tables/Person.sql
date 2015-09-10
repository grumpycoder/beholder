CREATE TABLE [Beholder].[Person] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [PersonId]              INT            NOT NULL,
    [MovementClassId]       INT            NULL,
    [ConfidentialityTypeId] INT            NULL,
    [DistinguishableMarks]  VARCHAR (1012) NULL,
    [DateCreated]           DATETIME2 (7)  CONSTRAINT [DF_Person_DateCreated] DEFAULT (getdate()) NULL,
    [CreatedUserId]         INT            NOT NULL,
    [DateModified]          DATETIME2 (7)  NULL,
    [ModifiedUserId]        INT            NULL,
    [RemovalStatusId]       INT            NULL,
    [RemovalReason]         VARCHAR (50)   NULL,
    [DateDeleted]           DATETIME2 (7)  NULL,
    [DeletedUserId]         INT            NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_BeholderPerson_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_BeholderPerson_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_BeholderPerson_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_Person_ConfidentialityType] FOREIGN KEY ([ConfidentialityTypeId]) REFERENCES [Beholder].[ConfidentialityType] ([Id]),
    CONSTRAINT [FK_Person_MovementClass] FOREIGN KEY ([MovementClassId]) REFERENCES [Beholder].[MovementClass] ([Id]),
    CONSTRAINT [FK_Person_RemovalStatus] FOREIGN KEY ([RemovalStatusId]) REFERENCES [Beholder].[RemovalStatus] ([Id])
);

