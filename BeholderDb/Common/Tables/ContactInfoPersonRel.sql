CREATE TABLE [Common].[ContactInfoPersonRel] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [PersonId]          INT           NOT NULL,
    [ContactId]         INT           NOT NULL,
    [ContactTypeId]     INT           NOT NULL,
    [FirstKnownUseDate] DATETIME2 (7) NULL,
    [LastKnownUseDate]  DATETIME2 (7) NULL,
    [PrimaryStatusId]   INT           NULL,
    [DateCreated]       DATETIME2 (7) CONSTRAINT [DF_ContactPersonRel_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]     INT           NOT NULL,
    [DateModified]      DATETIME2 (7) NULL,
    [ModifiedUserId]    INT           NULL,
    [DateDeleted]       DATETIME2 (7) NULL,
    [DeletedUserId]     INT           NULL,
    CONSTRAINT [PK_ContactPersonRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [CommonDATA1],
    CONSTRAINT [FK_ContactPersonRel_Contact] FOREIGN KEY ([ContactId]) REFERENCES [Common].[ContactInfo] ([Id]),
    CONSTRAINT [FK_ContactPersonRel_ContactType] FOREIGN KEY ([ContactTypeId]) REFERENCES [Common].[ContactInfoType] ([Id]),
    CONSTRAINT [FK_ContactPersonRel_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_ContactPersonRel_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_ContactPersonRel_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_ContactPersonRel_Person] FOREIGN KEY ([PersonId]) REFERENCES [Common].[Person] ([Id]),
    CONSTRAINT [FK_ContactPersonRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id])
);

