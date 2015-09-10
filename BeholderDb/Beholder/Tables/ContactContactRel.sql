CREATE TABLE [Beholder].[ContactContactRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [ContactId]          INT           NOT NULL,
    [Contact2Id]         INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NOT NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_ContactContactRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_ContactContactRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_ContactContactRel_Contact] FOREIGN KEY ([ContactId]) REFERENCES [Beholder].[Contact] ([Id]),
    CONSTRAINT [FK_ContactContactRel_Contact1] FOREIGN KEY ([Contact2Id]) REFERENCES [Beholder].[Contact] ([Id]),
    CONSTRAINT [FK_ContactContactRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_ContactContactRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

