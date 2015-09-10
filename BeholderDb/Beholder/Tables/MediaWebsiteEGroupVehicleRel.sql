CREATE TABLE [Beholder].[MediaWebsiteEGroupVehicleRel] (
    [Id]                   INT           IDENTITY (1, 1) NOT NULL,
    [MediaWebsiteEGroupId] INT           NOT NULL,
    [VehicleId]            INT           NOT NULL,
    [DateStart]            DATETIME2 (7) NULL,
    [DateEnd]              DATETIME2 (7) NULL,
    [RelationshipTypeId]   INT           NOT NULL,
    [ApprovalStatusId]     INT           NULL,
    [PrimaryStatusId]      INT           NULL,
    CONSTRAINT [PK_MediaWebsiteEGroupVehicleRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaWebsiteEGroupVehicleRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_MediaWebsiteEGroupVehicleRel_MediaWebsiteEGroup] FOREIGN KEY ([MediaWebsiteEGroupId]) REFERENCES [Beholder].[MediaWebsiteEGroup] ([Id]),
    CONSTRAINT [FK_MediaWebsiteEGroupVehicleRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_MediaWebsiteEGroupVehicleRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id]),
    CONSTRAINT [FK_MediaWebsiteEGroupVehicleRel_Vehicle] FOREIGN KEY ([VehicleId]) REFERENCES [Beholder].[Vehicle] ([Id])
);

