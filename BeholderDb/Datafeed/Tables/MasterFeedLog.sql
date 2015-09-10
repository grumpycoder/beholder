CREATE TABLE [Datafeed].[MasterFeedLog] (
    [MasterFeedLogId]  INT           IDENTITY (1, 1) NOT NULL,
    [FeedTypeId]       INT           NOT NULL,
    [RecordCount]      INT           NOT NULL,
    [ErrorRecordCount] INT           NOT NULL,
    [ValidRecordCount] INT           NOT NULL,
    [IsSuccess]        BIT           NOT NULL,
    [IsDebug]          BIT           NOT NULL,
    [EntryDate]        DATETIME2 (7) CONSTRAINT [DF_MasterFeedLog_EntryDate] DEFAULT (getdate()) NOT NULL,
    [FileHashValue]    VARCHAR (512) NULL,
    [FeedDate]         DATE          NULL,
    CONSTRAINT [pk_MasterFeedLog] PRIMARY KEY CLUSTERED ([MasterFeedLogId] ASC)
);

