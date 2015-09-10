CREATE TABLE [Beholder].[Image] (
    [Id]                    INT                        IDENTITY (1, 1) NOT NULL,
    [FileStreamID]          UNIQUEIDENTIFIER           DEFAULT (newsequentialid()) ROWGUIDCOL NOT NULL,
    [MimeTypeId]            INT                        NOT NULL,
    [ConfidentialityTypeId] INT                        NULL,
    [IMAGE]                 VARBINARY (MAX) FILESTREAM DEFAULT (0x) NULL,
    [DateCreated]           DATETIME2 (7)              NOT NULL,
    [CreatedUserId]         INT                        NOT NULL,
    [DateModified]          DATETIME2 (7)              NULL,
    [ModifiedUserId]        INT                        NULL,
    [DateDeleted]           DATETIME2 (7)              NULL,
    [DeletedUserId]         INT                        NULL,
    [OLD_OBJECT_ID]         INT                        NULL,
    CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderFileStreamDBScheme] ([Id]),
    UNIQUE NONCLUSTERED ([FileStreamID] ASC) ON [PRIMARY],
    UNIQUE NONCLUSTERED ([FileStreamID] ASC, [Id] ASC) ON [BeholderFileStreamDBScheme] ([Id])
);

