CREATE TABLE [Beholder].[ContactEventRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [ContactId]          INT           NOT NULL,
    [EventId]            INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NOT NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_ContactEventRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_ContactEventRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_ContactEventRel_Contact] FOREIGN KEY ([ContactId]) REFERENCES [Beholder].[Contact] ([Id]),
    CONSTRAINT [FK_ContactEventRel_Event] FOREIGN KEY ([EventId]) REFERENCES [Beholder].[Event] ([Id]),
    CONSTRAINT [FK_ContactEventRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_ContactEventRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

