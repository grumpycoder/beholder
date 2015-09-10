CREATE TABLE [Beholder].[ChapterStatusHistory] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [ChapterId]        INT           NOT NULL,
    [ActiveStatusId]   INT           NOT NULL,
    [ActiveYear]       INT           NOT NULL,
    [ReportStatusFlag] BIT           NOT NULL,
    [DateCreated]      DATETIME2 (7) CONSTRAINT [DF_ChapterStatusHistory_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]    INT           NOT NULL,
    [DateModified]     DATETIME2 (7) NULL,
    [ModifiedUserId]   INT           NULL,
    [DateDeleted]      DATETIME2 (7) NULL,
    [DeletedUserId]    INT           NULL,
    CONSTRAINT [PK_ChapterStatusHistory] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_ChapterStatusHistory_ActiveStatus] FOREIGN KEY ([ActiveStatusId]) REFERENCES [Beholder].[ActiveStatus] ([Id]),
    CONSTRAINT [FK_ChapterStatusHistory_Chapter] FOREIGN KEY ([ChapterId]) REFERENCES [Beholder].[Chapter] ([Id]),
    CONSTRAINT [FK_ChapterStatusHistory_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_ChapterStatusHistory_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_ChapterStatusHistory_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id])
);

