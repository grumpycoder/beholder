﻿CREATE TABLE [Beholder].[MediaImage] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [MediaTypeId]             INT            NOT NULL,
    [ImageTypeId]             INT            NULL,
    [ImageTitle]              VARCHAR (100)  NULL,
    [PhotographerArtist]      VARCHAR (80)   NULL,
    [DatePublished]           DATETIME2 (7)  NULL,
    [RollFrameNumber]         VARCHAR (15)   NULL,
    [NewsSourceId]            INT            NULL,
    [ContactOwner]            VARCHAR (80)   NULL,
    [MovementClassId]         INT            NULL,
    [ConfidentialityTypeId]   INT            NULL,
    [DateRenewalPermission]   DATETIME2 (7)  NULL,
    [RenewalPermissionTypeId] INT            NULL,
    [RenewalPermission]       VARCHAR (500)  NULL,
    [Summary]                 VARCHAR (4000) NULL,
    [City]                    VARCHAR (40)   NULL,
    [County]                  VARCHAR (40)   NULL,
    [StateId]                 INT            NULL,
    [BatchSortOrder]          INT            NULL,
    [ImageFileName]           VARCHAR (100)  NULL,
    [DateTaken]               DATETIME2 (7)  NULL,
    [DateCreated]             DATETIME2 (7)  NULL,
    [CreatedUserId]           INT            NULL,
    [DateModified]            DATETIME2 (7)  NULL,
    [ModifiedUserId]          INT            NULL,
    [RemovalStatusId]         INT            NULL,
    [RemovalReason]           VARCHAR (50)   NULL,
    [DateDeleted]             DATETIME2 (7)  NULL,
    [DeletedUserId]           INT            NULL,
    [ImageId]                 INT            NULL,
    [CatalogId]               VARCHAR (20)   NULL,
    CONSTRAINT [PK_MediaImage] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_MediaImage_Image] FOREIGN KEY ([ImageId]) REFERENCES [Beholder].[Image] ([Id])
);

