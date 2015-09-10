CREATE TABLE [Beholder].[MediaWebsiteEGroupContext] (
    [Id]                   INT                        IDENTITY (1, 1) NOT NULL,
    [FileStreamID]         UNIQUEIDENTIFIER           CONSTRAINT [DF_MediaWebsiteEGroupContext_FileStreamID] DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [MimeTypeId]           INT                        NOT NULL,
    [FileName]             VARCHAR (255)              NULL,
    [DocumentExtension]    VARCHAR (10)               NULL,
    [ContextText]          VARBINARY (MAX) FILESTREAM CONSTRAINT [DF_MediaWebsiteEGroupContext_ContextText] DEFAULT (0x) NULL,
    [MediaWebsiteEGroupId] INT                        NULL,
    [IsUncompressed]       BIT                        NULL,
    [ContextText1]         VARBINARY (MAX)            NULL,
    CONSTRAINT [PK_MediaWebsiteEGroupContext] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderFileStreamDBScheme] ([Id]),
    UNIQUE NONCLUSTERED ([FileStreamID] ASC) ON [PRIMARY],
    CONSTRAINT [UQ_MediaWebsiteEGroupContext1] UNIQUE NONCLUSTERED ([FileStreamID] ASC, [Id] ASC) ON [BeholderFileStreamDBScheme] ([Id]),
    CONSTRAINT [UQ_MediaWebsiteEGroupContext2] UNIQUE NONCLUSTERED ([FileStreamID] ASC, [Id] ASC) ON [BeholderFileStreamDBScheme] ([Id])
);

