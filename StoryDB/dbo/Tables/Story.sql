CREATE TABLE [dbo].[Story] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (MAX) NOT NULL,
    [ImgLink]  NVARCHAR (MAX) NOT NULL,
    [Slug]     NVARCHAR (MAX) NOT NULL,
    [Category] INT            NOT NULL,
    CONSTRAINT [PK_Stories] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Story_Category] FOREIGN KEY ([Category]) REFERENCES [dbo].[Category] ([ID]) ON DELETE CASCADE
);

