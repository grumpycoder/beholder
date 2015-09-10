CREATE TABLE [Beholder].[MediaImageVehicleRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [MediaImageId]       INT           NOT NULL,
    [VehicleId]          INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NOT NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_MediaImageVehicleRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaImageVehicleRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_MediaImageVehicleRel_MediaImage] FOREIGN KEY ([MediaImageId]) REFERENCES [Beholder].[MediaImage] ([Id]),
    CONSTRAINT [FK_MediaImageVehicleRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_MediaImageVehicleRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id]),
    CONSTRAINT [FK_MediaImageVehicleRel_Vehicle] FOREIGN KEY ([VehicleId]) REFERENCES [Beholder].[Vehicle] ([Id])
);

