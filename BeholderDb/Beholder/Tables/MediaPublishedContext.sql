CREATE TABLE [Beholder].[MediaPublishedContext] (
    [Id]                INT                        IDENTITY (1, 1) NOT NULL,
    [FileStreamID]      UNIQUEIDENTIFIER           CONSTRAINT [DF_MediaPublishedContext_FileStreamID] DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [MimeTypeId]        INT                        NOT NULL,
    [FileName]          VARCHAR (255)              NULL,
    [DocumentExtension] VARCHAR (10)               NULL,
    [ContextText]       VARBINARY (MAX) FILESTREAM CONSTRAINT [DF_MediaPublishedContext_ContextText] DEFAULT (0x) NULL,
    [MediaPublishedId]  INT                        NULL,
    [IsUncompressed]    BIT                        NULL,
    [ContextText1]      VARBINARY (MAX)            NULL,
    CONSTRAINT [PK_MediaPublishedContext] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderFileStreamDBScheme] ([Id]),
    UNIQUE NONCLUSTERED ([FileStreamID] ASC) ON [PRIMARY],
    CONSTRAINT [UQ_MediaPublishedContext1] UNIQUE NONCLUSTERED ([FileStreamID] ASC, [Id] ASC) ON [BeholderFileStreamDBScheme] ([Id]),
    CONSTRAINT [UQ_MediaPublishedContext2] UNIQUE NONCLUSTERED ([FileStreamID] ASC, [Id] ASC) ON [BeholderFileStreamDBScheme] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [idx_nc_MediaPublishedId]
    ON [Beholder].[MediaPublishedContext]([MediaPublishedId] ASC)
    ON [Index];

