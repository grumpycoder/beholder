CREATE TABLE [Common].[DBAudit] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [RevisionStamp]  DATETIME2 (7)  NOT NULL,
    [TableName]      VARCHAR (255)  NOT NULL,
    [KeyValue]       INT            NULL,
    [UserName]       VARCHAR (255)  NOT NULL,
    [Actions]        VARCHAR (255)  NOT NULL,
    [OldData]        VARCHAR (MAX)  NULL,
    [NewData]        VARCHAR (MAX)  NULL,
    [ChangedColumns] VARCHAR (4000) NULL,
    CONSTRAINT [PK_DBAudit] PRIMARY KEY CLUSTERED ([Id] ASC) ON [CommonDATA1]
) TEXTIMAGE_ON [CommonDATA1];

