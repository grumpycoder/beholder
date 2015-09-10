CREATE TABLE [Beholder].[OrganizationEventRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [OrganizationId]     INT           NOT NULL,
    [EventId]            INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NOT NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_OrganizationEventRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_OrganizationEventRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_OrganizationEventRel_Organization] FOREIGN KEY ([OrganizationId]) REFERENCES [Beholder].[Organization] ([Id]),
    CONSTRAINT [FK_OrganizationEventRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_OrganizationEventRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

