CREATE TABLE [Beholder].[MediaCorrespondenceMediaCorrespondenceRel] (
    [Id]                     INT           IDENTITY (1, 1) NOT NULL,
    [MediaCorrespondenceId]  INT           NOT NULL,
    [MediaCorrespondence2Id] INT           NOT NULL,
    [DateStart]              DATETIME2 (7) NULL,
    [DateEnd]                DATETIME2 (7) NULL,
    [RelationshipTypeId]     INT           NOT NULL,
    [ApprovalStatusId]       INT           NULL,
    [PrimaryStatusId]        INT           NULL,
    CONSTRAINT [PK_MediaCorrespondenceMediaCorrespondenceRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaCorrespondenceMediaCorrespondenceRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceMediaCorrespondenceRel_MediaCorrespondence] FOREIGN KEY ([MediaCorrespondenceId]) REFERENCES [Beholder].[MediaCorrespondence] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceMediaCorrespondenceRel_MediaCorrespondence1] FOREIGN KEY ([MediaCorrespondence2Id]) REFERENCES [Beholder].[MediaCorrespondence] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceMediaCorrespondenceRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceMediaCorrespondenceRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

