CREATE TABLE [Beholder].[EventStatusHistory] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [EventId]          INT           NOT NULL,
    [ActiveStatusId]   INT           NOT NULL,
    [ActiveYear]       INT           NOT NULL,
    [ReportStatusFlag] BIT           NOT NULL,
    [DateCreated]      DATETIME2 (7) NOT NULL,
    [CreatedUserId]    INT           NOT NULL,
    [DateModified]     DATETIME2 (7) NULL,
    [ModifiedUserId]   INT           NULL,
    [DateDeleted]      DATETIME2 (7) NULL,
    [DeletedUserId]    INT           NULL,
    CONSTRAINT [PK_EventStatusHistory] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_EventStatusHistory_ActiveStatus] FOREIGN KEY ([ActiveStatusId]) REFERENCES [Beholder].[ActiveStatus] ([Id]),
    CONSTRAINT [FK_EventStatusHistory_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_EventStatusHistory_Event] FOREIGN KEY ([EventId]) REFERENCES [Beholder].[Event] ([Id]),
    CONSTRAINT [FK_EventStatusHistory_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_EventStatusHistory_User] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id])
);

