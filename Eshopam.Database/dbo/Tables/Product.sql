CREATE TABLE [dbo].[Product] (
    [Id]          INT             NOT NULL,
    [Code]        NVARCHAR (20)   NOT NULL,
    [Name]        NVARCHAR (100)  NOT NULL,
    [Description] NVARCHAR (MAX)  NOT NULL,
    [Price]       REAL            NOT NULL,
    [Photo]       VARBINARY (MAX) NOT NULL,
    [CategoryId]  INT             NOT NULL,
    [UserId]      INT             NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Product_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Category] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Product_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [IX_Product] UNIQUE NONCLUSTERED ([Code] ASC)
);



