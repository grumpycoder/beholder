CREATE TABLE [Beholder].[PersonPersonRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [PersonId]           INT           NOT NULL,
    [Person2Id]          INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NOT NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_PersonPersonRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_PersonPersonRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_PersonPersonRel_Person] FOREIGN KEY ([PersonId]) REFERENCES [Beholder].[Person] ([Id]),
    CONSTRAINT [FK_PersonPersonRel_Person1] FOREIGN KEY ([Person2Id]) REFERENCES [Beholder].[Person] ([Id]),
    CONSTRAINT [FK_PersonPersonRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_PersonPersonRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

