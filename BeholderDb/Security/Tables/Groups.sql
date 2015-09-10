CREATE TABLE [Security].[Groups] (
    [Id]                    INT          IDENTITY (1, 1) NOT NULL,
    [Name]                  VARCHAR (50) NOT NULL,
    [ConfidentialityTypeId] INT          NULL,
    [DateCreated]           DATETIME     NULL,
    [CreatedUserId]         INT          NULL,
    [DateModified]          DATETIME     NULL,
    [ModifiedUserId]        INT          NULL,
    [DateDeleted]           DATETIME     NULL,
    [DeletedUserId]         INT          NULL,
    CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED ([Id] ASC) ON [CommonDATA1]
);

