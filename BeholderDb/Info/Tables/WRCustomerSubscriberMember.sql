CREATE TABLE [Info].[WRCustomerSubscriberMember] (
    [Lastname]          VARCHAR (36)  NULL,
    [Firstname]         VARCHAR (18)  NULL,
    [DOB]               INT           NULL,
    [Suffix]            VARCHAR (18)  NULL,
    [ADDR1]             VARCHAR (35)  NULL,
    [ADDR2]             VARCHAR (32)  NULL,
    [CITY]              VARCHAR (24)  NULL,
    [STATE]             VARCHAR (2)   NULL,
    [ZIP]               VARCHAR (5)   NULL,
    [ZIP4]              VARCHAR (4)   NULL,
    [Phone]             VARCHAR (25)  NULL,
    [Email]             VARCHAR (MAX) NULL,
    [Fax]               VARCHAR (25)  NULL,
    [CreditCardNo]      VARCHAR (25)  NULL,
    [ExpDate]           VARCHAR (10)  NULL,
    [CustomerId]        VARCHAR (5)   NOT NULL,
    [Info]              VARCHAR (MAX) NULL,
    [LastTransactionId] INT           NULL,
    CONSTRAINT [PK_WRCustomerSubscriberMember] PRIMARY KEY CLUSTERED ([CustomerId] ASC)
);

