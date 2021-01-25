CREATE TABLE [dbo].[UserInfo] (
    [Id]        INT           NOT NULL,
    [FirstName] VARCHAR (50)  NOT NULL,
    [LastName]  VARCHAR (50)  NOT NULL,
    [Email]     VARCHAR (100) NOT NULL,
    CONSTRAINT [Key1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Relationship6] FOREIGN KEY ([Id]) REFERENCES [dbo].[User] ([Id])
);

