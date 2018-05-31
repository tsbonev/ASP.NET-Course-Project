CREATE TABLE [dbo].[Users] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]       NVARCHAR (200) NOT NULL,
    [LastName]        NVARCHAR (200) NOT NULL,
    [IsAdministrator] BIT            CONSTRAINT [DF_Users_IsAdministrator] DEFAULT ((0)) NOT NULL,
    [Username]        NVARCHAR (200) CONSTRAINT [DF_Users_Username] DEFAULT ('') NOT NULL,
    [Password]        NVARCHAR (200) CONSTRAINT [DF_Users_Password] DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([ID] ASC)
);





