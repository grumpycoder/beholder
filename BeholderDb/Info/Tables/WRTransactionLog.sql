CREATE TABLE [Info].[WRTransactionLog] (
    [TransactionId]          INT           IDENTITY (1, 1) NOT NULL,
    [TransactionDate]        VARCHAR (50)  NULL,
    [TransactionDescription] VARCHAR (255) NULL,
    [TransactionAmount]      MONEY         NULL,
    [CustomerId]             VARCHAR (5)   NULL,
    CONSTRAINT [PK_WRTransactionLog] PRIMARY KEY CLUSTERED ([TransactionId] ASC)
);

