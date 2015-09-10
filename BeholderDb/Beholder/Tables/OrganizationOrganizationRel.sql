CREATE TABLE [Beholder].[OrganizationOrganizationRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [OrganizationId]     INT           NOT NULL,
    [Organization2Id]    INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NOT NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_OrganizationOrganizationRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_OrganizationOrganizationRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_OrganizationOrganizationRel_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Beholder].[Organization] ([Id]),
    CONSTRAINT [FK_OrganizationOrganizationRel_Organization1] FOREIGN KEY ([Organization2Id]) REFERENCES [Beholder].[Organization] ([Id]),
    CONSTRAINT [FK_OrganizationOrganizationRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_OrganizationOrganizationRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

