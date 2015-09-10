CREATE TABLE [Beholder].[MediaCorrespondenceVehicleRel] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [MediaCorrespondenceId] INT           NOT NULL,
    [VehicleId]             INT           NOT NULL,
    [DateStart]             DATETIME2 (7) NULL,
    [DateEnd]               DATETIME2 (7) NULL,
    [RelationshipTypeId]    INT           NOT NULL,
    [ApprovalStatusId]      INT           NULL,
    [PrimaryStatusId]       INT           NULL,
    CONSTRAINT [PK_MediaCorrespondenceVehicleRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaCorrespondenceVehicleRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceVehicleRel_MediaCorrespondence] FOREIGN KEY ([MediaCorrespondenceId]) REFERENCES [Beholder].[MediaCorrespondence] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceVehicleRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceVehicleRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id]),
    CONSTRAINT [FK_MediaCorrespondenceVehicleRel_Vehicle] FOREIGN KEY ([VehicleId]) REFERENCES [Beholder].[Vehicle] ([Id])
);

