CREATE TABLE [Common].[State] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [StateCode]      CHAR (2)      NOT NULL,
    [StateName]      VARCHAR (25)  NOT NULL,
    [DateCreated]    DATETIME2 (7) CONSTRAINT [DF_State_DateCreated] DEFAULT (getdate()) NOT NULL,
    [CreatedUserId]  INT           NOT NULL,
    [DateModified]   DATETIME2 (7) NULL,
    [ModifiedUserId] INT           NULL,
    CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED ([Id] ASC) ON [CommonDATA1],
    CONSTRAINT [FK_State_CreatedUser] FOREIGN KEY ([CreatedUserId]) REFERENCES [Security].[User] ([Id]),
    CONSTRAINT [FK_State_ModifiedUser] FOREIGN KEY ([ModifiedUserId]) REFERENCES [Security].[User] ([Id])
);

