CREATE TABLE [Datafeed].[FeedType] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (100) NOT NULL,
    [DateCreated]    DATETIME2 (7) CONSTRAINT [DF_FeedType_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]  INT           NOT NULL,
    [DateModified]   DATETIME2 (7) NULL,
    [ModifiedUserId] INT           NULL,
    [DateDeleted]    DATETIME2 (7) NULL,
    [DeletedUserId]  INT           NULL,
    CONSTRAINT [pk_FeedType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

