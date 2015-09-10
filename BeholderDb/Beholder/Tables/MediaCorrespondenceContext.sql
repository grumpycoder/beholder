CREATE TABLE [Beholder].[MediaCorrespondenceContext] (
    [Id]                    INT                        IDENTITY (1, 1) NOT NULL,
    [FileStreamID]          UNIQUEIDENTIFIER           CONSTRAINT [DF_MediaCorrespondenceContext_FileStreamID] DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [MimeTypeId]            INT                        NOT NULL,
    [FileName]              VARCHAR (255)              NULL,
    [DocumentExtension]     VARCHAR (10)               NULL,
    [ContextText]           VARBINARY (MAX) FILESTREAM CONSTRAINT [DF_MediaCorrespondenceContext_ContextText] DEFAULT (0x) NULL,
    [MediaCorrespondenceId] INT                        NULL,
    [IsUncompressed]        BIT                        NULL,
    [ContextText1]          VARBINARY (MAX)            NULL,
    CONSTRAINT [PK_MediaCorrespondenceContext] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderFileStreamDBScheme] ([Id]),
    UNIQUE NONCLUSTERED ([FileStreamID] ASC) ON [PRIMARY],
    CONSTRAINT [UQ_MediaCorrContext1] UNIQUE NONCLUSTERED ([FileStreamID] ASC, [Id] ASC) ON [BeholderFileStreamDBScheme] ([Id]),
    CONSTRAINT [UQ_MediaCorrContext2] UNIQUE NONCLUSTERED ([FileStreamID] ASC, [Id] ASC) ON [BeholderFileStreamDBScheme] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [idx_nc_MediaCorrespondenceId]
    ON [Beholder].[MediaCorrespondenceContext]([MediaCorrespondenceId] ASC)
    ON [Index];

