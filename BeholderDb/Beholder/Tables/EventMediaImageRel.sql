CREATE TABLE [Beholder].[EventMediaImageRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [EventId]            INT           NOT NULL,
    [MediaImageId]       INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NOT NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_EventMediaImageRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_EventMediaImageRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_EventMediaImageRel_MediaImage] FOREIGN KEY ([MediaImageId]) REFERENCES [Beholder].[MediaImage] ([Id]),
    CONSTRAINT [FK_EventMediaImageRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_EventMediaImageRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id])
);

