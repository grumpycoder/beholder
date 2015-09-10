CREATE TABLE [Beholder].[PersonMediaCorrespondenceRel] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [PersonId]              INT           NOT NULL,
    [MediaCorrespondenceId] INT           NOT NULL,
    [DateStart]             DATETIME2 (7) NULL,
    [DateEnd]               DATETIME2 (7) NULL,
    [RelationshipTypeId]    INT           NOT NULL,
    [ApprovalStatusId]      INT           NULL,
    [PrimaryStatusId]       INT           NULL,
    CONSTRAINT [PK_PersonMediaCorrespondenceRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_PersonMediaCorrespondenceRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_PersonMediaCorrespondenceRel_MediaCorrespondence] FOREIGN KEY ([MediaCorrespondenceId]) REFERENCES [Beholder].[MediaCorrespondence] ([Id]),
    CONSTRAINT [FK_PersonMediaCorrespondenceRel_Person] FOREIGN KEY ([PersonId]) REFERENCES [Beholder].[Person] ([Id]),
    CONSTRAINT [FK_PersonMediaCorrespondenceRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_PersonMediaCorrespondenceRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

