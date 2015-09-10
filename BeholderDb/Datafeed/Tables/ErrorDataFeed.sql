CREATE TABLE [Datafeed].[ErrorDataFeed] (
    [ErrorDataFeedId] INT           IDENTITY (1, 1) NOT NULL,
    [MasterFeedLogId] INT           NOT NULL,
    [RecordSets]      XML           NULL,
    [ProcessedDate]   DATETIME      CONSTRAINT [DF_ErrorDataFeed_ProcessedDate] DEFAULT (getdate()) NULL,
    [Comment]         VARCHAR (512) NULL,
    CONSTRAINT [PK_ErrorDataFeed] PRIMARY KEY CLUSTERED ([ErrorDataFeedId] ASC)
);

