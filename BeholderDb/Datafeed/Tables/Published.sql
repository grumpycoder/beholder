CREATE TABLE [Datafeed].[Published] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [BatchId]         INT           NOT NULL,
    [BatchNumber]     VARCHAR (30)  NOT NULL,
    [PublicationInfo] VARCHAR (200) NULL,
    [PublicationDate] DATETIME2 (7) NULL,
    [DateReceived]    DATETIME2 (7) CONSTRAINT [DF_Published_DateReceived] DEFAULT (getdate()) NULL,
    [MovementClassId] INT           NULL,
    [FileLocation]    VARCHAR (512) NULL,
    CONSTRAINT [PK_Published] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1]
);

