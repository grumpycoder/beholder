CREATE TABLE [Beholder].[OrganizationStatusHistory] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [OrganizationId]   INT           NOT NULL,
    [ActiveStatusId]   INT           NOT NULL,
    [ActiveYear]       INT           NOT NULL,
    [ReportStatusFlag] BIT           NOT NULL,
    [DateCreated]      DATETIME2 (7) CONSTRAINT [DF_OrganizationStatusHistory_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]    INT           NOT NULL,
    [DateModified]     DATETIME2 (7) NULL,
    [ModifiedUserId]   INT           NULL,
    [DateDeleted]      DATETIME2 (7) NULL,
    [DeletedUserId]    INT           NULL,
    CONSTRAINT [PK_OrganizationStatusHistory] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_OrganizationStatusHistory_ActiveStatus] FOREIGN KEY ([ActiveStatusId]) REFERENCES [Beholder].[ActiveStatus] ([Id]),
    CONSTRAINT [FK_OrganizationStatusHistory_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_OrganizationStatusHistory_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_OrganizationStatusHistory_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_OrganizationStatusHistory_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Beholder].[Organization] ([Id])
);

