CREATE TABLE [Beholder].[VehicleComment] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [VehicleId]      INT            NOT NULL,
    [Comment]        VARCHAR (4000) NOT NULL,
    [DateCreated]    DATETIME2 (7)  CONSTRAINT [DF_VehicleComment_DateCreated] DEFAULT (getdate()) NULL,
    [CreatedUserId]  INT            NULL,
    [DateModified]   DATETIME2 (7)  NULL,
    [ModifiedUserId] INT            NULL,
    [DateDeleted]    DATETIME2 (7)  NULL,
    [DeletedUserId]  INT            NULL,
    CONSTRAINT [PK_VehicleComment] PRIMARY KEY CLUSTERED ([Id] ASC) ON [BeholderData1],
    CONSTRAINT [FK_VehicleComment_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_VehicleComment_DeletedUser] FOREIGN KEY ([DeletedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_VehicleComment_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_VehicleComment_Vehicle] FOREIGN KEY ([VehicleId]) REFERENCES [Beholder].[Vehicle] ([Id])
);

