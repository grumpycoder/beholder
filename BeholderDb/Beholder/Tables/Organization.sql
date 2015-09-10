CREATE TABLE [Beholder].[Organization] (
    [Id]                    INT           IDENTITY (1, 1) NOT NULL,
    [OrganizationTypeId]    INT           NOT NULL,
    [OrganizationName]      VARCHAR (100) NOT NULL,
    [OrganizationDesc]      VARCHAR (512) NULL,
    [ApprovalStatusId]      INT           NOT NULL,
    [ActiveStatusId]        INT           NOT NULL,
    [ActiveYear]            INT           NOT NULL,
    [ReportStatusFlag]      BIT           NOT NULL,
    [FormedDate]            DATETIME2 (7) NULL,
    [DisbandDate]           DATETIME2 (7) NULL,
    [MovementClassId]       INT           NULL,
    [ConfidentialityTypeId] INT           NULL,
    [DateCreated]           DATETIME2 (7) CONSTRAINT [DF_Organization_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]         INT           NOT NULL,
    [DateModified]          DATETIME2 (7) NULL,
    [ModifiedUserId]        INT           NULL,
    [RemovalStatusId]       INT           NULL,
    [RemovalReason]         VARCHAR (50)  NULL,
    [DateDeleted]           DATETIME2 (7) NULL,
    [DeletedUserId]         INT           NULL,
    CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1]
);

