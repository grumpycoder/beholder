CREATE TABLE [Beholder].[EventEventTypeRel] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [EventId]     INT NOT NULL,
    [EventTypeId] INT NOT NULL,
    CONSTRAINT [PK_EventEventTypeRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_EventEventTypeRel_Event] FOREIGN KEY ([EventId]) REFERENCES [Beholder].[Event] ([Id]),
    CONSTRAINT [FK_EventEventTypeRel_EventType] FOREIGN KEY ([EventTypeId]) REFERENCES [Beholder].[EventType] ([Id])
);

