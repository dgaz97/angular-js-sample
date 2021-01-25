CREATE TABLE [dbo].[UserRegistrationToken] (
    [Id]     BIGINT     IDENTITY (1, 1) NOT NULL,
    [UserId] BIGINT     NULL,
    [Token]  NCHAR (10) NOT NULL,
    CONSTRAINT [PK_SecurityToken] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UX_UserRegistrationToken_Token] UNIQUE NONCLUSTERED ([Token] ASC)
);

