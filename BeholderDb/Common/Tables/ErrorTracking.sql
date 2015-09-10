CREATE TABLE [Common].[ErrorTracking] (
    [ErrorNoID]      INT           NOT NULL,
    [ErrorNo]        INT           NULL,
    [ErrorProcedure] VARCHAR (100) NULL,
    [ErrorDate]      DATETIME      NULL,
    [ErrorMessage]   VARCHAR (MAX) NULL,
    [ErrorSeverity]  INT           NULL,
    [DatabaseName]   VARCHAR (50)  NULL,
    [WebError]       XML           NULL,
    [DateCreated]    DATETIME      CONSTRAINT [DF_ErrorTracking_DateCreated] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ErrorTracking] PRIMARY KEY CLUSTERED ([ErrorNoID] ASC) ON [CommonDATA1]
) TEXTIMAGE_ON [CommonDATA1];

