CREATE TABLE [Beholder].[AliasOrganizationRel] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [OrganizationId]    INT           NOT NULL,
    [AliasId]           INT           NOT NULL,
    [PrimaryStatusId]   INT           NULL,
    [FirstKnownUseDate] DATETIME2 (7) NULL,
    [LastKnownUseDate]  DATETIME2 (7) NULL,
    [DateCreated]       DATETIME2 (7) CONSTRAINT [DF_AliasOrganizationRel_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]     INT           NOT NULL,
    [DateModified]      DATETIME2 (7) NULL,
    [ModifiedUserId]    INT           NULL,
    [DateDeleted]       DATETIME2 (7) NULL,
    [DeletedUserId]     INT           NULL,
    CONSTRAINT [PK_AliasOrganizationRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_AliasOrganizationRel_Alias] FOREIGN KEY ([AliasId]) REFERENCES [Common].[Alias] ([Id]),
    CONSTRAINT [FK_AliasOrganizationRel_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AliasOrganizationRel_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AliasOrganizationRel_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_AliasOrganizationRel_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Beholder].[Organization] ([Id]),
    CONSTRAINT [FK_AliasOrganizationRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id])
);

