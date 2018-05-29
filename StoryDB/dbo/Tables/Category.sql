CREATE TABLE [dbo].[Category] (
    [ID]   INT            NOT NULL,
    [Name] NVARCHAR (MAX) NOT NULL,
    [lft]  INT            NOT NULL,
    [rgt]  INT            NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([ID] ASC)
);



