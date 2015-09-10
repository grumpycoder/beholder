CREATE TABLE [Beholder].[EventVehicleRel] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [EventId]            INT           NOT NULL,
    [VehicleId]          INT           NOT NULL,
    [DateStart]          DATETIME2 (7) NULL,
    [DateEnd]            DATETIME2 (7) NULL,
    [RelationshipTypeId] INT           NOT NULL,
    [ApprovalStatusId]   INT           NULL,
    [PrimaryStatusId]    INT           NULL,
    CONSTRAINT [PK_EventVehicleRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_EventVehicleRel_ApprovalStatus] FOREIGN KEY ([ApprovalStatusId]) REFERENCES [Beholder].[ApprovalStatus] ([Id]),
    CONSTRAINT [FK_EventVehicleRel_Event] FOREIGN KEY ([EventId]) REFERENCES [Beholder].[Event] ([Id]),
    CONSTRAINT [FK_EventVehicleRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id]),
    CONSTRAINT [FK_EventVehicleRel_RelationshipType] FOREIGN KEY ([RelationshipTypeId]) REFERENCES [Beholder].[RelationshipType] ([Id]),
    CONSTRAINT [FK_EventVehicleRel_Vehicle] FOREIGN KEY ([VehicleId]) REFERENCES [Beholder].[Vehicle] ([Id])
);

