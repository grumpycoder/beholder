CREATE TABLE [Beholder].[MediaAudioVideoComment] (
    [Id]                INT            IDENTITY (1, 1) NOT NULL,
    [MediaAudioVideoId] INT            NOT NULL,
    [Comment]           VARCHAR (4000) NOT NULL,
    [DateCreated]       DATETIME2 (7)  CONSTRAINT [DF_MediaAudioVideoComment_DateCreated] DEFAULT (getdate()) NULL,
    [CreatedUserId]     INT            NULL,
    [DateModified]      DATETIME2 (7)  NULL,
    [ModifiedUserId]    INT            NULL,
    [DateDeleted]       DATETIME2 (7)  NULL,
    [DeletedUserId]     INT            NULL,
    CONSTRAINT [PK_MediaAudioVideoComment] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaAudioVideoComment_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_MediaAudioVideoComment_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_MediaAudioVideoComment_MediaAudioVideo] FOREIGN KEY ([MediaAudioVideoId]) REFERENCES [Beholder].[MediaAudioVideo] ([Id]),
    CONSTRAINT [FK_MediaAudioVideoComment_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id])
);

