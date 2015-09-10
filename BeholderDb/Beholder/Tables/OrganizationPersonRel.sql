CREATE TABLE [Beholder].[OrganizationPersonRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [OrganizationId]     INT           NOT NULL,
    [PersonId]           INT           NOT NULL,
    [DateStart]          DATETIME2 (7) CONSTRAINT [DF_OrganizationPersonRel_DateCreated] DEFAULT (getdate()) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_OrganizationPersonRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_OrganizationPersonRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_OrganizationPersonRel_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Beholder].[Organization] ([Id]),
    CONSTRAINT [FK_OrganizationPersonRel_Organization1] FOREIGN KEY ([OrganizationId]) REFERENCES [Beholder].[Organization] ([Id]),
    CONSTRAINT [FK_OrganizationPersonRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_OrganizationPersonRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

