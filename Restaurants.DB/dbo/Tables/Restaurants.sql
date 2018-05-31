CREATE TABLE [dbo].[Restaurants] (
    [ID]          INT             IDENTITY (1, 1) NOT NULL,
    [CategoryID]  INT             NOT NULL,
    [CityID]      INT             NOT NULL,
    [Name]        NVARCHAR (200)  NOT NULL,
    [Description] NTEXT           NOT NULL,
    [ImageName]   NVARCHAR (1000) NULL,
    [DateCreated] DATETIME        NOT NULL,
    [Email]       NCHAR (10)      NULL,
    CONSTRAINT [PK_Restaurants] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Restaurants_Categories] FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([ID]),
    CONSTRAINT [FK_Restaurants_Cities] FOREIGN KEY ([CityID]) REFERENCES [dbo].[Cities] ([ID])
);



