﻿CREATE TABLE [Beholder].[ChapterContactInfoRel] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [ChapterId]         INT           NOT NULL,
    [ContactId]         INT           NOT NULL,
    [ContactTypeId]     INT           NOT NULL,
    [FirstKnownUseDate] DATETIME2 (7) NULL,
    [LastKnownUseDate]  DATETIME2 (7) NULL,
    [PrimaryStatusId]   INT           NULL,
    [Priority]          VARCHAR (50)  CONSTRAINT [DF_ContactChapterRel_Priority] DEFAULT ('Primary') NOT NULL,
    [DateCreated]       DATETIME2 (7) CONSTRAINT [DF_ContactChapterRel_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]     INT           NOT NULL,
    [DateModified]      DATETIME2 (7) NULL,
    [ModifiedUserId]    INT           NULL,
    [DateDeleted]       DATETIME2 (7) NULL,
    [DeletedUserId]     INT           NULL,
    CONSTRAINT [PK_ContactChapterRel] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_ContactChapterRel_Chapter] FOREIGN KEY ([ChapterId]) REFERENCES [Beholder].[Chapter] ([Id]),
    CONSTRAINT [FK_ContactChapterRel_Contact] FOREIGN KEY ([ContactId]) REFERENCES [Common].[ContactInfo] ([Id]),
    CONSTRAINT [FK_ContactChapterRel_ContactType] FOREIGN KEY ([ContactTypeId]) REFERENCES [Common].[ContactInfoType] ([Id]),
    CONSTRAINT [FK_ContactChapterRel_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_ContactChapterRel_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_ContactChapterRel_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_ContactChapterRel_PrimaryStatus] FOREIGN KEY ([PrimaryStatusId]) REFERENCES [Common].[PrimaryStatus] ([Id])
);

