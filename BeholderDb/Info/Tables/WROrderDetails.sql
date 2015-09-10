CREATE TABLE [Info].[WROrderDetails] (
    [OrderDetailID] INT          NOT NULL,
    [OrderID]       INT          NOT NULL,
    [ProductID]     VARCHAR (50) NULL,
    [DateSold]      DATETIME     NULL,
    [SaleCnt]       FLOAT (53)   NULL,
    [SalePrice]     MONEY        NULL,
    CONSTRAINT [PK_WROrderDetails] PRIMARY KEY CLUSTERED ([OrderDetailID] ASC),
    CONSTRAINT [FK_WROrderDetails_WROrders] FOREIGN KEY ([OrderID]) REFERENCES [Info].[WROrders] ([OrderID])
);

