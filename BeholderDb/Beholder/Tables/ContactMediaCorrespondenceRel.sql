CREATE TABLE [Beholder].[ContactMediaCorrespondenceRel] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [ContactId]             INT           NOT NULL,
    [MediaCorrespondenceId] INT           NOT NULL,
    [DateStart]             DATETIME2 (7) NULL,
    [DateEnd]               DATETIME2 (7) NULL,
    [RelationshipTypeId]    INT           NOT NULL,
    [ApprovalStatusId]      INT           NULL,
    [PrimaryStatusId]       INT           NULL,
    CONSTRAINT [PK_ContactMediaCorrespondenceRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_ContactMediaCorrespondenceRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_ContactMediaCorrespondenceRel_Contact] FOREIGN KEY ([ContactId]) REFERENCES [Beholder].[Contact] ([Id]),
    CONSTRAINT [FK_ContactMediaCorrespondenceRel_MediaCorrespondence] FOREIGN KEY ([MediaCorrespondenceId]) REFERENCES [Beholder].[MediaCorrespondence] ([Id]),
    CONSTRAINT [FK_ContactMediaCorrespondenceRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_ContactMediaCorrespondenceRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

